﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Multas.Models
{ 

    public class MultasDB: DbContext {
        //identificar qual o SGBD a usar 



        public MultasDB() : base("MultasDBConnectionString") { }

        // vamos colocar, aqui, as instruções relativas às tabelas do 'negócio'
        // descrever os nomes das tabelas na Base de Dados
        public virtual DbSet<Multas> Multas { get; set; } // tabela Multas
        public virtual DbSet<Condutores> Condutores { get; set; } // tabela Condutores
        public virtual DbSet<Agentes> Agentes { get; set; } // tabela Agentes
        public virtual DbSet<Viaturas> Viaturas { get; set; } // tabela Viaturas




    }

}