using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogofilmes.Exceptions
{
    public class MovieNotRegisteredException : Exception
    {
        public MovieNotRegisteredException()
           : base("Este jogo não está cadastrado")
        { }
    }
}
