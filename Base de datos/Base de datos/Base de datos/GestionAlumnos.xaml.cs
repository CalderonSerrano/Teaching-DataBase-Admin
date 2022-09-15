using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Base_de_datos
{
    /// <summary>
    /// Lógica de interacción para GestionAlumnos.xaml
    /// </summary>
    public partial class GestionAlumnos : Window
    {
        int contador = 0;
        DataSet dsAlumnos;
        SqlDataAdapter daDataAdapter;
        DataRow dr;
        Gestor gestor;
        string tipo = "Alumnos";
        public GestionAlumnos()
        {
            InitializeComponent();
            gestor = new Gestor();
            string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Instituto.mdf;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            daDataAdapter = new SqlDataAdapter($"SELECT * FROM {tipo}", conexion);
            dsAlumnos = new DataSet();
            daDataAdapter.Fill(dsAlumnos, tipo);
            gestor.numFilas = dsAlumnos.Tables[tipo].Rows.Count;
            gestor.NextUsu(Label, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
            conexion.Close();
        }
        private void Anterior(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            if (contador != 0)
            {
                contador--;
            }
            else
            {
                contador = gestor.numFilas - 1;
            }
            gestor.NextUsu(Label, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
        }

        private void Siguiente(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            if (contador != gestor.numFilas - 1)
            {
                contador++;
            }
            else
            {
                contador = 0;
            }
            gestor.NextUsu(Label, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
        }

        private void Último(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            contador = gestor.numFilas - 1;
            gestor.NextUsu(Label, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
        }

        private void Primero(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            contador = 0;
            gestor.NextUsu(Label, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo);
        }

        private void Limpiar(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            gestor.BorrarTextBox(tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail);
        }

        private void Anyadir(object sender, RoutedEventArgs e)
        {
            gestor.ContModificado(tipo, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
            gestor.Anyadir( dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo, daDataAdapter);
            gestor.ModificarLabel(Label, contador);
        }

        private void Modificar(object sender, RoutedEventArgs e)
        {
            gestor.ModificarContenido(tipo, dr, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, daDataAdapter);
        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {
            gestor.Eliminar(Label, dsAlumnos, contador, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, tipo, daDataAdapter, dr);
        }

        private void Recuperar(object sender, RoutedEventArgs e)
        {
            gestor.Recuperar(dr, dsAlumnos, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail, contador, Label, tipo, daDataAdapter);
        }

        private void VerRegistrados(object sender, RoutedEventArgs e)
        {
            gestor.Registrados(tipo, dsAlumnos, dr);
        }

        private void BuscarApellido(object sender, RoutedEventArgs e)
        {
            gestor.BuscarApellido(Label, dr, contador, dsAlumnos, tipo, tbDni, tbNombre, tbApellidos, tbTelefono, tbEmail);
        }
    }
}
