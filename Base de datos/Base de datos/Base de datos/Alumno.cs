using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_de_datos
{
    class Alumno : Usuario
    {
        public Alumno(string dni, string nombre, string apellidos, string telefono, string email) : base(dni, nombre, apellidos, telefono, email)
        {

        }
    }
}
