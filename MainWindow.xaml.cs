using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tarea2Eval.dto;
using Tarea2Eval.gui;
using Tarea2Eval.logica;

namespace Tarea2Eval
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Municipio> listaMunicipios;
        public ObservableCollection<Linea> listalineas;
        
        public MainWindow()
        {
            InitializeComponent();
            LogicaNegocio.inicializarSistema();
            listaMunicipios = LogicaNegocio.ListaMuncipiosSalida;
            
            
                 

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AltaLinea altaLinea = new AltaLinea(listaMunicipios);
            altaLinea.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            BajaLInea baja = new BajaLInea();
            baja.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ModificarLinea modificar = new ModificarLinea();
            modificar.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ConsultarLineas consultar = new ConsultarLineas();
            consultar.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            AltaItinerario altaItinerario = new AltaItinerario();
            altaItinerario.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            DarBajaItinerario bajaItinerario = new DarBajaItinerario(); bajaItinerario.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            ConsultarItinerario consultItinerario = new ConsultarItinerario();
            consultItinerario.Show();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            ModificarItinerario modificarItinerario=new ModificarItinerario();
            modificarItinerario.Show();
        }
    }
}