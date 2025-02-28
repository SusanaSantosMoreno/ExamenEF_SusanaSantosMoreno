﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models {
    
    [Table("coches")]
    public class Coches {
        
         [Key]
         [Column("idcoche")]
         public int IdCoche { get; set; }
        [Column("marca")]
        public String Marca { get; set; }
        [Column("modelo")]
        public String Modelo { get; set; }
        [Column("conductor")]
        public String Conductor { get; set; }
        [Column("imagen")]
        public String Imagen { get; set; }

        public Coches () {}

        public Coches (int idCoche, string marca, string modelo, 
            string conductor, String imagen) {
            IdCoche = idCoche;
            Marca = marca;
            Modelo = modelo;
            Conductor = conductor;
            Imagen = imagen;
        }
    }
}
