using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_de_datos
{
    class Usuario
    {
        public GestorUsuarios g { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public Usuario (string dni, string nombre, string apellidos, string telefono, string email)
        {
            Dni = dni;
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Email = email;
        }
    }
}
