using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public abstract class Usuario : IdentityUser
    {
        public Usuario() { }
        public Usuario(DateTime fechaRegistro, string email, string userName):base(userName) {
            this.FechaRegistro = fechaRegistro;
            this.Email = email;
        }
        public DateTime FechaRegistro { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Usuario> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
