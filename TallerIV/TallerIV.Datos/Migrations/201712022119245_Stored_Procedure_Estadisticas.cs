namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stored_Procedure_Estadisticas : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE PROCEDURE EstadisticaPorcentajeAviso2 (@avisoId INT) AS BEGIN
SELECT PorcentajeDePuntos,
       Cantidad,
       CAST (cast(Cantidad AS float)/ CAST (TotalUsuarios AS float) AS decimal (10,1))*100 AS PorcentajeDeUsuarios,
            MaximoAviso
FROM
  (SELECT PorcentajeDePuntos,
          count(1) AS Cantidad,
          sum(count(1)) OVER () AS TotalUsuarios,
                             maximo AS MaximoAviso
   FROM
     (SELECT cast(CAST(SUM(ap.Prioridad) AS float) / cast(Maximo AS float) AS decimal(10, 1))*100 AS PorcentajeDePuntos,
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
   GROUP BY temp.PorcentajeDePuntos,
            temp.maximo) AS TEMP2
ORDER BY Temp2.PorcentajeDePuntos END");

            Sql(@"CREATE PROCEDURE EstadisticaPorcentajeAviso (@avisoId INT) AS BEGIN
SELECT PorcentajeDePuntos,
       Cantidad,
       CAST (cast(Cantidad AS float)/ CAST (TotalUsuarios AS float) AS decimal (10,1))*100 AS PorcentajeDeUsuarios,
            MaximoAviso
FROM
  (SELECT PorcentajeDePuntos,
          count(1) AS Cantidad,
          sum(count(1)) OVER () AS TotalUsuarios,
                             maximo AS MaximoAviso FROM
     (SELECT cast(CAST(SUM(ap.Prioridad) AS float) / cast(Maximo AS float) AS decimal(10, 1))*100 AS PorcentajeDePuntos, maximo, uea.UsuarioEmpleado_Id
      FROM
        (SELECT SUM(ap1.Prioridad) AS maximo, ap1.Aviso_Id
         FROM AptitudesPorAviso ap1
         WHERE ap1.Aviso_Id = @avisoId
         GROUP BY ap1.Aviso_Id) a1
      INNER JOIN AptitudesPorAviso ap ON ap.Aviso_Id = a1.Aviso_Id
      INNER JOIN UsuarioEmpleadoAptitud uea ON ap.Aptitud_Id = uea.Aptitud_Id
      AND uea.UsuarioEmpleado_Id IN
        (SELECT uea.UsuarioEmpleado_Id
         FROM Usuarios u
         INNER JOIN BusquedaUsuarioPostulante bup ON bup.Id = u.Busqueda_Id
         INNER JOIN UsuarioEmpleadoAptitud uea ON uea.UsuarioEmpleado_Id = u.Id
         INNER JOIN
           (SELECT DISTINCT IIF(a.SueldoOfrecidoPrioridad = 3, a.SueldoOfrecido, -1) AS SueldoOfrecido, IIF(a.TipoRelacionDeTrabajoPrioridad = 3, a.TipoRelacionDeTrabajo, -1) AS TipoRelacionDeTrabajo, IIF(a.HorasTrabajoPrioridad = 3, a.HorasTrabajo, -1) AS HorasTrabajo
            FROM Avisos a
            WHERE a.Id = @avisoId) Temporal ON (Temporal.SueldoOfrecido <= bup.SueldoMinimo
                                         AND Temporal.HorasTrabajo <= bup.HorasTrabajo
                                         AND Temporal.TipoRelacionDeTrabajo <= bup.TipoRelacionDeTrabajo)
         GROUP BY uea.UsuarioEmpleado_Id)
      GROUP BY UsuarioEmpleado_Id, a1.maximo) AS TEMP
   GROUP BY temp.PorcentajeDePuntos,
            temp.maximo) AS TEMP2
ORDER BY TEMP2.PorcentajeDePuntos END");

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
