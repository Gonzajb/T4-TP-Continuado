using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TallerIV.Dominio;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TallerIV.Datos
{
    public class TallerIVDbContext : IdentityDbContext<Usuario>
    {

        public TallerIVDbContext() : base("TallerIVContext")
        {
        }

        public DbSet<Aviso> Aviso { get; set; }
        public DbSet<Encuentro> Encuentro { get; set; }
        public DbSet<Like> Like { get; set; }
        //public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioEmpleado> UsuarioEmpleado { get; set; }
        public DbSet<UsuarioEmpresa> UsuarioEmpresa { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //EJEMPLOS DE ARMADO DE MAPEO A BASE DE DATOS
            //DEFINICIONES
            //DEFINICIÓN DE CLAVE PRINCIPAL Y NOMBRE DE TABLA:   modelBuilder.Entity<Aviso>().HasKey(a => a.Id).ToTable("Avisos");
            //RELACIONES
            //MUCHOS A MUCHOS: modelBuilder.Entity<Aviso>().HasMany(a => a.TagsBuscados).WithMany();
            //UNO A MUCHOS:    modelBuilder.Entity<UsuarioEmpresa>().HasMany(u => u.Avisos).WithRequired().HasForeignKey(a => a.UsuarioEmpresa_Id);
            //MUCHOS A UNO:    modelBuilder.Entity<Like>().HasRequired(l => l.UsuarioReclutador).WithMany().HasForeignKey(a => a.UsuarioReclutador_Id);

            modelBuilder.Entity<Aviso>().HasKey(a => a.Id).ToTable("Avisos");
            modelBuilder.Entity<Aviso>().HasRequired(a => a.UsuarioReclutador).WithMany().HasForeignKey(a => a.UsuarioReclutador_Id);
            modelBuilder.Entity<Aviso>().HasMany(a => a.TagsBuscados).WithMany();

            modelBuilder.Entity<Encuentro>().HasKey(e => e.Id).ToTable("Encuentros");
            modelBuilder.Entity<Encuentro>().HasRequired(e => e.UsuarioReclutador).WithMany().HasForeignKey(a => a.UsuarioReclutador_Id);

            modelBuilder.Entity<Like>().HasKey(l => l.Id).ToTable("Likes");
            modelBuilder.Entity<Like>().HasRequired(l => l.UsuarioReclutador).WithMany().HasForeignKey(a => a.UsuarioReclutador_Id);
            modelBuilder.Entity<Like>().HasRequired(l => l.UsuarioEmpleado).WithMany().HasForeignKey(a => a.UsuarioEmpleado_Id);

            modelBuilder.Entity<IdentityUser>().HasKey(u => u.Id);
            modelBuilder.Entity<IdentityUserLogin>().HasKey(u => u.UserId);
            modelBuilder.Entity<IdentityUserRole>().HasKey(u => new { u.UserId, u.RoleId });

            modelBuilder.Entity<UsuarioEmpleado>().ToTable("Usuarios")
                .HasMany(u => u.Tags).WithMany();

            modelBuilder.Entity<UsuarioEmpresa>().ToTable("Usuarios")
                .HasMany(u => u.Avisos).WithRequired().HasForeignKey(a => a.UsuarioEmpresa_Id);

            //modelBuilder.Entity<UsuarioReclutador>().ToTable("Usuarios")
            //    .HasMany(u => u.Avisos).WithRequired().HasForeignKey(a => a.UsuarioReclutador_Id);
        }

        public static TallerIVDbContext Create()
        {
            return new TallerIVDbContext();
        }

        public System.Data.Entity.DbSet<TallerIV.Dominio.Tag> Tags { get; set; }
    }
}
