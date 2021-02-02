using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories {
    public interface IRepositoryCoches {

        public List<Coches> GetCoches ();
        public Coches GetCoche (int idCoche);
        public void AddCoche (String marca, String modelo, String conductor, String imagen);

        public void EditCoche (int idCoche, String marca, String modelo, String conductor, String imagen);
        public void DeleteCoche (int idCoche);

        public List<Coches> buscarModelo (String modelo);
    }
}
