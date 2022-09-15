using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_de_datos
{
    class Profesor : Usuario
    {
        public Profesor(string dni, string nombre, string apellidos, string telefono, string email) : base (dni, nombre, apellidos, telefono, email)
        {

        }
    }
}
