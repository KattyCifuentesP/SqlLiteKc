using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlLiteKc.Modelos
{
    public class Estudiante
    {
        [PrimaryKey, AutoIncrement]
        public int Id { set; get; }

        [MaxLength(255)]
        public string Nombre { set; get; }

        [MaxLength(255)]
        public string Usuario { set; get; }

        [MaxLength(255)]
        public string Contrasena { set; get; }
    }
}
