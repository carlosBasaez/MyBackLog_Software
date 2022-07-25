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

namespace UI
{
    /// <summary>
    /// Lógica de interacción para Agregar_plataforma.xaml
    /// </summary>
    public partial class Agregar_plataforma : Window
    {
        public Agregar_plataforma()
        {
            InitializeComponent();
        }

        private void Cancelar_add_Click(object sender, RoutedEventArgs e)
        {
            Gestionar_plataforma gestionar = new Gestionar_plataforma();
            gestionar.Show();
            this.Close();
        }

        private void aceptar_add_Click(object sender, RoutedEventArgs e)
        {
            //Comprobaciones
            if (titulo_plataforma_add == null || titulo_plataforma_add.Text.Length == 0)
            {
                MessageBox.Show("Ingrese titulo");
                return;
            }

            if (descripcion_plataforma_add == null || descripcion_plataforma_add.Text.Length == 0)
            {
                descripcion_plataforma_add.Text = "";
            }

            //Cargar
            Datos.Plataforma plataforma = new Datos.Plataforma
            {
                Titulo = titulo_plataforma_add.Text,
                Descripcion = descripcion_plataforma_add.Text
            };
            if (Negocio.PlataformaController.insertarPlataforma(plataforma))
            {
                MessageBox.Show("Insertado con éxtio");
            }
            else
            {
                MessageBox.Show("Error");
            }

            Cancelar_add_Click(sender, e);
        }
    }
}
