using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TallerIV.Dominio.RangoEstadisitica;

namespace TallerIV.Models
{
    public class EstadisticaViewModel
    {
        [Required]
        public string TituloAviso;
        //public int TotalReclutador;
        public float PorcentajeApRec;
        public float PorcentajeDesRec;
        //public int TotalPostulante;
        public float PorcentajeApPost;
        public float PorcentajeDesPost;
        public List<RangoEstadistica> RangosEstadistica = new List<RangoEstadistica>();    
    }
}