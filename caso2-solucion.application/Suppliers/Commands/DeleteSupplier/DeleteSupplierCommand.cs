using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Proveedores.Commands.DeleteProveedor
{
    public record DeleteSupplierCommand(int Id) : IRequest;
}
