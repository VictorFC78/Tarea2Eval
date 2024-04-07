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
    /// Lógica de interacción para AltaItinerario.xaml
    /// </summary>
    public partial class AltaItinerario : Window
    {
        private ObservableCollection<Linea> listaLineas;
        private ObservableCollection<Parada> paradasLinea;
        private Linea linea;
        private List<Municipio> municipios;
        private List<int> horasSalida;
        private List<int> minSalida;
        public AltaItinerario()
        {
            InitializeComponent();
            listaLineas = LogicaNegocio.ListaLineas;
            horasSalida = LogicaNegocio.getListaHoras();
            minSalida=LogicaNegocio.getListaMinutos();
            if (listaLineas.Count > 0)
            {
                cmbLinea.SelectedIndex = 0;
                linea = LogicaNegocio.getLineaPosicion(0);
                refrescardatos();
                lblHS.DataContext = linea.MunSalida;
                lblHL.DataContext = linea.MunLlegada;
                cmbLinea.DataContext = listaLineas;
            }
            municipios = LogicaNegocio.ListaMuncipiosSalida;
            cmbMun.DataContext = municipios;
            cmbHorLle.DataContext = horasSalida;
            cmbMinLle.DataContext = minSalida;
            cmbMun.SelectedIndex = 0;
            cmbHorLle.SelectedIndex = 0;
            cmbMinLle.SelectedIndex = 0;


        }
        private void refrescardatos()
        {
            paradasLinea = LogicaNegocio.getParadasLinea(linea);
            dg.DataContext = paradasLinea;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             linea = (Linea)cmbLinea.SelectedItem;
             refrescardatos();
             Municipio municipio = (Municipio)cmbMun.SelectedItem;
             TimeOnly intervalo = new TimeOnly((int)cmbHorLle.SelectedItem, (int)cmbMinLle.SelectedItem);
               
            //inicialmente comprobamos que municipio de parada no se igual que el de salida
            if (linea.MunSalida == municipio.Nombre)
            {
                MessageBox.Show("La parada no puede ser igual que el municipio de salida");
            }//comprobamos que la parada no exista ya en la lista
            else if (existeParadaLista(municipio))
            {
                MessageBox.Show("La parada ya existe en la lista");
            }//comprobamos si el municipio de parada es la ultima parada, en ese caso comprobamos que su intervalo es el mayor de  la lista
            else 
            {
                if(municipio.Nombre==linea.MunLlegada)
                {
                    if (intervaloCorrecto(intervalo))
                    {
                        Parada parada=new Parada(linea.Id,municipio.Nombre,intervalo);
                        LogicaNegocio.anaidirParadaLista(parada);
                        LogicaNegocio.anaidirParadaCsv();
                    }
                    else
                    {
                        MessageBox.Show("La parada final debe tener el mayor intervalo");
                    }
                }
                else
                {
                    if (existeMunLlegadaLista(linea) && intervaloCorrecto(intervalo))
                    {
                        MessageBox.Show("La parada debe tener menor intervalo que la parada final");
                    }else if(existeMunLlegadaLista(linea) && !intervaloCorrecto(intervalo))
                    {
                        Parada parada = new Parada(linea.Id, municipio.Nombre, intervalo);
                        LogicaNegocio.anaidirParadaLista(parada);
                        LogicaNegocio.anaidirParadaCsv();
                    }
                    else
                    {
                        Parada parada = new Parada(linea.Id, municipio.Nombre, intervalo);
                        LogicaNegocio.anaidirParadaLista(parada);
                        LogicaNegocio.anaidirParadaCsv();
                    }
                }
                refrescardatos();
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
        //comprueba si un municipio existe en la lista de paradas de el itinerario
       

        private void cmbLinea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linea = (Linea)cmbLinea.SelectedItem;
            lblHS.DataContext = linea.MunSalida;
            lblHL.DataContext = linea.MunLlegada;
            refrescardatos();

        }
       private Boolean existeParadaLista(Municipio municipio)
        {
            foreach(Parada parada in paradasLinea)
            {
                if (parada.Municipio == municipio.Nombre) return true;
            }
            return false;
        }
        private Boolean intervaloCorrecto(TimeOnly intervalo)
        {
            TimeOnly timeOnly = new TimeOnly(0,0);
            foreach(Parada parada in paradasLinea)
            {
                if(parada.Intervalo.CompareTo(timeOnly) > 0) timeOnly=parada.Intervalo;
            }
            if(intervalo.CompareTo(timeOnly) > 0) return true;
            return false;
        }
        private Boolean existeMunLlegadaLista(Linea linea)
        {
            foreach(Parada parada in paradasLinea)
            {
                if(parada.Municipio==linea.MunLlegada) return true;
            }
            return false;
        }
    }
    
}
