using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_de_datos
{
    class GestorUsuarios
    {
        public List<Profesor> Profesores { get; set; }
        public List<Alumno> Alumnos { get; set; }

        public GestorUsuarios()
        {
            Profesores = new List<Profesor>();
            Alumnos = new List<Alumno>();
        }
        public string MostrarUsuariosEliminados(Usuario usu)
        {
            string salida = "Lista de usuarios eliminados: \n";
            if (usu is Profesor)
            {
                foreach (Profesor prof in Profesores)
                {
                    salida += prof.Nombre.ToString() + "\n";
                }
            }
            else if(usu is Alumno)
            {
                foreach (Alumno alu in Alumnos)
                {
                    salida += alu.Nombre.ToString() + "\n";
                }
            }
            return salida;
        }
        public bool ComprobarUsuario(string nombre, Usuario usu)
        {
            bool existe = false;
            if (usu is Profesor)
            {
                foreach (Profesor p in Profesores)
                {
                    if (nombre == p.Nombre)
                    {
                        existe = true;
                    }
                }
            }
            else if (usu is Alumno)
            {
                foreach (Alumno a in Alumnos)
                {
                    if (nombre == a.Nombre)
                    {
                        existe = true;
                    }
                }
            }
            return existe;
        }
        public Usuario DevolverUsuario(string nombre, Usuario usu)
        {
            Usuario u = null;
            if (usu is Profesor)
            {
                foreach (Profesor p in Profesores)
                {
                    if (nombre == p.Nombre)
                    {
                        u = p;
                    }
                }
            }
            else if (usu is Alumno)
            {
                foreach (Alumno a in Alumnos)
                {
                    if (nombre == a.Nombre)
                    {
                        u = a;
                    }
                }
            }
            return u;
        }
        public void EliminarDeLista(string nombre, Usuario usu)
        {
            if (usu is Profesor)
            {
                for (int i = 0; i < Profesores.Count; i++)
                {
                    if (nombre == Profesores[i].Nombre)
                    {
                        Profesores.Remove(Profesores[i]);
                    }
                }
            }
            else if(usu is Alumno)
            {
                for (int i = 0; i < Alumnos.Count; i++)
                {
                    if (nombre == Alumnos[i].Nombre)
                    {
                        Alumnos.Remove(Alumnos[i]);
                    }
                }
            }
        }
    }
}
