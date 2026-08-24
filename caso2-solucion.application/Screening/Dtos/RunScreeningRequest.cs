using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Screening.Dtos
{
    public record RunScreeningRequest(List<EScreeningSource> Sources);
}
