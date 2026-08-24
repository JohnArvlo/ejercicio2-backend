using Azure.Core;
using caso2_solucion.application;
using caso2_solucion.application.Proveedores.Commands.CreateProveedor;
using caso2_solucion.application.Proveedores.Commands.DeleteProveedor;
using caso2_solucion.application.Proveedores.Commands.UpdateProveedorCommand;
using caso2_solucion.application.Proveedores.Queries.GetProveedorById;
using caso2_solucion.application.Proveedores.Queries.GetProveedoresList;
using caso2_solucion.application.Screening.Commands.RunScreening;
using caso2_solucion.application.Screening.Dtos;
using caso2_solucion.application.Suppliers.Commands.UpdateSupplier;
using caso2_solucion.domain.entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace caso2_solucion.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> GetAllSuppliers()
            => Ok(await _mediator.Send(new GetSuppliersListQuery()));

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySupplierId(int id)
        {
            var supplier = await _mediator.Send(new GetSupplierByIdQuery(id));
            return supplier is null ? NotFound() : Ok(supplier);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.TaxId))
            {
                return BadRequest(new
                {
                    message = "La identificación tributaria es obligatoria."
                });
            }

            if (command.TaxId.Length != 11)
            {
                return BadRequest(new
                {
                    message = "La identificación tributaria debe tener 11 dígitos."
                });
            }

            if (!command.TaxId.All(char.IsDigit))
            {
                return BadRequest(new
                {
                    message = "La identificación tributaria debe contener únicamente dígitos."
                });
            }

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBySupplierId), new { id }, id);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            await _mediator.Send(new DeleteSupplierCommand(id));
            return NoContent();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> SoftDeleteSupplier(int id)
        {
            await _mediator.Send(new SoftDeleteSupplierCommand(id));
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] UpdateSupplierRequest request)
        {
            var command = new UpdateSupplierCommand(
                id,
                request.LegalName,
                request.TradeName,
                request.TaxId,
                request.PhoneNumber,
                request.Email,
                request.Website,
                request.PhysicalAddress,
                request.Country,
                request.AnnualRevenueUsd
            );

            var updated = await _mediator.Send(command);

            if (!updated)
                return NotFound();

            return NoContent();
        }


        /// <param name="request">Fuentes a consultar: ofac, worldbank, icij (mínimo 1, máximo 3).</param>
        [Authorize]
        [HttpPost("{id}/screening")]
        public async Task<IActionResult> RunScreening(int id, [FromBody] RunScreeningRequest request)
        {
            if (request.Sources is null || request.Sources.Count == 0 || request.Sources.Count > 3)
                return BadRequest(new { message = "Debes seleccionar entre 1 y 3 fuentes." });

            var sourceNames = request.Sources.Select(s => s.ToString()).ToList();
            var result = await _mediator.Send(new RunScreeningCommand(id, sourceNames));
            return Ok(result);
        }

    }
}
