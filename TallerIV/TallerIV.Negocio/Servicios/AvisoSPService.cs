using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio;
using TallerIV.Datos.Repositorios;

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
        public List<double> PorcentajeDePuntos(int IdAviso)
        {
            return sqlRepository.PorcentajeDePuntos(IdAviso);
        }
    }
}
