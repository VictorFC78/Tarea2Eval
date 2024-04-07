using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tarea2Eval.dto;
using Tarea2Eval.logica;

namespace Tarea2Eval.gui
{
    /// <summary>
    /// Lógica de interacción para DarBajaItinerario.xaml
    /// </summary>
    public partial class DarBajaItinerario : Window
    {
        private ObservableCollection<Linea> listaLineas;
        private ObservableCollection<Parada> paradaLineas;
        private Linea linea;
        public DarBajaItinerario()
        {
            InitializeComponent();
            listaLineas = LogicaNegocio.ListaLineas;
            cmb.DataContext = listaLineas;
            
            if (listaLineas.Count>0 ) 
            {
                cmb.SelectedIndex = 0;
                linea = LogicaNegocio.getLineaPosicion(0);
                refrescarDatos();
            }           
        }
        private void refrescarDatos()
        {
            paradaLineas = LogicaNegocio.getParadasLinea(linea);
            dg.DataContext = paradaLineas;
            lblMS.DataContext = linea.MunSalida;
            lblML.DataContext = linea.MunLlegada;
        }

        private void cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linea =(Linea) cmb.SelectedItem;
            refrescarDatos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedIndex != -1)
            {
                Parada parada = dg.SelectedItem as Parada;
                LogicaNegocio.eliminarParada(parada);
                LogicaNegocio.anaidirParadaCsv();
                refrescarDatos();
            }
            else
            {
                MessageBox.Show("NO HAY NINGUNA SELECCION");
            }   
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
