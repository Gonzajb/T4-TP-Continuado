﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TallerIV.Dominio
{
    public abstract class Usuario: IdentityUser
    {
        public DateTime FechaRegistro { get; set; }
        
    }
}
