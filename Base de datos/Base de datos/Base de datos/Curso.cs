using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_de_datos
{
    class Curso
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public Curso (string nombre, string codigo)
        {
            Nombre = nombre;
            Codigo = codigo;
        }
    }
}
