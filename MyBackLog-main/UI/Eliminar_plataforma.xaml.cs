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
    /// Lógica de interacción para Eliminar_plataforma.xaml
    /// </summary>
    public partial class Eliminar_plataforma : Window
    {
        public Eliminar_plataforma()
        {
            InitializeComponent();
        }

        private void cancelar_eliminar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
