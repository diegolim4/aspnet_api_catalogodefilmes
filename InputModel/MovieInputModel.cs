using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogofilmes.InputModel
{
    public class MovieInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do filme deve conter entre 3 e 100 caracteres")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O gênero do filme deve conter entre 3 e 100 caracteres")]
        public string Genre { get; set; }
        [Required]
        public int Year { get; set; }        
       
    }
}
