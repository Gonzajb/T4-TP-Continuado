﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio.Avisos;
using TallerIV.Dominio.Coincidencias;

namespace TallerIV.Dominio.Coincidencias.CalculadoresDePorcentaje
{
    public class CalculadorDePorcentajeAviso : CalcularPorcentaje
    {
        public CalculadorDePorcentajeAviso() { }
        public CalculadorDePorcentajeAviso(Aviso aviso)
        {
            this.Aviso = aviso;
            this.PuntajeMaximo = CalcularPuntajeMaximo(aviso);
        }
        //public CalculadorDePorcentaje(UsuarioEmpleado usuarioEmpleado)
        //{
        //    this.Empleado = usuarioEmpleado;
        //}
        //private void CalcularPuntajeMaximo(Aviso aviso)
        //{
        //    int puntajeMaximo = 0;
        //    foreach(Tag tag in aviso.TagsBuscados)
        //    {
        //        //PuntajeMaximo += (int)aviso.TagsBuscadosPrioridad;
        //    }
        //    this.PuntajeMaximo = puntajeMaximo;
        //}
        public Coincidencia GenerarCoincidencia(UsuarioEmpleado empleado)
        {
            int PuntajeEmpleado = 0;
            Coincidencia coincidencia;
            if (empleado.Aptitud != null && Aviso.AptitudesBuscadas != null)
            {
                foreach (AptitudPorAviso aptitudAviso in Aviso.AptitudesBuscadas)
                {
                    int i = 0;
                    while (i < empleado.Aptitud.Count)
                    {
                        if (aptitudAviso.Aptitud.Id == empleado.Aptitud[i].Id)
                        {
                            PuntajeEmpleado += (int)aptitudAviso.Prioridad;
                        }
                        i++;
                    }
                    
                }
                if (PuntajeEmpleado == 0)
                {
                    coincidencia = new Coincidencia(0.00f, empleado, Aviso);
                } else
                {
                    coincidencia = new Coincidencia(PuntajeEmpleado / PuntajeMaximo, empleado, Aviso);

                }
            }
            else
            {
                coincidencia = null;
            }
            return coincidencia;
        }
        public Aviso Aviso { get; }
        public int PuntajeMaximo { get; }
    }

}
