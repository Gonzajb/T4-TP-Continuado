namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stored_Procedure_Estadisticas : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE PROCEDURE estadisticaporcentajeaviso (@avisoId INT) AS BEGIN
SELECT Porcentaje,
       count(1) AS Cantidad,
       sum(count(1)) OVER () AS TotalUsuarios,
                          maximo AS MaximoAviso
FROM
  (SELECT cast(CAST(SUM(ap.Prioridad) AS float) / cast(Maximo AS float) AS decimal(10, 1)) AS Porcentaje,
          maximo
   FROM
     (SELECT SUM(ap1.Prioridad) AS maximo,
             ap1.Aviso_Id
      FROM AptitudesPorAviso ap1
      WHERE ap1.Aviso_Id = @avisoId
      GROUP BY ap1.Aviso_Id) a1
   INNER JOIN AptitudesPorAviso ap ON ap.Aviso_Id = a1.Aviso_Id
   INNER JOIN UsuarioEmpleadoAptitud uea ON ap.Aptitud_Id = uea.Aptitud_Id
   WHERE ap.Aviso_Id = @avisoId
   GROUP BY UsuarioEmpleado_Id,
            maximo) AS TEMP
GROUP BY temp.Porcentaje,
         temp.maximo
ORDER BY Porcentaje END
");
            Sql(@"CREATE PROCEDURE [AvisoAprobadosPorUsuario] @avisoId INT AS BEGIN
SELECT COUNT(UsuarioEmpleado_Id) AS TOTAL
FROM UsuarioEmpleadoAviso
WHERE Aviso_Id = @avisoId END");

            Sql(@"CREATE PROCEDURE [AvisoDesaprobadosPorUsuario] @avisoId INT AS BEGIN
SELECT COUNT(UsuarioEmpleado_Id) AS TOTAL
FROM UsuarioEmpleadoAviso1
WHERE Aviso_Id = @avisoId END");
        }
        
        public override void Down()
        {
        }
    }
}
