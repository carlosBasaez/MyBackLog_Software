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
    /// Lógica de interacción para Gestionar_plataforma.xaml
    /// </summary>
    public partial class Gestionar_plataforma : Window
    {
        #region Variables
        List<Datos.Plataforma> ListPlataforma;
        #endregion


        public Gestionar_plataforma()
        {
            InitializeComponent();
            Listar();
        }

        private void Listar()
        {
            grid_datos.Items.Clear();
            ListPlataforma = Negocio.PlataformaController.listaPlataformas();
            if (ListPlataforma != null)
            {                
                foreach (var item in ListPlataforma)
                {
                    grid_datos.Items.Add(item);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Agregar_plataforma add_plataforma = new Agregar_plataforma();
            add_plataforma.Show();
            this.Close();
        }

        private void editplataforma_Click(object sender, RoutedEventArgs e)
        {
            int index = grid_datos.SelectedIndex;
            int id = index > -1 ? ListPlataforma[index].Id_plataforma : -1;
            Editar_plataforma editar_Plataforma = new Editar_plataforma(id);
            editar_Plataforma.Show();
            this.Close();
        }

        private void deleteplataforma_Click(object sender, RoutedEventArgs e)
        {
            //Eliminar_plataforma del = new Eliminar_plataforma();
            //del.Show();

            int select = grid_datos.SelectedIndex;

            if (select == -1)
            {
                MessageBox.Show("Seleccione una plataforma");
                return;
            }

            Negocio.PlataformaController.eliminarPlataforma(ListPlataforma[select].Id_plataforma);

            Listar();
        }

        private void contenido_Selected(object sender, RoutedEventArgs e)
        {
            Modificar_contenido modificar = new Modificar_contenido();
            modificar.Show();
            this.Close();
        }
    }

}