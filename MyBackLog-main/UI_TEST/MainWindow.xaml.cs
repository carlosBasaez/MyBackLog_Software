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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Negocio;
using Datos;

namespace UI_TEST
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Nota> lista_notas;
        public List<Nota> eliminados_notas;
        public List<NotaTabla> lista_notaTabla;
        
        public class NotaTabla
        {
            private string descripcion;
            private bool completado;

            public string Descripcion { get => descripcion; set => descripcion = value; }
            public bool Completado { get => completado; set => completado = value; }
        }
        
        
        
        public MainWindow()
        {
            InitializeComponent();
            Preparativos();
        }
        
        private void Preparativos()
        {
            CMD("Comenzado Preparativo");

            lista_notas = NotaController.listar(1);
            lista_notaTabla = new List<NotaTabla>();
            eliminados_notas = new List<Nota>();

            foreach (var item in lista_notas)
            {
                NotaTabla nt = new NotaTabla();
                nt.Descripcion = item.Descripcion;
                nt.Completado = item.Completado;
                lista_notaTabla.Add(nt);
            }

            tabla.ItemsSource = lista_notaTabla;
            
            CMD("Preparativos Listos");
        }

        private void ActualizarLista()
        {
            tabla.Items.Refresh();
            CMD("Actualizada la lista");
        }

        

        private void CMD(string mensaje)
        {
            string actual = cmd.Text;

            actual = ">>> " + mensaje + "\n" + actual;

            cmd.Text = actual;
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Nota nota = new Nota();
            nota.Id_nota = -1;
            nota.Id_contenido = -1;
            nota.Descripcion = "";
            nota.Completado = false;

            NotaTabla nt = new NotaTabla();
            nt.Descripcion = "";
            nt.Completado = false;

            lista_notas.Add(nota);
            lista_notaTabla.Add(nt);

            CMD("Agregado Nota");
            ActualizarLista();
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            int index = tabla.SelectedIndex;
            if (index < 0) return;

            eliminados_notas.Add(lista_notas[index]);

            lista_notaTabla.RemoveAt(index);
            lista_notas.RemoveAt(index);

            CMD("Eliminado Nota");
            ActualizarLista();
        }

        private void tabla_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CMD("columnas: " + tabla.Columns.Count);
            tabla.Columns[0].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
            tabla.Columns[1].Width = new DataGridLength(0.2, DataGridLengthUnitType.Star);
        }
    }
}
