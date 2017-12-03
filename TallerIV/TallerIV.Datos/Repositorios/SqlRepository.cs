using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using TallerIV.Dominio.RangoEstadisitica;

namespace TallerIV.Datos.Repositorios
{
    public class SqlRepository
    {
        private SqlConnection sqlConnection;
        private SqlDataReader reader = null;
        private SqlCommand cmd;


        public SqlRepository()
        {
            string cs = ConfigurationManager.ConnectionStrings["TallerIVContext"].ConnectionString;
            this.sqlConnection = new SqlConnection(cs);            
        }

        public int AprobacionesDePostulantes (int IdAviso)
        {
            int resultado;
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "AvisoAprobadosPorUsuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@avisoId", IdAviso);

            sqlConnection.Open();

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                resultado = reader.GetInt32(reader.GetOrdinal("TOTAL"));
            } else
            {
                resultado = 0;
            }
            cmd.Dispose();
            sqlConnection.Close();
            return resultado;
        }

        public int DesaprobacionesDePostulantes(int IdAviso)
        {
            int resultado;
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "AvisoDesaprobadosPorUsuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@avisoId", IdAviso);

            sqlConnection.Open();

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                resultado = reader.GetInt32(reader.GetOrdinal("TOTAL"));
            } else
            {
                resultado = 0;
            }

            cmd.Dispose();
            sqlConnection.Close();
            return resultado;
        }

        public List<RangoEstadistica> PorcentajeDePuntos (int IdAviso)
        {
            List<double> Porcentaje = new List<double>();
            List<int> Cantidad = new List<int>();
            List<RangoEstadistica> rangoEstadistica = new List<RangoEstadistica>();

            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "EstadisticaPorcentajeAviso";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@avisoId", IdAviso);
            sqlConnection.Open();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {                
                rangoEstadistica.Add(new RangoEstadistica(reader.GetDecimal(reader.GetOrdinal("PorcentajeDePuntos")), reader.GetInt32(reader.GetOrdinal("Cantidad")),reader.GetDecimal(reader.GetOrdinal("PorcentajeDeUsuarios"))));
            }
            cmd.Dispose();
            sqlConnection.Close();
            return rangoEstadistica;
        }
        
    }
}
