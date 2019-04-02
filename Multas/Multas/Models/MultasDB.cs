using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Multas.Models
{ 

    public class MultasDB: DbContext {
        //identificar qual o SGBD a usar 




        //definir as tabelas da BD 

        public DbSet<Condutores> Condutores { get; set; }
        public DbSet<Viaturas> Viaturas { get; set; }
        public DbSet<Multas> Multas { get; set; }
        public DbSet<Agentes> Agentes { get; set; }




    }

}