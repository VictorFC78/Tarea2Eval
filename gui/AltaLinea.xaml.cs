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
    /// Lógica de interacción para AltaLinea.xaml
    /// </summary>
    public partial class AltaLinea : Window
    {
        private List<Municipio> listaMunicipios;
        private List<int> horas;
        private List<int> minutos;

        private ObservableCollection<Linea> listaLineas;

        public AltaLinea(List<Municipio>lista)
        {
            InitializeComponent();
            listaMunicipios = lista;
            cmbMunSalida.DataContext = listaMunicipios;
            cmbMunLlegada.DataContext = listaMunicipios;
            cmbMunSalida.SelectedIndex = 0;
            cmbMunLlegada.SelectedIndex = 0;
            horas=LogicaNegocio.getListaHoras();
            minutos=LogicaNegocio.getListaMinutos();
            listaLineas = LogicaNegocio.ListaLineas;
            cmbHorSal.DataContext = horas;
            cmbHorSal.SelectedIndex = 0;
            cmbHInterv.DataContext = horas;
            cmbHInterv.SelectedIndex = 0;
            cmbMinSal.DataContext = minutos;
            cmbMinSal.SelectedIndex = 0;
            cmbMInterv.DataContext = minutos;
            cmbMInterv.SelectedIndex = 0;

        }
       
            
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //recupera los municipios seleccionados
            Municipio municipioSalida=cmbMunSalida.SelectedItem as Municipio;
            Municipio municipiollegada = cmbMunLlegada.SelectedItem as Municipio;
            //recuperamos las horas y mi utos seleccionados para crear objeto tipo
            int horaSalida =(int) cmbHorSal.SelectedItem;
            int minSalida=(int) cmbMinSal.SelectedItem;
            int horaIntervalo=(int)cmbHInterv.SelectedItem;
            int minIntervalo=(int)cmbMInterv.SelectedItem;
            TimeOnly tiempoSalida= new TimeOnly(horaSalida,minSalida);//la hora y minuto de llegada
            TimeOnly tiempollegda = new TimeOnly(horaIntervalo, minIntervalo);//la hora y mi uto de salida
            //comparamos para que la hora de llegada sea mayor que la salida
            if (!LogicaNegocio.existeLineaLista(municipioSalida, municipiollegada))
            {
                if (municipiollegada.Codigo == municipioSalida.Codigo)
                {
                    MessageBox.Show("EL MUNICIPIO DE SALIDA Y LLEGADA DEBEN SER DISTINTOS");
                }
                else
                {
                    //recuperamos el ultimo id de linea y le sumamos uno
                    int id = LogicaNegocio.ultimoid();
                    //creamos un istancia de linea nueva
                    Linea linea = new Linea(id + 1, municipioSalida.Nombre, municipiollegada.Nombre, tiempoSalida, tiempollegda);
                    LogicaNegocio.anaidirLinea(linea);
                    LogicaNegocio.anaidirLineaCsv();
                 
                }
            }
            else
            {
                MessageBox.Show("YA EXISTE UNA LINEA CON ESE ITINERARIO DE SALIDA Y LLEGADA");
            }
           
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
