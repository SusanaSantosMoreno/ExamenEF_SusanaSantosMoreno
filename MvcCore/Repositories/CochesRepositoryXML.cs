using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Repositories {
    public class CochesRepositoryXML : IRepositoryCoches {

        PathProvider provider;
        private XDocument docXML;
        private String path;

        public CochesRepositoryXML(PathProvider prov) {
            this.provider = prov;
            this.path = this.provider.MapPath("coches.xml", Folders.Documents);
            this.docXML = XDocument.Load(this.path);
        }

        public void AddCoche (string marca, string modelo, string conductor, string imagen) {
            int idCoche = this.GetCoches().OrderByDescending(x => x.IdCoche).FirstOrDefault().IdCoche;
            XElement elementCoche = new XElement("coche");
            elementCoche.Add(new XElement("idcoche", (idCoche+1)));
            elementCoche.Add(new XElement("marca", marca));
            elementCoche.Add(new XElement("modelo", modelo));
            elementCoche.Add(new XElement("conductor", conductor));
            elementCoche.Add(new XElement("imagen", imagen));

            this.docXML.Element("coches").Add(elementCoche);
            this.docXML.Save(this.path);
        }

        public void DeleteCoche (int idCoche) {
            var consulta = from datos in this.docXML.Descendants("coche")
                           where datos.Element("idcoche").Value == idCoche.ToString()
                           select datos;
            XElement elementCoche = consulta.FirstOrDefault();
            elementCoche.Remove();
            this.docXML.Save(this.path);
        }

        public void EditCoche (int idCoche, string marca, string modelo, string conductor, string imagen) {
            var consulta = from datos in this.docXML.Descendants("coche")
                           where datos.Element("idcoche").Value == idCoche.ToString()
                           select datos;
            XElement elementCoche = consulta.FirstOrDefault();
            elementCoche.Element("marca").Value = marca;
            elementCoche.Element("modelo").Value = modelo;
            elementCoche.Element("conductor").Value = conductor;
            elementCoche.Element("imagen").Value = imagen;
            this.docXML.Save(this.path);
        }

        public Coches GetCoche (int idCoche) {
            var consulta = from datos in this.docXML.Descendants("coche")
                           where datos.Element("idcoche").Value == idCoche.ToString()
                           select new Coches(Convert.ToInt32(datos.Element("idcoche").Value),
                                datos.Element("marca").Value, datos.Element("modelo").Value,
                                 datos.Element("conductor").Value, datos.Element("imagen").Value);
            return consulta.FirstOrDefault();
        }

        public List<Coches> GetCoches () {
            var consulta = from datos in this.docXML.Descendants("coche")
                           select new Coches(Convert.ToInt32(datos.Element("idcoche").Value),
                                datos.Element("marca").Value, datos.Element("modelo").Value,
                                 datos.Element("conductor").Value, datos.Element("imagen").Value);
            return consulta.ToList();
        }
        public List<Coches> buscarModelo (string modelo) {
            return this.GetCoches().Where(x => x.Modelo.ToLower().Contains(modelo) || x.Modelo.Contains(modelo)).ToList();
        }

    }
}
