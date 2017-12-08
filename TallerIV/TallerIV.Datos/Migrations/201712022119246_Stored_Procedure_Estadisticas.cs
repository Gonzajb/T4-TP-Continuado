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
SELECT
  PORCENTAJEDEPUNTOS,
  CANTIDAD,
  CAST (CAST(CANTIDAD AS FLOAT) / CAST (TOTALUSUARIOS AS FLOAT) AS DECIMAL (10, 1))*100 AS PORCENTAJEDEUSUARIOS,
  MAXIMOAVISO 
FROM
  (
    SELECT
      PORCENTAJEDEPUNTOS,
      COUNT(1) AS CANTIDAD,
      SUM(COUNT(1)) OVER () AS TOTALUSUARIOS,
      MAXIMO AS MAXIMOAVISO 
    FROM
      (
        SELECT
          CAST(CAST(SUM(AP.PRIORIDAD) AS FLOAT) / CAST(MAXIMO AS FLOAT) AS DECIMAL(10, 1))*100 AS PORCENTAJEDEPUNTOS,
          MAXIMO,
          UEA.USUARIOEMPLEADO_ID 
        FROM
          (
            SELECT
              SUM(AP1.PRIORIDAD) AS MAXIMO,
              AP1.AVISO_ID 
            FROM
              APTITUDESPORAVISO AP1 
            WHERE
              AP1.AVISO_ID = @avisoId 
            GROUP BY
              AP1.AVISO_ID
          )
          A1 
          INNER JOIN
            APTITUDESPORAVISO AP 
            ON AP.AVISO_ID = A1.AVISO_ID 
          INNER JOIN
            USUARIOEMPLEADOAPTITUD UEA 
            ON AP.APTITUD_ID = UEA.APTITUD_ID 
        WHERE
          AP.AVISO_ID = @avisoId 
          AND UEA.USUARIOEMPLEADO_ID IN 
          (
            SELECT
              U.ID 
            FROM
              BUSQUEDAUSUARIOPOSTULANTE BUP 
              INNER JOIN
                USUARIOS U 
                ON U.BUSQUEDA_ID = BUP.ID 
              INNER JOIN
                (
                  SELECT
                    CASE
                      AV.SUELDOOFRECIDOPRIORIDAD 
                      WHEN
                        3 
                      THEN
                        AV.SUELDOOFRECIDO 
                      ELSE
                        9999999999 
                    END
                    AS SUELDOOFRECIDO1, 
                    CASE
                      AV.HORASTRABAJOPRIORIDAD 
                      WHEN
                        3 
                      THEN
                        AV.HORASTRABAJO 
                      ELSE
                        - 1 
                    END
                    AS HORASTRABAJO1, 
                    CASE
                      AV.TIPORELACIONDETRABAJOPRIORIDAD 
                      WHEN
                        3 
                      THEN
                        AV.TIPORELACIONDETRABAJO 
                      ELSE
                        - 1 
                    END
                    AS TIPORELACIONDETRABAJO1 
                  FROM
                    AVISOS AV 
                  WHERE
                    AV.ID = @avisoId
                )
                TEMPBUSQUEDA 
                ON TEMPBUSQUEDA.HORASTRABAJO1 <= BUP.HORASTRABAJO 
                AND TEMPBUSQUEDA.SUELDOOFRECIDO1 >= BUP.SUELDOMINIMO 
                AND TEMPBUSQUEDA.TIPORELACIONDETRABAJO1 <= BUP.TIPORELACIONDETRABAJO
          )
        GROUP BY
          USUARIOEMPLEADO_ID, MAXIMO
      )
      AS TEMP 
    WHERE
      TEMP.USUARIOEMPLEADO_ID IN 
      (
        SELECT
          OTROTEMP.USUARIOEMPLEADO_ID 
        FROM
          (
            SELECT
              USAPT.USUARIOEMPLEADO_ID,
              COUNT (USAPT.USUARIOEMPLEADO_ID) AS CONTANDO 
            FROM
              USUARIOEMPLEADOAPTITUD USAPT 
              INNER JOIN
                APTITUDESPORAVISO APAV 
                ON APAV.APTITUD_ID = USAPT.APTITUD_ID 
              LEFT JOIN
                (
                  SELECT
                    COUNT(*) AS CONT,
                    APA3.AVISO_ID 
                  FROM
                    APTITUDESPORAVISO APA3 
                  WHERE
                    APA3.AVISO_ID = @avisoId 
                    AND APA3.PRIORIDAD = 3 
                  GROUP BY
                    APA3.AVISO_ID
                )
                TEMP 
                ON TEMP.AVISO_ID = APAV.AVISO_ID 
            WHERE
              APAV.AVISO_ID = @avisoId 
              AND 
              (
                APAV.PRIORIDAD = 3 
                OR TEMP.CONT IS NULL
              )
            GROUP BY
              USAPT.USUARIOEMPLEADO_ID 
            HAVING
(
              SELECT
                COUNT(*) 
              FROM
                APTITUDESPORAVISO APAV2 
              WHERE
                APAV2.AVISO_ID = @avisoId 
                AND APAV2.PRIORIDAD = 3) = COUNT(USAPT.USUARIOEMPLEADO_ID) 
                OR 
                (
                  SELECT
                    COUNT(*) 
                  FROM
                    APTITUDESPORAVISO APAV2 
                  WHERE
                    APAV2.AVISO_ID = @avisoId 
                    AND APAV2.PRIORIDAD = 3
                )
                = 0
          )
          AS OTROTEMP
      )
    GROUP BY
      TEMP.PORCENTAJEDEPUNTOS,
      TEMP.MAXIMO
  )
  AS TEMP2 
ORDER BY
  TEMP2.PORCENTAJEDEPUNTOS END");

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
