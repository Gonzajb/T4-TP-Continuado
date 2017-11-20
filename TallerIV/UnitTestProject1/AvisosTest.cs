﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TallerIV.Dominio;
using TallerIV.Dominio.Coincidencias;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TallerIV.Dominio.Avisos;
using TallerIV.Dominio.Usuarios;

namespace UnitTestProject1
{
    [TestClass]
    public class AvisosTest
    {
        [TestMethod]
        public void ProbarAprobacionAvisos()
        {
            List<AptitudPorAviso> AptitudesAviso = new List<AptitudPorAviso>();
            Aptitud Ap1 = new Aptitud(1, "Apt1");
            Aptitud Ap2 = new Aptitud(2, "Apt2");
            Aptitud Ap3 = new Aptitud(3, "Apt3");
            Aptitud Ap4 = new Aptitud(4, "Apt4");

            AptitudPorAviso APV1 = new AptitudPorAviso(1, 22, Ap1, Prioridad.Baja);
            AptitudPorAviso APV2 = new AptitudPorAviso(2, 22, Ap2, Prioridad.Normal);
            AptitudPorAviso APV3 = new AptitudPorAviso(3, 22, Ap3, Prioridad.Excluyente);
            AptitudPorAviso APV4 = new AptitudPorAviso(4, 22, Ap4, Prioridad.Normal);
            AptitudesAviso.Add(APV1);
            AptitudesAviso.Add(APV2);
            AptitudesAviso.Add(APV3);
            AptitudesAviso.Add(APV4);

            List<Aptitud> AptitudesPostulante = new List<Aptitud>();
            AptitudesPostulante.Add(Ap2);
            AptitudesPostulante.Add(Ap3);

            List<Aptitud> ListaPrueba = new List<Aptitud>();
            Aptitud ApPrueba = new Aptitud(10, "Prueba");
            ListaPrueba.Add(ApPrueba);
            ListaPrueba.Add(Ap1);
                       
            UsuarioEmpresa UE = new UsuarioEmpresa("3066552214", "Razon Social", DateTime.Now, "aa@gmail.com", "aa@gmail.com");
            UsuarioReclutador UR = new UsuarioReclutador(DateTime.Now, "dsa@gmail.com", "", "dsa@gmail.com", "GG", "dsad", DateTime.Now, "dsa");
            List<Aviso> Avisos = new List<Aviso>();
            for (int i =0; i < 5; i++)
            {
                if (i > 2)
                {
                    Aviso a = new Aviso("Titulo" + i, "Desc" + i, DateTime.Now, UR, AptitudesAviso,1000, Prioridad.Baja, TipoRelacionDeTrabajo.Dependencia, TallerIV.Dominio.Usuarios.Prioridad.Normal, 4, TallerIV.Dominio.Usuarios.Prioridad.Normal, "dsasa", "DS");
                    Avisos.Add(a);
                } else
                {
                    Aviso a = new Aviso("Titulo" + i, "Desc" + i, DateTime.Now, UR, AptitudesAviso, 1000, Prioridad.Baja, TipoRelacionDeTrabajo.Dependencia, TallerIV.Dominio.Usuarios.Prioridad.Normal, 4, TallerIV.Dominio.Usuarios.Prioridad.Normal, "dsasa", "DS");
                    Avisos.Add(a);
                }
                
            }
            List <UsuarioEmpleado> Empleados = new List<UsuarioEmpleado>();
            for (int i=0; i < 5; i++)
            {
                if (i == 2)
                {
                    UsuarioEmpleado UEmp = new UsuarioEmpleado(DateTime.Now, "aa@gmail.com", "", "AA" + i, "dd", "asa", DateTime.Now, "SA", AptitudesPostulante);
                    Empleados.Add(UEmp);
                } else
                {
                    UsuarioEmpleado UEmp = new UsuarioEmpleado(DateTime.Now, "aa@gmail.com", "", "AA" + i, "dd", "asa", DateTime.Now, "SA", ListaPrueba);
                    Empleados.Add(UEmp);

                }
            }
            GeneradorCoincidencias GC = new GeneradorCoincidencias();
            List<Coincidencia> ListaCoincidencias = new List<Coincidencia>();
            ListaCoincidencias = GC.GenerarListadoCoincidencias(Avisos[3], Empleados);

            if (ListaCoincidencias != null)
            {
                Console.WriteLine("dsadasda");
            }

        }
    }
}
