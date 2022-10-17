using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain
{
    public class Streamer:BaseDomainModel
    {
        public string Nombre { get; set; } = String.Empty;

        public string? Url { get; set; }
    }
}
