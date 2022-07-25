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
using Negocio;
using Datos;

namespace UI
{
    /// <summary>
    /// Lógica de interacción para Gestion_contenido.xaml
    /// </summary>
    public partial class Gestion_contenido : Window
    {

        #region Variables
        public int id_contenido = -1;
        public Modo modo = Modo.create;
        public Contenido contenido;
        public Libro libro;
        public Pelicula pelicula;
        public Juego juego;
        public Serie serie;
        public List<Plataforma> lista_plataformas;
        public List<Progresion> lista_progresion;
        public List<Adquisicion> lista_adquisiciones;

        public enum Modo 
        {
            create, 
            update 
        }
        #endregion

        public Gestion_contenido(int id_cont = -1)
        {
            InitializeComponent();
            
            CargarComboBox();
            
            if (id_cont > -1) //Update?
            {
                //Configurar Update
                modo = Modo.update;
                id_contenido = id_cont;
                CargarDatosUpdate();
            }

            JuegoPreparativos();
            
            if (modo == Modo.create)
                Titulo.Content = "Crear Nuevo Contenido";
            if (modo == Modo.update)
                Titulo.Content = "Actualizar Contenido";
        }

        private void salir_editar_Click(object sender, RoutedEventArgs e)
        {
            CerrarVentana();
        }

        private void CargarComboBox()
        {
            //Conseguir las listas de la base de datos
            lista_plataformas = PlataformaController.listaPlataformas();
            lista_progresion = ProgresionController.listarProgresiones();
            lista_adquisiciones = AdquisicionController.listaAdquisiciones();

            //Cargar listas en ComboBoxes
            //Cargar Plataformas
            ComboBox_Plataforma.Items.Clear();
            foreach (var item in lista_plataformas)
            {
                ComboBox_Plataforma.Items.Add(item.Titulo);
            }

            //Cargar Progresion
            ComboBox_Progresion.Items.Clear();
            foreach (var item in lista_progresion)
            {
                ComboBox_Progresion.Items.Add(item.Nombre_estado);
            }

            //Cargar Adquisicion
            ComboBox_Adquisicion.Items.Clear();
            foreach (var item in lista_adquisiciones)
            {
                ComboBox_Adquisicion.Items.Add(item.Nombre_adquision);
            }
        }

        private void CargarDatosUpdate()
        {
            //Conseguir datos contenido
            contenido = ContenidoController.getContenido(id_contenido);

            Txt_Titulo.Text = contenido.Titulo;
            Txt_Descripcion.Text = contenido.Descripcion;
            Txt_Calificacion.Text = contenido.Calificacion.ToString();
            Txt_Horas_Inversion.Text = contenido.Horas_inversion.ToString();
            ComboBox_Plataforma.SelectedIndex = lista_plataformas.FindIndex(x => x.Id_plataforma == contenido.Id_plataforma);
            ComboBox_Progresion.SelectedIndex = lista_progresion.FindIndex(x => x.Id_progresion == contenido.Id_progresion);
            ComboBox_Adquisicion.SelectedIndex = lista_adquisiciones.FindIndex(x => x.Id_adquisicion == contenido.Id_adquisicion);


            //Cargar Contenido Libro
            libro = LibroController.getLibro(contenido.Id_contenido);
            if (libro != null)
            {
                Txt_Libro_CantidadPaginas.Text = libro.Cantidad_paginas.ToString();
                Txt_Libro_PaginaActual.Text = libro.Pagina.ToString();
                subtipo_cbo.SelectedIndex = 0;
            }

            //Cargar Contenido Pelicula
            pelicula = PeliculaController.getPelicula(contenido.Id_contenido);
            if (pelicula != null)
            {
                Txt_Pelicula_Duracion.Text = pelicula.Duracion_minutos.ToString();
                Txt_Pelicula_Minuto.Text = pelicula.Minuto.ToString();
                subtipo_cbo.SelectedIndex = 1;
            }

            //Cargar Contenido Serie

            serie = SerieController.getSerie(contenido.Id_contenido);
            if (serie != null)
            {

                Txt_Tiempo_Capitulos.Text = serie.Tiempo_capitulo.ToString();
                Txt_Capitulos_Temporada.Text = serie.Capitulos_temporada.ToString();
                Txt_Temporada.Text = serie.Temporada.ToString();
                Txt_Capitulo.Text = serie.Capitulo.ToString();
                Txt_MinutoSerie.Text = serie.Minuto.ToString();
                subtipo_cbo.SelectedIndex = 2;
            }

            //Cargar Contenido Juego
            juego = JuegoController.get(contenido.Id_contenido);
            if (juego != null)
            {
                subtipo_cbo.SelectedIndex = 3;
                JuegoPreparativos();
            }
        }

        private void subtipo_cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indice = subtipo_cbo.SelectedIndex;
            if (indice == 1)
            {
                pelicula_grid.Visibility = Visibility.Visible;
                grid_juego.Visibility = Visibility.Collapsed;
                grid_serie.Visibility = Visibility.Collapsed;
                grid_libro.Visibility = Visibility.Collapsed;
            }
            if (indice == 3)
            {
                pelicula_grid.Visibility = Visibility.Collapsed;
                grid_juego.Visibility = Visibility.Visible;
                grid_serie.Visibility = Visibility.Collapsed;
                grid_libro.Visibility = Visibility.Collapsed;
            }
            if (indice == 2)
            {
                pelicula_grid.Visibility = Visibility.Collapsed;
                grid_juego.Visibility = Visibility.Collapsed;
                grid_serie.Visibility = Visibility.Visible;
                grid_libro.Visibility = Visibility.Collapsed;
            }
            if (indice == 0)
            {
                pelicula_grid.Visibility = Visibility.Collapsed;
                grid_juego.Visibility = Visibility.Collapsed;
                grid_serie.Visibility = Visibility.Collapsed;
                grid_libro.Visibility = Visibility.Visible;
            }
        }

        private void aceptar_editar_Click(object sender, RoutedEventArgs e)
        {
            //Crear Contenido
            if (modo == Modo.create)
            {
                contenido = new Contenido();

                #region TextBox
                contenido.Titulo = Txt_Titulo.Text;
                contenido.Descripcion = Txt_Descripcion.Text;
                contenido.Calificacion = int.Parse(Txt_Calificacion.Text);
                contenido.Horas_inversion = int.Parse(Txt_Horas_Inversion.Text);
                #endregion

                #region ComboBox
                if (ComboBox_Plataforma.SelectedIndex != -1)
                {
                    contenido.Id_plataforma = lista_plataformas[ComboBox_Plataforma.SelectedIndex].Id_plataforma;
                }
                else
                {
                    MessageBox.Show("Se debe seleccionar una plataforma", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (ComboBox_Progresion.SelectedIndex != -1)
                {
                    contenido.Id_progresion = lista_progresion[ComboBox_Progresion.SelectedIndex].Id_progresion;
                }
                else
                {
                    MessageBox.Show("Se debe seleccionar una progresion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (ComboBox_Adquisicion.SelectedIndex != -1)
                {
                    contenido.Id_adquisicion = lista_adquisiciones[ComboBox_Adquisicion.SelectedIndex].Id_adquisicion;
                }
                else
                {
                    MessageBox.Show("Se debe seleccionar una adquisicion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                #endregion

                //Subtipos
                if (subtipo_cbo.SelectedIndex == -1)
                {
                    MessageBox.Show("Se debe seleccionar un subtipo");
                    return;
                }
                libro = null;
                pelicula = null;
                serie = null;
                juego = null;

                #region Libro
                if (subtipo_cbo.SelectedIndex == 0)
                {
                    libro = new Libro();
                    libro.Cantidad_paginas = int.Parse(Txt_Libro_CantidadPaginas.Text);
                    libro.Pagina = int.Parse(Txt_Libro_PaginaActual.Text);
                }
                #endregion

                #region Pelicula
                if (subtipo_cbo.SelectedIndex == 1)
                {
                    pelicula = new Pelicula();
                    pelicula.Duracion_minutos = int.Parse(Txt_Pelicula_Duracion.Text);
                    pelicula.Minuto = int.Parse(Txt_Pelicula_Minuto.Text);
                }
                #endregion

                #region Serie
                if (subtipo_cbo.SelectedIndex == 2)
                {
                    serie = new Serie();
                    serie.Tiempo_capitulo = int.Parse(Txt_Tiempo_Capitulos.Text);
                    serie.Capitulos_temporada = int.Parse(Txt_Capitulos_Temporada.Text);
                    serie.Temporada = int.Parse(Txt_Temporada.Text);
                    serie.Capitulo = int.Parse(Txt_Capitulo.Text);
                    serie.Minuto = int.Parse(Txt_MinutoSerie.Text);
                }
                #endregion

                #region Juego
                if (subtipo_cbo.SelectedIndex == 3)
                {
                    juego = new Juego();
                    
                }
                #endregion

                #region Cargar contenido
                if (ContenidoController.insertContenido(contenido))
                {
                    MessageBox.Show("Insercion de Contenido correcto");
                }
                else
                {
                    MessageBox.Show("Insercion de Contenido no ha funcionado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int id_contenido = ContenidoController.LastID();

                bool correcto_subtipo = false;
    
                

                //Libro
                if (libro != null)
                {
                    libro.Id_contenido = id_contenido;
                    if (correcto_subtipo = LibroController.insertLibro(libro))
                    {
                        MessageBox.Show("Ingresado correctamente Libro");
                    }
                    else
                    {
                        MessageBox.Show("Libro no se ha ingresado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                //Pelicula
                if (pelicula != null)
                {
                    pelicula.Id_contenido = id_contenido;
                    if (correcto_subtipo = PeliculaController.insertPelicula(pelicula))
                    {
                        MessageBox.Show("Ingresado correctamente Pelicula");
                    }
                    else
                    {
                        MessageBox.Show("Pelicula no se ha ingresado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                //Serie
                if (serie != null)
                {
                    serie.Id_contenido = id_contenido;
                    if (correcto_subtipo = SerieController.insertSerie(serie))
                    {
                        MessageBox.Show("Serie ingresada correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Serie no se ha ingresado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    
                }

                //Juego
                if (juego != null)
                {
                    juego.Id_contenido = id_contenido;
                    if (correcto_subtipo = JuegoController.insert(juego))
                    {
                        MessageBox.Show("Ingresado correctamente Juego");
                        correcto_subtipo = NotasModificar(id_contenido);
                    }
                    else
                    {
                        MessageBox.Show("Juego no se ha ingresado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                if (!correcto_subtipo)
                {
                    if (ContenidoController.deleteContenido(id_contenido))
                    {
                        MessageBox.Show("Eliminacion de contenido de emergencia: Exitoso");
                    }
                    else
                    {
                        MessageBox.Show("Eliminacion de contenido de emergencia: Fallo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    return;
                }

                #endregion


            }

            //Modificar Contenido
            if (modo == Modo.update)
            {
                contenido = new Contenido();

                #region TextBox
                contenido.Id_contenido = id_contenido;
                contenido.Titulo = Txt_Titulo.Text;
                contenido.Descripcion = Txt_Descripcion.Text;
                contenido.Calificacion = int.Parse(Txt_Calificacion.Text);
                contenido.Horas_inversion = int.Parse(Txt_Horas_Inversion.Text);
                #endregion

                #region ComboBox
                if (ComboBox_Plataforma.SelectedIndex != -1)
                {
                    contenido.Id_plataforma = lista_plataformas[ComboBox_Plataforma.SelectedIndex].Id_plataforma;
                }
                else
                {
                    MessageBox.Show("Se debe seleccionar una plataforma", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (ComboBox_Progresion.SelectedIndex != -1)
                {
                    contenido.Id_progresion = lista_progresion[ComboBox_Progresion.SelectedIndex].Id_progresion;
                }
                else
                {
                    MessageBox.Show("Se debe seleccionar una progresion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (ComboBox_Adquisicion.SelectedIndex != -1)
                {
                    contenido.Id_adquisicion = lista_adquisiciones[ComboBox_Adquisicion.SelectedIndex].Id_adquisicion;
                }
                else
                {
                    MessageBox.Show("Se debe seleccionar una adquisicion", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                #endregion

                //Subtipos
                if (subtipo_cbo.SelectedIndex == -1)
                {
                    MessageBox.Show("Se debe seleccionar un subtipo");
                    return;
                }
                libro = null;
                pelicula = null;
                serie = null;
                juego = null;
                bool correcto_subtipo = false;

                #region Libro
                if (subtipo_cbo.SelectedIndex == 0)
                {
                    libro = new Libro();
                    libro.Cantidad_paginas = int.Parse(Txt_Libro_CantidadPaginas.Text);
                    libro.Pagina = int.Parse(Txt_Libro_PaginaActual.Text);
                }
                #endregion

                #region Pelicula
                if (subtipo_cbo.SelectedIndex == 1)
                {
                    pelicula = new Pelicula();
                    pelicula.Duracion_minutos = int.Parse(Txt_Pelicula_Duracion.Text);
                    pelicula.Minuto = int.Parse(Txt_Pelicula_Minuto.Text);
                }
                #endregion
                
                #region Serie
                if (subtipo_cbo.SelectedIndex == 2)
                {
                    serie = new Serie();
                    serie.Tiempo_capitulo = int.Parse(Txt_Tiempo_Capitulos.Text);
                    serie.Capitulos_temporada = int.Parse(Txt_Capitulos_Temporada.Text);
                    serie.Temporada = int.Parse(Txt_Temporada.Text);
                    serie.Capitulo = int.Parse(Txt_Capitulo.Text);
                    serie.Minuto = int.Parse(Txt_MinutoSerie.Text);
                }
                #endregion

                #region Juego
                if (subtipo_cbo.SelectedIndex == 3)
                {
                    juego = new Juego();
                }
                #endregion

                #region Cargar contenido
                if (ContenidoController.updateContenido(contenido))
                {
                    MessageBox.Show("Actualizacion de Contenido correcto");
                }
                else
                {
                    MessageBox.Show("Actualizacion de Contenido no ha funcionado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                

                //Libro
                if (libro != null)
                {
                    PeliculaController.deletePelicula(id_contenido);
                    JuegoController.delete(id_contenido);
                    SerieController.deleteSerie(id_contenido);
                    MessageBox.Show("Recordatorio: Falta eliminacion de tipo al cambiar tipo en libro");

                    libro.Id_contenido = id_contenido;
                    if (LibroController.existLibro(id_contenido)) {
                        if (correcto_subtipo = LibroController.updateLibro(libro))
                        {
                            MessageBox.Show("actualizado correctamente Libro");
                        }
                        else
                        {
                            MessageBox.Show("Libro no se ha actualizado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        if (correcto_subtipo = LibroController.insertLibro(libro))
                        {
                            MessageBox.Show("Ingresado correctamente Libro");
                        }
                        else
                        {
                            MessageBox.Show("Libro no se ha ingresado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

                //Pelicula
                if (pelicula != null)
                {
                    LibroController.deleteLibro(id_contenido);
                    JuegoController.delete(id_contenido);
                    SerieController.deleteSerie(id_contenido);
                    MessageBox.Show("Recordatorio: Falta eliminacion de tipo al cambiar tipo en pelicula");

                    pelicula.Id_contenido = id_contenido;
                    if (PeliculaController.existPelicula(id_contenido))
                    {
                        if (correcto_subtipo = PeliculaController.updatePelicula(pelicula))
                        {
                            MessageBox.Show("Actualizado correctamente Pelicula");
                        }
                        else
                        {
                            MessageBox.Show("Pelicula no se ha actualizado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        if (correcto_subtipo = PeliculaController.insertPelicula(pelicula))
                        {
                            MessageBox.Show("Ingresado correctamente Pelicula");
                        }
                        else
                        {
                            MessageBox.Show("Pelicula no se ha ingresado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }   
                    }
                }

                //Serie
                if (serie != null)
                {
                    
                    LibroController.deleteLibro(id_contenido);
                    JuegoController.delete(id_contenido);
                    PeliculaController.deletePelicula(id_contenido);
                    MessageBox.Show("Recordatorio: Falta eliminacion de tipo al cambiar tipo en serie");
                    serie.Id_contenido = id_contenido;

                    if (SerieController.existSerie(id_contenido))
                    {
                        if (correcto_subtipo = SerieController.updateSerie(serie))
                        {
                            MessageBox.Show("Actualizado correctamente Serie");
                        }
                        else
                        {
                            MessageBox.Show("Serie no se ha actualizado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        if (correcto_subtipo = SerieController.insertSerie(serie))
                        {
                            MessageBox.Show("Ingresado correctamente Serie");
                        }
                        else
                        {
                            MessageBox.Show("Serie no se ha ingresado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

                //Juego
                if (juego != null)
                {
                    LibroController.deleteLibro(id_contenido);
                    PeliculaController.deletePelicula(id_contenido);
                    SerieController.deleteSerie(id_contenido);
                    MessageBox.Show("Recordatorio: Falta eliminacion de tipo al cambiar tipo en juego");

                    juego.Id_contenido = id_contenido;
                    if (JuegoController.exist(id_contenido))
                    {
                        if (correcto_subtipo = JuegoController.update(id_contenido))
                        {
                            MessageBox.Show("Actualizado correctamente Juego");
                        }
                        else
                        {
                            MessageBox.Show("Juego no se ha actualizado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        correcto_subtipo = NotasModificar(id_contenido);
                    }
                    else
                    {
                        if (correcto_subtipo = JuegoController.insert(juego))
                        {
                            MessageBox.Show("Ingresado correctamente Juego");
                        }
                        else
                        {
                            MessageBox.Show("Juego no se ha ingresado correctamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

                if (!correcto_subtipo)
                {
                    if (ContenidoController.deleteContenido(id_contenido))
                    {
                        MessageBox.Show("Eliminacion de contenido de emergencia: Exitoso", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Eliminacion de contenido de emergencia: Fallo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    return;
                }

                #endregion
            }

            CerrarVentana();
        }

        

        public void CerrarVentana()
        {
            Modificar_contenido modificar = new Modificar_contenido();
            modificar.Show();
            this.Close();
        }

        #region Juegos



        public List<Nota> lista_notas;
        public List<Nota> eliminados_notas;
        public List<NotaTabla> lista_notaTabla;

        public class NotaTabla
        {
            private string descripcion;
            private bool completado;

            public string Descripcion { get => descripcion; set => descripcion = value; }
            public bool Listo { get => completado; set => completado = value; }
        }

        private void JuegoPreparativos()
        {   
            lista_notas = NotaController.listar(id_contenido);
            lista_notaTabla = new List<NotaTabla>();
            eliminados_notas = new List<Nota>();

            foreach (var item in lista_notas)
            {
                NotaTabla nt = new NotaTabla();
                nt.Descripcion = item.Descripcion;
                nt.Listo = item.Completado;
                lista_notaTabla.Add(nt);
            }

            DataGrid_juego_tabla.ItemsSource = lista_notaTabla;
            ActualizarLista();
        }

        private void ActualizarLista()
        {
            DataGrid_juego_tabla.Items.Refresh();
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
            nt.Listo = false;

            lista_notas.Add(nota);
            lista_notaTabla.Add(nt);

            ActualizarLista();
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid_juego_tabla.SelectedIndex;
            if (index < 0) return;

            eliminados_notas.Add(lista_notas[index]);

            lista_notaTabla.RemoveAt(index);
            lista_notas.RemoveAt(index);

            ActualizarLista();
        }

        private void tabla_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataGrid_juego_tabla.Columns.Count >= 2)
            {
                DataGrid_juego_tabla.Columns[0].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
                DataGrid_juego_tabla.Columns[1].Width = new DataGridLength(0.2, DataGridLengthUnitType.Star);
            }
        }

        private bool NotasModificar(int id_contenido)
        {
            for (int i = 0; i < lista_notas.Count; i++)
            {
                if (DataGrid_juego_tabla.Items.Count < i + 1) break;
                NotaTabla nt = (NotaTabla) DataGrid_juego_tabla.Items[i];
                Nota nota = lista_notas[i];
                nota.Descripcion = nt.Descripcion;
                nota.Completado = nt.Listo;
                if (nota.Id_contenido < 0) nota.Id_contenido = id_contenido;

                if (NotaController.exist(nota.Id_nota, nota.Id_contenido))
                {
                    //existe nota
                    if (nota.Descripcion != "")
                    {
                        if (NotaController.update(nota) == false)
                        {
                            MessageBox.Show("Update nota a fallado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        if (NotaController.delete(nota.Id_nota, nota.Id_contenido) == false)
                        {
                            MessageBox.Show("Delete nota a fallado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                }
                else
                {
                    //no existe nota
                    if (nota.Descripcion != "")
                    {
                        if (NotaController.insert(nota) == false)
                        {
                            MessageBox.Show("Insert nota a fallado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                }
            }

            foreach (var nota in eliminados_notas)
            {
                if (NotaController.exist(nota.Id_nota, nota.Id_contenido))
                {
                    if (NotaController.delete(nota.Id_nota, nota.Id_contenido) == false)
                    {
                        MessageBox.Show("Delete nota a fallado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion
    }
}
