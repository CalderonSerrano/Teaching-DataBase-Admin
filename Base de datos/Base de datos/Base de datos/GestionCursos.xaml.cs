using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Base_de_datos
{
    /// <summary>
    /// Lógica de interacción para GestionCursos.xaml
    /// </summary>
    public partial class GestionCursos : Window
    {
        private SqlDataAdapter daDataAdapter;
        private DataSet dsCursos;
        private int numFilas;
        DataRow dr;
        private int contador = 0;
        Gestor gestor = new Gestor();
        Curso c;
        public GestionCursos()
        {
            InitializeComponent();
            string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Instituto.mdf;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            daDataAdapter = new SqlDataAdapter($"SELECT * FROM Cursos", conexion);
            dsCursos = new DataSet();
            daDataAdapter.Fill(dsCursos, "Cursos");
            numFilas = dsCursos.Tables["Cursos"].Rows.Count;

            NextCurso();
            ModificarLabel();
            conexion.Close();
        }
        public void ModificarLabel()
        {
            Label.Content = $"Registro {contador + 1} / {numFilas}";
        }

        private void Anterior(object sender, RoutedEventArgs e)
        {
            ElementoModificado();
            if (contador != 0)
            {
                contador--;
            }
            else
            {
                contador = numFilas - 1;
            }
            NextCurso();
        }

        private void Siguiente(object sender, RoutedEventArgs e)
        {
            ElementoModificado();
            if (contador != numFilas - 1)
            {
                contador++;
            }
            else
            {
                contador = 0;
            }
            NextCurso();
        }

        private void Último(object sender, RoutedEventArgs e)
        {
            ElementoModificado();
            contador = numFilas - 1;
            NextCurso();
        }

        private void Primero(object sender, RoutedEventArgs e)
        {
            ElementoModificado();
            contador = 0;
            NextCurso();
        }

        private void Limpiar(object sender, RoutedEventArgs e)
        {
            ElementoModificado();
            Limpiar();
        }

        private void Anyadir(object sender, RoutedEventArgs e)
        {
            ElementoModificado();
            dr = dsCursos.Tables["Cursos"].NewRow();
            dr["Nombre"] = tbNombre.Text;
            dr["Codigo"] = tbCodigo.Text;
            dsCursos.Tables["Cursos"].Rows.Add(dr);
            numFilas++;
            SqlCommandBuilder cb = new SqlCommandBuilder(daDataAdapter);
            daDataAdapter.Update(dsCursos, "Cursos");
            MessageBox.Show($"El curso {tbNombre.Text} ha sido añadido con éxito.");
            contador = numFilas - 1;
            ModificarLabel();
        }

        private void Modificar(object sender, RoutedEventArgs e)
        {
            if (numFilas > 0)
            {
                dr = dsCursos.Tables["Cursos"].Rows[contador];
                dr["Nombre"] = tbNombre.Text;
                dr["Codigo"] = tbCodigo.Text;
                SqlCommandBuilder cb = new SqlCommandBuilder(daDataAdapter);
                daDataAdapter.Update(dsCursos, "Cursos");
            }
        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {
            if (numFilas > 0)
            {
                MessageBoxResult Resultado;
                Resultado = MessageBox.Show($"¿Seguro que quiere eliminar el curso {tbNombre.Text} de la base de datos?", "Gestor", MessageBoxButton.YesNo);
                if (Resultado == MessageBoxResult.Yes)
                {
                    c = new Curso(tbNombre.Text, tbCodigo.Text);
                    gestor.CursosEliminados.Add(c);
                    MessageBox.Show($"El curso {tbNombre.Text} ha sido borrado con éxito.");
                    dsCursos.Tables["Cursos"].Rows[contador].Delete();
                    numFilas--;
                    SqlCommandBuilder cb = new SqlCommandBuilder(daDataAdapter);
                    daDataAdapter.Update(dsCursos, "Cursos");

                    if (contador > numFilas - 1)
                    {
                        contador = 0;
                    }
                    NextCurso();
                }
            }
            else
            {
                MessageBox.Show("No hay cursos en la base de datos.");
            }
        }

        private void Recuperar(object sender, RoutedEventArgs e)
        {
            if (gestor.CursosEliminados.Count != 0)
            {
                MessageBox.Show(gestor.ListaCursos());
                MessageBoxResult Resultado;
                Resultado = MessageBox.Show("¿Desea recuperar alguno de estos cursos?", "Gestor", MessageBoxButton.YesNo);
                if (Resultado == MessageBoxResult.Yes)
                {
                    string texto = Microsoft.VisualBasic.Interaction.InputBox(
                        "¿Qué curso desea recuperar?",
                        "Gestor");
                    if (gestor.ComprobarCurso(texto))
                    {
                        Curso recuperado = gestor.DevolverCurso(texto);
                        dr = dsCursos.Tables["Cursos"].NewRow();
                        tbNombre.Text = recuperado.Nombre;
                        tbCodigo.Text = recuperado.Codigo;
                        dr["Nombre"] = tbNombre.Text;
                        dr["Codigo"] = tbCodigo.Text;
                        dsCursos.Tables["Cursos"].Rows.Add(dr);
                        numFilas++;
                        SqlCommandBuilder cb = new SqlCommandBuilder(daDataAdapter);
                        daDataAdapter.Update(dsCursos, "Cursos");
                        MessageBox.Show($"El usuario {tbNombre.Text} ha sido recuperado con éxito.");
                        contador = numFilas - 1;
                        gestor.EliminarCurso(texto);
                        ModificarLabel();
                    }
                    else
                    {
                        MessageBox.Show("Curso no encontrado.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay cursos eliminados.");
            }
        }

        private void VerCursos(object sender, RoutedEventArgs e)
        {
            string listaNombres = $"Nombre de los cursos registrados en la base de datos: \n";
            for (int i = 0; i < numFilas; i++)
            {
                dr = dsCursos.Tables["Cursos"].Rows[i];
                listaNombres += $"\n{dr["nombre"]}";
            }
            MessageBox.Show(listaNombres);
        }

        private void BuscarCodigo(object sender, RoutedEventArgs e)
        {
            string texto = Microsoft.VisualBasic.Interaction.InputBox(
                       "Introduzca el código del curso.",
                       "Gestor");
            for (int i = 0; i < numFilas; i++)
            {
                dr = dsCursos.Tables["Cursos"].Rows[i];
                if (dr["Codigo"].ToString() == texto)
                {
                    MessageBox.Show("Curso encontrado.");
                    contador = i;
                    NextCurso();
                }
            }
        }
        private void Limpiar()
        {
            tbNombre.Clear();
            tbCodigo.Clear();
        }
        private void NextCurso()
        {
            if (numFilas > 0)
            {
                if (contador >= 0)
                {
                    dr = dsCursos.Tables["Cursos"].Rows[contador];
                    tbNombre.Text = dr["Nombre"].ToString();
                    tbCodigo.Text = dr["Codigo"].ToString();
                    ModificarLabel();
                }
            }
            else
            {
                Limpiar();
            }
        }
        private void ModificarContenido()
        {
            if (numFilas > 0)
            {
                dr = dsCursos.Tables["Cursos"].Rows[contador];
                dr["Nombre"] = tbNombre.Text;
                dr["Codigo"] = tbCodigo.Text;
                SqlCommandBuilder cb = new SqlCommandBuilder(daDataAdapter);
                daDataAdapter.Update(dsCursos, "Cursos");
            }
        }
        private void ElementoModificado()
        {
            dr = dsCursos.Tables["Cursos"].Rows[contador];
            if (tbNombre.Text != dr["Nombre"].ToString() || tbCodigo.Text != dr["Codigo"].ToString())
            {
                MessageBoxResult Cambio = MessageBox.Show($"¿Desea sobreescribir los datos modificados?", "Gestor", MessageBoxButton.YesNo);
                if (Cambio == MessageBoxResult.Yes)
                {
                    ModificarContenido();
                }
            }
        }
    }
}
