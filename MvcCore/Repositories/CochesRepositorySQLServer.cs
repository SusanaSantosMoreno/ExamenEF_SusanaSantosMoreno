using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region PROCEDURES
/*CREATE PROCEDURE AddCoche (@MARCA NVARCHAR(50), @MODELO NVARCHAR (50), @CONDUCTOR NVARCHAR (50), @IMAGEN NVARCHAR (100)) 
AS
    DECLARE @IDCOCHE INT
	SELECT @IDCOCHE = MAX(idcoche) +1 FROM coches;

INSERT INTO coches VALUES (@IDCOCHE, @MARCA, @MODELO, @CONDUCTOR, @IMAGEN);
GO
*/
#endregion

namespace MvcCore.Repositories {
    public class CochesRepositorySQLServer:IRepositoryCoches {

        CochesContext context;

        public CochesRepositorySQLServer(CochesContext cont) {
            this.context = cont;
        }

        public void AddCoche (string marca, string modelo, string conductor, string imagen) {
            String sql = "AddCoche @MARCA, @MODELO, @CONDUCTOR, @IMAGEN";
            SqlParameter parMarca = new SqlParameter("@MARCA", marca);
            SqlParameter parModelo = new SqlParameter("@MODELO", modelo);
            SqlParameter parConductor = new SqlParameter("@CONDUCTOR", conductor);
            SqlParameter parImagen = new SqlParameter("@IMAGEN", imagen);
            context.Database.ExecuteSqlRaw(sql, parMarca, parModelo, parConductor, parImagen);
        }

        public void DeleteCoche (int idCoche) {
            Coches coche = this.GetCoche(idCoche);
            this.context.coches.Remove(coche);
            this.context.SaveChanges();
        }

        public void EditCoche (int idCoche, string marca, string modelo, string conductor, string imagen) {
            Coches coche = this.GetCoche(idCoche);
            coche.Marca = marca;
            coche.Modelo = modelo;
            coche.Conductor = conductor;
            coche.Imagen = imagen;
            this.context.SaveChanges();
        }

        public Coches GetCoche (int idCoche) {
            return this.context.coches.Where(x => x.IdCoche == idCoche).FirstOrDefault();
        }

        public List<Coches> GetCoches () {
            return this.context.coches.ToList();
        }

        public List<Coches> buscarModelo (string modelo) {
            return this.context.coches.Where(x => x.Modelo.Contains(modelo)).ToList();
        }
    }
}
