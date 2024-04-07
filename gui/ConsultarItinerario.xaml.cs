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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tarea2Eval.dto;
using Tarea2Eval.logica;

namespace Tarea2Eval.gui
{
    /// <summary>
    /// Lógica de interacción para ConsultarItinerario.xaml
    /// </summary>
    public partial class ConsultarItinerario : Window
    {
        private ObservableCollection<Linea> listaLineas;
        private ObservableCollection<Parada> paradaLineas;
        private Linea linea;
        public ConsultarItinerario()
        {
            InitializeComponent();
            listaLineas = LogicaNegocio.ListaLineas;
            cmb.DataContext = listaLineas;
            cmb.SelectedIndex = 0;
            if(listaLineas.Count > 0)
            {
                linea = LogicaNegocio.getLineaPosicion(0);
                refresacarDatos();
            }
            

        }

     
        private void refresacarDatos()
        {
            paradaLineas = LogicaNegocio.getParadasLinea(linea);
            dg.DataContext = paradaLineas;
            lblMS.DataContext = linea.MunSalida;
            lblML.DataContext = linea.MunLlegada;

            

        }

        private void cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linea=(Linea)cmb.SelectedItem;
            refresacarDatos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
