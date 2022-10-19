using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain
{
    public class Director:BaseDomainModel
    {
        public string Nombre { get; set; } = String.Empty;

        public string Apellido { get; set; } = String.Empty;

        public int VideoId { get; set; }

        public virtual Video? Video { get; set; }

    }
}
