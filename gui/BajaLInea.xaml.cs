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
    /// Lógica de interacción para BajaLInea.xaml
    /// </summary>
    public partial class BajaLInea : Window
    {
        private ObservableCollection<Linea> listaLineas;
        public BajaLInea()
        {
            InitializeComponent();
            listaLineas =LogicaNegocio.ListaLineas ;
            dg.DataContext = listaLineas;

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(dg.SelectedIndex != -1) 
            {
                Linea? linea = dg.SelectedItem as Linea;
                LogicaNegocio.eliminarParadasLinea(linea);
                LogicaNegocio.eliminarLinea(linea);               
                LogicaNegocio.anaidirLineaCsv();
                LogicaNegocio.anaidirParadaCsv();

            }
            else
            {
                MessageBox.Show("No existe seleccion");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
