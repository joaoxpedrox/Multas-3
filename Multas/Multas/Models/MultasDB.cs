using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class MultasDB : DbContext
    {


        public MultasDB() : base("MultasDBConnectionString") { }


        // definir as tabelas
        public virtual DbSet<Multas> Multas { get; set; } // tabela Multas
        public virtual DbSet<Condutores> Condutores { get; set; } // tabela Condutores
        public virtual DbSet<Agentes> Agentes { get; set; } // tabela Agentes
        public virtual DbSet<Viaturas> Viaturas { get; set; } // tabela Viaturas


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }




    }
}