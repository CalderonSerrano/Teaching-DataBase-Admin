using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Base_de_datos
{
    public partial class MainWindow : Window
    {
        int contador = 0;
        DataSet dsProfesores;
        SqlDataAdapter daDataAdapter;
        DataRow dr;
        Gestor gestor;
        string tipo = "Profesores";
        public MainWindow()
        {
            InitializeComponent();
            gestor = new Gestor();
            string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Instituto.mdf;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            daDataAdapter = new SqlDataAdapter($"SELECT * FROM {tipo}", conexion);
            dsProfesores = new DataSet();
            daDataAdapter.Fill(dsProfesores, tipo);
            gestor.numFilas = dsProfesores.Tables[tipo].Rows.Count;
            gestor.NextUsu(Label, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
            conexion.Close();
        }


        private void Anterior(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            if (contador != 0)
            {
                contador--;
            }
            else
            {
                contador = gestor.numFilas - 1;
            }
            gestor.NextUsu(Label, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
        }

        private void Siguiente(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            if (contador != gestor.numFilas - 1)
            {
                contador++;
            }
            else
            {
                contador = 0;
            }
            gestor.NextUsu(Label, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
        }

        private void Último(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            contador = gestor.numFilas - 1;
            gestor.NextUsu(Label, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
        }

        private void Primero(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            contador = 0;
            gestor.NextUsu(Label, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
        }

        private void Limpiar(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            gestor.BorrarTextBox(tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail);
        }

        private void Anyadir(object sender, RoutedEventArgs e)
        {
            gestor.Anyadir(dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo, daDataAdapter);
            gestor.ModificarLabel(Label, contador);
        }

        private void Modificar(object sender, RoutedEventArgs e)
        {
            gestor.ModificarContenido(tipo, dr, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {
            gestor.Eliminar(Label, dsProfesores, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo, daDataAdapter, dr);
        }

        private void Recuperar(object sender, RoutedEventArgs e)
        {
            gestor.Recuperar(dr, dsProfesores, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, contador, Label, tipo, daDataAdapter);
        }

        private void VerRegistrados(object sender, RoutedEventArgs e)
        {
            gestor.Registrados(tipo, dsProfesores, dr);
        }

        private void BuscarApellido(object sender, RoutedEventArgs e)
        {
            gestor.BuscarApellido(Label, dr, contador, dsProfesores, tipo, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail);
        }
    }
}
