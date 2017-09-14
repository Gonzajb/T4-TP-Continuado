﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public class UsuarioEmpleado: UsuarioPersona
    {
        public UsuarioEmpleado(){}

        public UsuarioEmpleado(DateTime fechaDeResgistro, string email, string userName, string nombre, string apellido, DateTime? fechaDeNacimiento) : base(fechaDeResgistro, email, userName,nombre,apellido,fechaDeNacimiento)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaDeNacimiento = fechaDeNacimiento;
        }
        public virtual List<Tag> Tags { get; set; }
    }
}