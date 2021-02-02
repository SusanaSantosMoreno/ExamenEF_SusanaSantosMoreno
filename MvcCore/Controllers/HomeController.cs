using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.Repositories;

namespace MvcCore.Controllers
{
    public class HomeController : Controller {
        IRepositoryCoches repository;

        public HomeController (IRepositoryCoches repo) {
            this.repository = repo;
        }

        public IActionResult Index(){
            List<Coches> coches = this.repository.GetCoches();
            return View(coches);
        }

        [HttpPost]
        public IActionResult Index (String modelo) {
            List<Coches> coches = this.repository.buscarModelo(modelo);
            return View(coches);
        }

        public IActionResult Details (int idCoche) {
            Coches coche = this.repository.GetCoche(idCoche);
            return View(coche);
        }

        public IActionResult Create () {
            return View();
        }

        [HttpPost]
        public IActionResult Create (String Marca, String Modelo, String Conductor, String Imagen) {
            this.repository.AddCoche(Marca, Modelo, Conductor, Imagen);
            return RedirectToAction("Index");
        }

        public IActionResult Update (int idCoche) {
            Coches coche = this.repository.GetCoche(idCoche);
            return View(coche);
        }

        [HttpPost]
        public IActionResult Update (int idCoche, String Marca, String Modelo, String Conductor, String Imagen) {
            this.repository.EditCoche(idCoche, Marca, Modelo, Conductor, Imagen);
            return RedirectToAction("Index");
        }

        public IActionResult Delete (int idCoche, String c) {
            Coches coche = this.repository.GetCoche(idCoche);
            return View(coche);
        }

        [HttpPost]
        public IActionResult Delete (int idCoche) {
            this.repository.DeleteCoche(idCoche);
            return RedirectToAction("Index");
        }
    }
}
