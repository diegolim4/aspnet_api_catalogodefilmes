using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogofilmes.ViewModel
{
    public class MovieViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }
    }
}
