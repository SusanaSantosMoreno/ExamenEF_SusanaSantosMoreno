using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories {
    public class CochesRepositoryMysql : IRepositoryCoches {
        CochesContext context;

        public CochesRepositoryMysql (CochesContext cont) {
            this.context = cont;
        }

        public void AddCoche (string marca, string modelo, string conductor, string imagen) {
            int idCoche = this.context.coches.OrderByDescending(x => x.IdCoche).FirstOrDefault().IdCoche;
            Coches coche = new Coches((idCoche+1), marca, modelo, conductor, imagen);
            this.context.coches.Add(coche);
            this.context.SaveChanges();
        }

        public List<Coches> buscarModelo (string modelo) {
            return this.context.coches.Where(x => x.Modelo.Contains(modelo)).ToList();
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
    }
}
