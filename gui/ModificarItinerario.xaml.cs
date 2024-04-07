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
    /// Lógica de interacción para ModificarItinerario.xaml
    /// </summary>
    public partial class ModificarItinerario : Window
    {
        private ObservableCollection<Linea> listaLineas;
        private Linea linea;
        private ObservableCollection<Parada> listaparadas;
        private List<int> horas;
        private List<int> minutos;
        private List<Municipio> municipios;
        private List<Municipio> municipiosBis;
        public ModificarItinerario()
        {
            InitializeComponent();
            listaLineas = LogicaNegocio.ListaLineas;
            cmb.DataContext = listaLineas;
            horas=LogicaNegocio.getListaHoras();
            cmbHorSal.DataContext = horas;
            cmbHorSal.SelectedIndex = 0;
            minutos=LogicaNegocio.getListaMinutos();
            cmbMinSal.DataContext = minutos;
            cmbMinSal.SelectedIndex = 0;
            municipios = LogicaNegocio.ListaMuncipiosSalida;
            cmbMun.DataContext = municipios;
            cmbMun.SelectedIndex = 0;
            
            if (listaLineas.Count > 0 )
            {
                cmb.SelectedIndex = 0;
                linea = LogicaNegocio.getLineaPosicion(0);
                refresacarDatos();
            }
        }
        private void refresacarDatos()
        {
            listaparadas = LogicaNegocio.getParadasLinea(linea);
            dg.DataContext = listaparadas;
            lblMS.DataContext = linea.MunSalida;
            lblML.DataContext = linea.MunLlegada;
            
        }

        private void cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linea = (Linea)cmb.SelectedItem;
            refresacarDatos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if(dg.SelectedIndex!=-1)
            {
                Parada parada = dg.SelectedItem as Parada;
                Municipio municipioParada = cmbMun.SelectedItem as Municipio;
                TimeOnly nuevoIntervalo = new TimeOnly((int)cmbHorSal.SelectedItem, (int)cmbMinSal.SelectedItem);
                if (municipioParada.Nombre == linea.MunSalida)//comprueba que no se el municipio de salida
                {
                    MessageBox.Show("NO SE PERMITE UTILIZAR EL MUNICIPIO DE SALIDA COMO PARADA");
                //si solo se modifica el tiempo comprueba que sea el correcto
                }else if (municipioParada.Nombre == parada.Municipio)
                {
                    if (existeMunLlegadaLista(linea))
                    {
                        if(!intervaloCorrecto(nuevoIntervalo)) 
                        {
                            parada.Intervalo= nuevoIntervalo;
                        }
                        else
                        {
                            MessageBox.Show("EL INTERVALO DEBE SER MENOR QUE EL DE LA ULTIMA PARADA");
                        }
                    }
                    // al cambiar el municipio se comprueba que el nuevo no exista en la lista de paradas
                }else
                {
                    if (existeParadaLista(municipioParada))
                    {
                        MessageBox.Show("LA PARADA SELCCIONADA YA EXISTE EN EL ITINERARIO");
                    }
                    else //al no existir se comprueba que el tiempo se el correcto
                    {
                        if (existeMunLlegadaLista(linea))//al existir la parada final se comprueba el tiempo
                        {
                            if (!intervaloCorrecto(nuevoIntervalo))
                            {
                                parada.Municipio = municipioParada.Nombre;
                                parada.Intervalo = nuevoIntervalo;
                            }
                            else
                            {
                                MessageBox.Show("EL INTERVALO DEBE SER MENOR QUE EL DE LA ULTIMA PARADA");
                            }
                        }
                        else
                        {
                            parada.Municipio = municipioParada.Nombre;
                            parada.Intervalo = nuevoIntervalo;
                        }
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("NO SE HA SELECCINADO NINGUNA PARADA");

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private Boolean intervaloCorrecto(TimeOnly intervalo)
        {
            TimeOnly timeOnly = new TimeOnly(0, 0);
            foreach (Parada parada in listaparadas)
            {
                if (parada.Intervalo.CompareTo(timeOnly) > 0) timeOnly = parada.Intervalo;
            }
            if (intervalo.CompareTo(timeOnly) > 0) return true;
            return false;
        }
        private Boolean existeMunLlegadaLista(Linea linea)
        {
            foreach (Parada parada in listaparadas)
            {
                if (parada.Municipio == linea.MunLlegada) return true;
            }
            return false;
        }
        private Boolean existeParadaLista(Municipio municipio)
        {
            foreach (Parada parada in listaparadas)
            {
                if (parada.Municipio == municipio.Nombre) return true;
            }
            return false;
        }
    }
}
