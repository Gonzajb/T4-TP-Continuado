using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio;
using TallerIV.Datos.Repositorios;
using TallerIV.Dominio.RangoEstadisitica;

namespace TallerIV.Negocio.Servicios
{
    public class AvisoSPService
    {
        private SqlRepository sqlRepository;
        public AvisoSPService()
        {
            sqlRepository = new SqlRepository();
        }
        public int AprobacionesDeUsuarios(int IdAviso)
        {
            return sqlRepository.AprobacionesDePostulantes(IdAviso);
        }
        public int DesaprobacionesDeUsuarios (int IdAviso)
        {
            return sqlRepository.DesaprobacionesDePostulantes(IdAviso);
        }
        public List<RangoEstadistica> PorcentajeDePuntos(int IdAviso)
        {
            return sqlRepository.PorcentajeDePuntos(IdAviso);
        }
        public float CalcularPorcentaje(int parcial1, int parcial2)
        {
            float resultado;

            
            if (parcial1+parcial2 == 0)
            {
                resultado = 0;                
            } else
            {
                resultado = (float)parcial1 / (float)(parcial1 + parcial2); ;
            }
            return resultado;

        }

        // Teniea sentido si devolvia todos los rangos pero solo se devuelven los que tengan datos.

        //public RangoEstadistica[] DevolverRangoEstadisticaOrdenado(int IdAviso)
        //{
        //    List<RangoEstadistica> Temp = PorcentajeDePuntos(IdAviso);
        //    RangoEstadistica[] rangoEstadistica = new RangoEstadistica[11];
        //    for (int j = 0; j < rangoEstadistica.Length; j++)
        //    {
        //        rangoEstadistica[j] = new RangoEstadistica();
        //    }
        //    decimal contador = 0;
        //    int i = 0;
        //    while (contador !=rangoEstadistica.Length)
        //    {
        //        if (i >= Temp.Count || contador*10 != Temp[i].Rango)
        //        {
        //            rangoEstadistica[(int)contador].Rango = (decimal)contador * (decimal)10.0;
        //            rangoEstadistica[(int)contador].Cantidad = 0;
        //            rangoEstadistica[(int)contador].PorcentajePostulantes = 0;
        //            contador = contador + 1;
        //        } else
        //        {
        //            rangoEstadistica[(int)contador].Rango = (int)Temp[i].Rango;
        //            rangoEstadistica[(int)contador].Cantidad = Temp[i].Cantidad;
        //            rangoEstadistica[(int)contador].PorcentajePostulantes = Temp[i].PorcentajePostulantes;
        //            i++;
        //            contador = contador + 1;
        //        }
        //    }
        //    return rangoEstadistica;
        //}
    }
}
