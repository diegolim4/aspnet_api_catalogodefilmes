using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogofilmes.Exceptions
{
    public class MovieRegisteredException : Exception
    {
        public MovieRegisteredException()
           : base("Este filme já está cadastrado")
        { }
    }
}
