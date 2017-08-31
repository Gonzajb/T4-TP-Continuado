using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TallerIV.Negocio;

namespace TallerIV.Datos
{
    public class TallerIVContext : DbContext
    {

        public TallerIVContext() : base("TallerIVContext")
        {
        }

        public DbSet<Aviso> Aviso { get; set; }
        public DbSet<Encuentro> Encuentro { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioEmpleado> UsuarioEmpleado { get; set; }
        public DbSet<UsuarioEmpresa> UsuarioEmpresa { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
