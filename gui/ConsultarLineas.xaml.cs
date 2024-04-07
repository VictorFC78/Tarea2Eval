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
    /// Lógica de interacción para ConsultarLineas.xaml
    /// </summary>
    public partial class ConsultarLineas : Window
    {
        private ObservableCollection<Linea> listaLineas;
        public ConsultarLineas()
        {
            InitializeComponent();
            listaLineas = LogicaNegocio.ListaLineas;
            dg.DataContext = listaLineas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
