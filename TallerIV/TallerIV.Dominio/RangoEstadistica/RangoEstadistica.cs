using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio.RangoEstadisitica
{
    public class RangoEstadistica
    {
        public decimal Rango;
        public int Cantidad;
        public decimal PorcentajePostulantes;

        public RangoEstadistica() { }
        public RangoEstadistica (decimal Rango, int Cantidad, decimal PorcentajePostulantes)
        {
            this.Rango = Rango;
            this.Cantidad = Cantidad;
            this.PorcentajePostulantes = PorcentajePostulantes;
        }
    }
}
