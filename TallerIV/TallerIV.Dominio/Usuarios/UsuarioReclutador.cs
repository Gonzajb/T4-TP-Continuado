﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public class UsuarioReclutador: UsuarioPersona
    {
        public UsuarioReclutador() { }

        public UsuarioReclutador (DateTime fechaDeResgistro, string email, string userName, string nombre, string apellido, DateTime? fechaDeNacimiento) : base(fechaDeResgistro, email, userName,nombre,apellido,fechaDeNacimiento)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaDeNacimiento = fechaDeNacimiento;
        }
        //public List<Aviso>Avisos { get; set; }
        public string UsuarioEmpresa_Id { get; set; }
    }
}
