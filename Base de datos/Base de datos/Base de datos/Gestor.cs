using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Base_de_datos
{
    class Gestor
    {
        public int numFilas { get; set; }
        private GestorUsuarios gestorUsuario = new GestorUsuarios();
        public List<Curso> CursosEliminados = new List<Curso>();
        Usuario u;
        public string ListaCursos()
        {
            string cursos = "Cursos impartidos: \n \n";
            if (CursosEliminados.Count != 0)
            {
                foreach (Curso c in CursosEliminados)
                {
                    cursos += c.Nombre + "\n";
                }
            }
            return cursos;
        }
        public Curso DevolverCurso(string nombre)
        {
            Curso curso = null;
            foreach (Curso c in CursosEliminados)
            {
                if (nombre == c.Nombre)
                {
                    curso = c;
                }
            }
            return curso;
        }
        public bool ComprobarCurso(string nombre)
        {
            bool existe = false;
            foreach (Curso c in CursosEliminados)
            {
                if (c.Nombre == nombre)
                {
                    existe = true;
                }
            }
            return existe;
        }
        public void EliminarCurso(string nombre)
        {
            for (int i = 0; i < CursosEliminados.Count; i++)
            {
                if (nombre == CursosEliminados[i].Nombre)
                {
                    CursosEliminados.Remove(CursosEliminados[i]);
                }
            }
        }

        public void BorrarTextBox(TextBox dni, TextBox nombre, TextBox apellidos, TextBox telefono, TextBox email)
        {
            dni.Clear();
            nombre.Clear();
            apellidos.Clear();
            telefono.Clear();
            email.Clear();
        }
        public void ModificarLabel(Label Label, int contador)
        {
            Label.Content = $"Registro {contador + 1} / {numFilas}";
        }

        public void Registrados(string tipo, DataSet tipoData, DataRow dr)
        {
            string listaNombres = $"Nombre de los {tipo} registrados en la base de datos: \n";
            for (int i = 0; i < numFilas; i++)
            {
                dr = tipoData.Tables[tipo].Rows[i];
                listaNombres += $"\n{dr["nombre"]}";
            }
            MessageBox.Show(listaNombres);
        }
        public void ModificarContenido(string tipo, DataRow dr, DataSet tipoData, int contador, TextBox dni, TextBox nombre, TextBox apellidos, TextBox telefono, TextBox email, SqlDataAdapter daDataAdapter)
        {
            if (numFilas > 0)
            {
                dr = tipoData.Tables[tipo].Rows[contador];
                dr["DNI"] = dni.Text;
                dr["nombre"] = nombre.Text;
                dr["apellido"] = apellidos.Text;
                dr["tlf"] = telefono.Text;
                dr["email"] = email.Text;
                SqlCommandBuilder cb = new SqlCommandBuilder(daDataAdapter);
                daDataAdapter.Update(tipoData, tipo);
            }
        }
        public void ContModificado(string tipo, DataRow dr, DataSet tipoData, int contador, TextBox dni, TextBox nombre, TextBox apellidos, TextBox telefono, TextBox email, SqlDataAdapter daDataAdapter)
        {
            if (contador >= 0)
            {
                dr = tipoData.Tables[tipo].Rows[contador];
                if (apellidos.Text != dr["apellido"].ToString() || nombre.Text != dr["nombre"].ToString() || email.Text != dr["email"].ToString() || telefono.Text != dr["tlf"].ToString() || dni.Text != dr["DNI"].ToString())
                {
                    MessageBoxResult Cambio = MessageBox.Show($"¿Desea sobreescribir los datos modificados?", "Gestor", MessageBoxButton.YesNo);
                    if (Cambio == MessageBoxResult.Yes)
                    {
                        ModificarContenido(tipo, dr, tipoData, contador, dni, nombre, apellidos, telefono, email, daDataAdapter);
                    }
                }
            }
        }
        public void NextUsu(Label Label, DataRow dr, DataSet tipoData, int contador, TextBox dni, TextBox nombre, TextBox apellidos, TextBox telefono, TextBox email, string tipo)
        {
            if (numFilas > 0)
            {
                if (contador >= 0)
                {
                    dr = tipoData.Tables[tipo].Rows[contador];
                    dni.Text = dr["DNI"].ToString();
                    nombre.Text = dr["nombre"].ToString();
                    apellidos.Text = dr["apellido"].ToString();
                    telefono.Text = dr["tlf"].ToString();
                    email.Text = dr["email"].ToString();
                    ModificarLabel(Label, contador);
                }
            }
            else
            {
                BorrarTextBox(dni, nombre, apellidos, telefono, email);
            }
        }
        public void Anyadir(DataRow dr, DataSet tipoData, int contador, TextBox dni, TextBox nombre, TextBox apellidos, TextBox telefono, TextBox email, string tipo, SqlDataAdapter daDataAdapter)
        {
            dr = tipoData.Tables[tipo].NewRow();
            dr["DNI"] = dni.Text;
            dr["nombre"] = nombre.Text;
            dr["apellido"] = apellidos.Text;
            dr["tlf"] = telefono.Text;
            dr["email"] = email.Text;
            tipoData.Tables[tipo].Rows.Add(dr);
            numFilas++;
            SqlCommandBuilder cb = new SqlCommandBuilder(daDataAdapter);
            daDataAdapter.Update(tipoData, tipo);
            MessageBox.Show($"El usuario {nombre.Text} ha sido añadido con éxito.");
            contador = numFilas - 1;
        }
        public void Eliminar(Label Label, DataSet tipoData, int contador, TextBox dni, TextBox nombre, TextBox apellidos, TextBox telefono, TextBox email, string tipo, SqlDataAdapter daDataAdapter, DataRow dr)
        {
            if (numFilas > 0)
            {
                MessageBoxResult Resultado;
                Resultado = MessageBox.Show($"¿Seguro que quiere eliminar el usuario {nombre.Text} de la base de datos?", "Gestor", MessageBoxButton.YesNo);
                if (Resultado == MessageBoxResult.Yes)
                {
                    if(tipo == "Profesores")
                    {
                        u = new Profesor(dni.Text, nombre.Text, apellidos.Text, telefono.Text, email.Text);
                        gestorUsuario.Profesores.Add((Profesor)u);
                    }
                    else if (tipo == "Alumnos")
                    {
                        u = new Alumno(dni.Text, nombre.Text, apellidos.Text, telefono.Text, email.Text);
                        gestorUsuario.Alumnos.Add((Alumno)u);
                    }
                    MessageBox.Show($"El usuario {nombre.Text} ha sido borrado con éxito.");
                    tipoData.Tables[tipo].Rows[contador].Delete();
                    numFilas--;
                    SqlCommandBuilder cb = new SqlCommandBuilder(daDataAdapter);
                    daDataAdapter.Update(tipoData, tipo);
                    if (contador > numFilas - 1)
                    {
                        contador = 0;
                    }
                    NextUsu(Label, dr, tipoData, contador, dni, nombre, apellidos, telefono, email, tipo);
                }
            }
            else
            {
                MessageBox.Show("No hay usuarios en la base de datos.");
            }
        }
        public void BuscarApellido(Label Label, DataRow dr, int contador, DataSet tipoData, string tipo, TextBox dni, TextBox nombre, TextBox apellidos, TextBox telefono, TextBox email)
        {
            string texto = Microsoft.VisualBasic.Interaction.InputBox(
                       $"Introduzca el apellido del {tipo}.",
                       "Gestor");
            for (int i = 0; i < numFilas; i++)
            {
                dr = tipoData.Tables[tipo].Rows[i];
                if (dr["apellido"].ToString() == texto)
                {
                    MessageBox.Show($"{tipo} encontrado.");
                    contador = i;
                    NextUsu(Label, dr, tipoData, contador, dni, nombre, apellidos, telefono, email, tipo);
                }
            }
        }

        public void Recuperar(DataRow dr, DataSet tipoData, TextBox dni, TextBox nombre, TextBox apellidos, TextBox telefono, TextBox email, int contador, Label Label, string tipo, SqlDataAdapter daDataAdapter)
        {
            if (gestorUsuario.Profesores.Count != 0 || gestorUsuario.Alumnos.Count != 0)
            {
                MessageBox.Show(gestorUsuario.MostrarUsuariosEliminados(u));
                MessageBoxResult Resultado;
                Resultado = MessageBox.Show($"¿Desea recuperar alguno de estos usuarios?", "Gestor", MessageBoxButton.YesNo);
                if (Resultado == MessageBoxResult.Yes)
                {
                    string texto = Microsoft.VisualBasic.Interaction.InputBox(
                        "¿Qué usuario desea recuperar?",
                        "Gestor");
                    if (gestorUsuario.ComprobarUsuario(texto, u))
                    {
                        Usuario recuperado = gestorUsuario.DevolverUsuario(texto, u);
                        dr = tipoData.Tables[tipo].NewRow();
                        dni.Text = recuperado.Dni;
                        nombre.Text = recuperado.Nombre;
                        apellidos.Text = recuperado.Apellidos;
                        telefono.Text = recuperado.Telefono;
                        email.Text = recuperado.Email;
                        dr["DNI"] = dni.Text;
                        dr["nombre"] = nombre.Text;
                        dr["apellido"] = apellidos.Text;
                        dr["tlf"] = telefono.Text;
                        dr["email"] = email.Text;
                        tipoData.Tables[tipo].Rows.Add(dr);
                        numFilas++;
                        SqlCommandBuilder cb = new SqlCommandBuilder(daDataAdapter);
                        daDataAdapter.Update(tipoData, tipo);
                        MessageBox.Show($"El usuario {nombre.Text} ha sido recuperado con éxito.");
                        contador = numFilas - 1;
                        gestorUsuario.EliminarDeLista(texto, u);
                        ModificarLabel(Label, contador);
                    }
                    else
                    {
                        MessageBox.Show("Usuario no encontrado.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay usuarios eliminados.");
            }
        }
    }
}
