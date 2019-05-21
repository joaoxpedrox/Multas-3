using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Multas
    {
        public int ID { get; set; }

        public string Infracao { get; set; }

        [Display(Name = "Local da Multa")]
        public string LocalDaMulta { get; set; }

        public decimal ValorMulta { get; set; }
        [Display(Name ="Data de aplicação da multa")]
        public DateTime DataDaMulta { get; set; }

        //Criação das chaves forasteiras 

        //FK para Viaturas 
        [ForeignKey("Viatura")]
        public int ViaturaFK { get; set; } //base de Dados
        public virtual Viaturas Viatura { get; set; } //c# 

        //FK para o condutor 
        [ForeignKey("Condutor")]
        public int CondutorFK { get; set; }
        public virtual Condutores Condutor { get; set; }

        //FK para agentes 
        [ForeignKey("Agente")]
        public int AgenteFK { get; set; }
        public virtual Agentes Agente { get; set; }

     
}
}