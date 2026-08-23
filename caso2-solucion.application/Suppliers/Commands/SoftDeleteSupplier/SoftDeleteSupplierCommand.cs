using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Proveedores.Commands.UpdateProveedorCommand
{
    public record SoftDeleteSupplierCommand(int Id) : IRequest;
}
