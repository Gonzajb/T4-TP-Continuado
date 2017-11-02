using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TallerIV.Dominio;
using Microsoft.AspNet.Identity.EntityFramework;
using TallerIV.Dominio.Avisos;

namespace TallerIV.Datos
{
    public class TallerIVDbContext : IdentityDbContext<Usuario>
    {

        public TallerIVDbContext() : base("TallerIVContext")
        {
        }

        public DbSet<Aviso> Avisos { get; set; }
        public DbSet<Encuentro> Encuentros { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Aptitud> Aptitudes { get; set; }
        public DbSet<AptitudPorAviso> AptitudesPorAviso { get; set; }
        //public DbSet<UsuarioEmpleado> UsuarioEmpleado { get; set; }
        //public DbSet<UsuarioEmpresa> UsuarioEmpresa { get; set; }


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
            modelBuilder.Entity<Aptitud>().HasKey(a => a.Id).ToTable("Aptitudes");

            modelBuilder.Entity<Aviso>().HasKey(a => a.Id).ToTable("Avisos");
            modelBuilder.Entity<Aviso>().HasRequired(a => a.UsuarioReclutador).WithMany().HasForeignKey(a => a.UsuarioReclutador_Id);
            modelBuilder.Entity<Aviso>().HasMany(a => a.AptitudesBuscadas).WithRequired().HasForeignKey(ab => ab.Aviso_Id);
            modelBuilder.Entity<Aviso>().HasMany(a => a.UsuariosEmpleadoAprobados).WithMany();
            modelBuilder.Entity<Aviso>().HasMany(a => a.UsuariosEmpleadoDesaprobados).WithMany();

            modelBuilder.Entity<AptitudPorAviso>().HasKey(x => new { x.Aptitud_Id, x.Aviso_Id })
                .ToTable("AptitudesPorAviso");
            modelBuilder.Entity<AptitudPorAviso>().HasRequired(x => x.Aptitud).WithMany().HasForeignKey(a => a.Aptitud_Id);

            modelBuilder.Entity<Encuentro>().HasKey(e => e.Id).ToTable("Encuentros");
            modelBuilder.Entity<Encuentro>().HasRequired(e => e.UsuarioReclutador).WithMany().HasForeignKey(a => a.UsuarioReclutador_Id);

            modelBuilder.Entity<Like>().HasKey(l => l.Id).ToTable("Likes");
            modelBuilder.Entity<Like>().HasRequired(l => l.UsuarioReclutador).WithMany().HasForeignKey(a => a.UsuarioReclutador_Id);
            modelBuilder.Entity<Like>().HasRequired(l => l.UsuarioEmpleado).WithMany().HasForeignKey(a => a.UsuarioEmpleado_Id);

            modelBuilder.Entity<IdentityUser>().HasKey(u => u.Id);
            modelBuilder.Entity<IdentityUserLogin>().HasKey(u => u.UserId);
            modelBuilder.Entity<IdentityUserRole>().HasKey(u => new { u.UserId, u.RoleId });

            modelBuilder.Entity<UsuarioEmpleado>().ToTable("Usuarios")
                .HasMany(u => u.Aptitud).WithMany();
            modelBuilder.Entity<UsuarioEmpleado>()
                .HasOptional(u => u.Busqueda).WithMany().HasForeignKey(x => x.Busqueda_Id);
            modelBuilder.Entity<UsuarioEmpleado>().HasMany(x => x.AvisosAprobados).WithMany();
            modelBuilder.Entity<UsuarioEmpleado>().HasMany(x => x.AvisosDesaprobados).WithMany();

            modelBuilder.Entity<UsuarioEmpresa>().ToTable("Usuarios")
                .HasMany(u => u.Avisos).WithRequired().HasForeignKey(a => a.UsuarioEmpresa_Id);
            modelBuilder.Entity<UsuarioEmpresa>()
                .HasMany(u => u.Reclutadores).WithRequired().HasForeignKey(a => a.UsuarioEmpresa_Id);
        }

        public static TallerIVDbContext Create()
        {
            return new TallerIVDbContext();
        }

        public System.Data.Entity.DbSet<TallerIV.Dominio.Usuarios.BusquedaUsuarioPostulante> BusquedaUsuarioPostulantes { get; set; }

        public System.Data.Entity.DbSet<TallerIV.Dominio.UsuarioReclutador> IdentityUsers { get; set; }
    }
}
