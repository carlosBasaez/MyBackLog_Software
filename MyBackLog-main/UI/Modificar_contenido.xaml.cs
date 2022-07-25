using Datos;
using Negocio;
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
    /// Lógica de interacción para Modificar_contenido.xaml
    /// </summary>
    public partial class Modificar_contenido : Window
    {
        private List<Contenido> Contenidos;
        public List<Plataforma> lista_plataformas;


        public Modificar_contenido()
        {
            InitializeComponent();
            Llenar();
            LlenarPlat();
        }

        private void Llenar()
        {
            info.Items.Clear();
            Contenidos = Negocio.ContenidoController.listarContenido();
            if (Contenidos !=null)
            {
                foreach (var item in Contenidos)
                {
                    info.Items.Add(item);
                }
            }
        }
        private void LlenarPlat()
        {
            lista_plataformas = PlataformaController.listaPlataformas();
            if (lista_plataformas != null) 
            {
                foreach (var item in lista_plataformas)
                {
                    cboPlatFiltro.Items.Add(item.Titulo);
                }
            }
        }

        private void agregar_contenido_Click(object sender, RoutedEventArgs e)
        {
            Gestion_contenido ges = new Gestion_contenido();
            ges.Show();
            this.Close();
        }

        private void deletecontent_Click(object sender, RoutedEventArgs e)
        {
            int select = info.SelectedIndex;
            if (select == -1)
            {
                MessageBox.Show("Seleccione un contenido");
            }
            else
            {
                if (Negocio.ContenidoController.deleteContenido(Contenidos[select].Id_contenido))
                {
                    Console.WriteLine("Eliminar");
                }
            }

            Llenar();
        }

        private void modificar_contenido_Click(object sender, RoutedEventArgs e)
        {
            int select = info.SelectedIndex;
            if (select == -1)
            {
                MessageBox.Show("Se debe seleccionar un contenido", "Aviso", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            
            Gestion_contenido editar = new Gestion_contenido(Contenidos[select].Id_contenido);
            editar.Show();
            this.Close();
        }

        private void plat_Selected(object sender, RoutedEventArgs e)
        {
            Gestionar_plataforma gestionar = new Gestionar_plataforma();
            gestionar.Show();
            this.Close();
        }

        private void filtrado_Click(object sender, RoutedEventArgs e)
        {
            int id_cat = cboCatFiltro.SelectedIndex;
            int plat = 0;
            if (cboPlatFiltro.SelectedIndex > 0) 
            {
                plat = lista_plataformas[cboPlatFiltro.SelectedIndex-1].Id_plataforma;
            }
            int cali = cboEstFiltro.SelectedIndex;
            if (id_cat > 0)
            {
                string table = cboCatFiltro.Text;
                Filtrado(plat, cali, table);
            }
            else 
            {
                Filtrado(plat, cali);
            }
        }
        private void Filtrado(int id_plataforma, int id_calificacion, string tabla = null){
            string where = "";
            if (id_plataforma != 0 || id_calificacion != 0 || tabla != null)
            {
                info.Items.Clear();
                Contenidos = null;
                Console.WriteLine(id_plataforma);
                Console.WriteLine(id_calificacion);
                Console.WriteLine(tabla);

                if (id_calificacion > 0 && id_plataforma == 0)
                {
                    where = $" where calificacion = {id_calificacion}";
                }
                else if (id_plataforma > 0 && id_calificacion == 0)   
                {
                    where = $" where id_plataforma = {id_plataforma}";
                }
                else if(id_plataforma > 0 && id_calificacion > 0)
                {
                    where = $" where  calificacion = {id_calificacion} and id_plataforma = {id_plataforma}";
                }
                if (tabla != "") 
                {
                    Contenidos = ContenidoController.Filtro(where, tabla);
                }
                else
                {
                    Contenidos = ContenidoController.Filtro(where);

                }
                if (Contenidos != null)
                {
                    foreach (var item in Contenidos)
                    {
                        info.Items.Add(item);
                    }
                }
            }
            else
            {
                Llenar();
            }
        }
    }
}
