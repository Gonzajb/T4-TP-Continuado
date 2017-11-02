using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TallerIV.Dominio;
using TallerIV.Dominio.Coincidencias;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UnitTestProject1
{
    [TestClass]
    public class AvisosTest
    {
        [TestMethod]
        public void ProbarAprobacionAvisos()
        {
            UsuarioEmpresa UE = new UsuarioEmpresa("3066552214", "Razon Social", DateTime.Now, "aa@gmail.com", "aa@gmail.com");
            UsuarioReclutador UR = new UsuarioReclutador(DateTime.Now, "dsa@gmail.com", "dsa@gmail.com", "GG", "dsad", DateTime.Now, "dsa");
            List<Aviso> Avisos = new List<Aviso>();
            for (int i =0; i < 5; i++)
            {
                Aviso a = new Aviso("Titulo" + i, "Desc" + i, DateTime.Now, UR, null, TipoRelacionDeTrabajo.Dependencia, TallerIV.Dominio.Usuarios.Prioridad.Normal, 4, TallerIV.Dominio.Usuarios.Prioridad.Normal, "dsasa", "La Empresa");
                Avisos.Add(a);
            }
            List <UsuarioEmpleado> Empleados = new List<UsuarioEmpleado>();
            for (int i=0; i < 5; i++)
            {
                UsuarioEmpleado UEmp = new UsuarioEmpleado(DateTime.Now, "aa@gmail.com", "AA" + 1, "dd", "asa", DateTime.Now, "SA", null);
                Empleados.Add(UEmp);
            }
            GeneradorCoincidencias GC = new GeneradorCoincidencias();
            GC.GenerarListadoCoincidencias(Avisos[0], Empleados.AsQueryable());
        }
    }
}
