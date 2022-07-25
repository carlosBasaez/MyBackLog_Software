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
using System.Text.RegularExpressions;
using Datos;
using Negocio;

namespace UI
{
    /// <summary>
    /// Lógica de interacción para Editar_plataforma.xaml
    /// </summary>
    public partial class Editar_plataforma : Window
    {
        #region Variables
        public int id_editar = -1;
        
        #endregion

        public Editar_plataforma(int id = -1)
        {
            InitializeComponent();

            id_editar = id;
            if (id_editar > -1)
            {
                Datos.Plataforma plataforma = Negocio.PlataformaController.getPlataforma(id_editar);
                txt_id_plataforma.Text = plataforma.Id_plataforma.ToString();
                txt_titulo.Text = plataforma.Titulo;
                txt_descripcion.Text = plataforma.Descripcion;
            }
        }

        private void cancelar_edit_Click(object sender, RoutedEventArgs e)
        {
            Gestionar_plataforma gestionar = new Gestionar_plataforma();
            gestionar.Show();
            this.Close();
        }

        private void aceptar_edit_Click(object sender, RoutedEventArgs e)
        {
            int id_plataforma;
            string titulo_plataforma;
            string descripcion_plataforma;

            if (!int.TryParse(txt_id_plataforma.Text, out id_plataforma))
            {
                MessageBox.Show("Ingrese un id valido");
                return;
            }

            if (txt_titulo.Text == null || txt_titulo.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un titulo valido");
                return;
            }
            else
            {
                titulo_plataforma = txt_titulo.Text;
            }

            if (txt_descripcion.Text == null)
            {
                MessageBox.Show("Hay un problema con la descripcion");
                return;
            }
            else
            {
                descripcion_plataforma = txt_descripcion.Text;
            }

            Datos.Plataforma plataforma = new Datos.Plataforma();
            plataforma.Id_plataforma = id_plataforma;
            plataforma.Titulo = titulo_plataforma;
            plataforma.Descripcion = descripcion_plataforma;

            if (Negocio.PlataformaController.updatePlataforma(plataforma))
            {
                MessageBox.Show("Plataforma actualizada");
                cancelar_edit_Click(sender, e);
            }
            else
            {
                if (!Negocio.PlataformaController.existePlataforma(id_plataforma))
                {
                    MessageBox.Show("No se pudo actualizar la plataforma. El id no coincide con ninguna plataforma actual");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la plataforma");
                }
                return;
            }
        }

        private void txt_id_plataforma_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
