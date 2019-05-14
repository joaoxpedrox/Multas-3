using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Agentes
    {

        [Key] //identifica este atributo como Primary Key 
        public int ID { get; set; }

       // [Required]
        //Se quiser que a mensagem de erro venha em X lingua, devo     
        //[Required (ErrorMessage="O nome é de preenchimento obrigatório")] 
       // [StringLength(50, ErrorMessage ="O {0} deve ter, no máximo, {1} caracteres")]
      //  [RegularExpression(""  , ErrorMessage ="O {0} só pode conter letras. Cada palavra deve começar com uma Maiúscula")]

        //[]- Representa um conjunto, de onde sai apenas um símbolo 
        // + - Representa uma, ou mais ocorrªencias
        // * representa zero, ou mais, ocorrências 
        // ? representa zero ou uma ocorrência 
        //  [A-Z][a-z]+  (( | e | de | do | dos | da | das ) [A-Z][a-z]+)*
        //          ( ((e|de|do|dos|da|das) )?)
        //          ( (((e|d(a|o)s?|e)) )?)




        [Required(ErrorMessage = "o Nome é de preenchimento obrigatório.")]
        [StringLength(40)]
        [RegularExpression("[A-ZÁÉÍÓÚÂ][a-záéíóúàèìòùäëïöüãõâêîôûçñ]+(( | e | de | da | das | do | dos |-|'|d')[A-ZÁÉÍÓÚÂ][a-záéíóúàèìòùäëïöüãõâêîôûçñ]*){1,3}",
                         ErrorMessage = "só são aceites palavras, começadas por Maiúscula, " +
                                       "separadas por um espeço em branco.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "a Esquadra é de preenchimento obrigatório.")]
        [StringLength(30)]
        [RegularExpression("Torres Novas|Tomar|Entroncamento",
                           ErrorMessage = " só é aceite Torres Novas ou Tomar ou Entroncamento")]
        public string Esquadra { get; set; }

        [StringLength(30)]
        public string Fotografia { get; set; }

        // lista das multas associadas ao Agente
        public ICollection<Multas> ListaDeMultas { get; set; }



    }
}