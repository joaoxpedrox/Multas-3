using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Multas
    {
        public int ID { get; set; }

        public string Infracao { get; set; }

        public string LocalDaMulta { get; set; }

        public decimal ValorMulta { get; set; }

        public DateTime DataDaMulta { get; set; }


    }
}