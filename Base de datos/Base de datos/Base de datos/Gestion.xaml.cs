using System;
using System.Collections.Generic;
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
    public partial class Gestion : Window
    {
        MainWindow Main;
        GestionAlumnos GestionAlumnos;
        GestionCursos GestionCursos;
        public Gestion()
        {
            InitializeComponent();
            Main = new MainWindow();
            GestionAlumnos = new GestionAlumnos();
            GestionCursos = new GestionCursos();
        }

        private void GestorProf(object sender, RoutedEventArgs e)
        {
            Main.Show();
        }

        private void GestorAlu(object sender, RoutedEventArgs e)
        {
            GestionAlumnos.Show();
        }

        private void GestorCursos(object sender, RoutedEventArgs e)
        {
            GestionCursos.Show();
        }
    }
}
