using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain
{
    public class Actor:BaseDomainModel
    {
        public Actor()
        {
            Videos = new HashSet<Video>();
        }

        public string Nombre { get; set; } = String.Empty;

        public string Apellido { get; set; } = String.Empty;

        public virtual ICollection<Video> Videos { get; set; }

    }
}
