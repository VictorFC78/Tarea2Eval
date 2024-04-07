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
    /// Lógica de interacción para ModificarLinea.xaml
    /// </summary>
    public partial class ModificarLinea : Window
    {
        private ObservableCollection<Linea> listalineas;
        private List<Municipio> municipios;
        private List<int> horas;
        private List<int> minutos;
        public ModificarLinea()
        {
            InitializeComponent();
            listalineas = LogicaNegocio.ListaLineas;
            dg.DataContext = listalineas;
            horas = LogicaNegocio.getListaHoras();
            minutos=LogicaNegocio.getListaMinutos();
            municipios = LogicaNegocio.ListaMuncipiosSalida;
            cmbMS.DataContext = municipios;cmbMS.SelectedIndex = 0;
            cmbML.DataContext = municipios;cmbML.SelectedIndex = 0;
            cmbHS.DataContext = horas; cmbHS.SelectedIndex = 0;
            cmbMinS.DataContext= minutos;cmbMinS.SelectedIndex = 0;
            cmbIH.DataContext = horas; cmbIH.SelectedIndex = 0;
            cmbIM.DataContext=minutos;cmbIM.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedIndex != -1)
            {
                Linea linea = LogicaNegocio.getLineaPosicion(dg.SelectedIndex);
                linea.MunSalida=((Municipio)cmbMS.SelectedItem).Nombre;
                linea.MunLlegada = ((Municipio)cmbML.SelectedItem).Nombre;
                linea.HoraSalida=new TimeOnly((int)cmbHS.SelectedItem,(int)cmbMinS.SelectedItem);
                linea.Intervalo= new TimeOnly((int)cmbIH.SelectedItem, (int)cmbIM.SelectedItem);
                LogicaNegocio.anaidirLineaCsv();

            }
            else
            {
                MessageBox.Show("No existe seleccion");
            }
        }
    }
}
