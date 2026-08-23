using Azure.Core;
using caso2_solucion.application;
using caso2_solucion.application.Proveedores.Commands.CreateProveedor;
using caso2_solucion.application.Proveedores.Commands.DeleteProveedor;
using caso2_solucion.application.Proveedores.Commands.UpdateProveedorCommand;
using caso2_solucion.application.Proveedores.Queries.GetProveedorById;
using caso2_solucion.application.Proveedores.Queries.GetProveedoresList;
using caso2_solucion.application.Suppliers.Commands.UpdateSupplier;
using caso2_solucion.domain.entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace caso2_solucion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
            => Ok(await _mediator.Send(new GetSuppliersListQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var supplier = await _mediator.Send(new GetSupplierByIdQuery(id));
            return supplier is null ? NotFound() : Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateSupplierCommand command)
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
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            await _mediator.Send(new DeleteSupplierCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> SoftDeleteSupplier(int id)
        {
            await _mediator.Send(new SoftDeleteSupplierCommand(id));
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierRequest request)
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

    }
}
