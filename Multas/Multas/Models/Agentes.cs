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

        [Required]
        //Se quiser que a mensagem de erro venha em X lingua, devo     
        //[Required (ErrorMessage="O nome é de preenchimento obrigatório")] 
        [StringLength(50, ErrorMessage ="O {0} deve ter, no máximo, {1} caracteres")]
        [RegularExpression(""  , ErrorMessage ="O {0} só pode conter letras. Cada palavra deve começar com uma Maiúscula")]

        //[]- Representa um conjunto, de onde sai apenas um símbolo 
        // + - Representa uma, ou mais ocorrªencias
        // * representa zero, ou mais, ocorrências 
        // ? representa zero ou uma ocorrência 
        [A-Z][a-z]+  (( | e | de | do | dos | da | das ) [A-Z][a-z]+)*
                    ( ((e|de|do|dos|da|das) )?)
                    ( (((e|d(a|o)s?|e)) )?)


        public string Nome { get; set; }




        [Required(ErrorMessage = "A esquadra é de preenchimento obrigatório")]
        [StringLength(20, ErrorMessage="A {0} deve ter, no máximo, {1} caracteres")]
        public string Esquadra { get; set; }

        public string Fotografia { get; set; }
        //********************************************
        //lista das multas associadas ao agente 
        public ICollection<Multas> ListaDeMultas { get; set; }

    }
}