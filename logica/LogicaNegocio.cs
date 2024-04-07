using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tarea2Eval.dto;

namespace Tarea2Eval.logica
{
    public static class LogicaNegocio
    {
        private static List<Municipio> listaMuncipios;   
        private static String archivoMunicipios = "municipios.csv";
        private static String archivoLineas = "Lineas.csv";
        private static String archivoParadas = "Paradas.csv";
        private static ObservableCollection<Linea> listaLineas;
        private static ObservableCollection<Parada> listaParadas;

        public static ObservableCollection<Linea> ListaLineas { get => listaLineas; set => listaLineas = value; }
        public static List<Municipio> ListaMuncipiosSalida { get => listaMuncipios; set => listaMuncipios = value; }
        public static ObservableCollection<Parada> ListaParadas { get => listaParadas; set => listaParadas = value; }




        //se invoca al iniciar la aplicacion
        public static void inicializarSistema()
        {
            //carga en memoria los municipios
            using (StreamReader str = new StreamReader(archivoMunicipios))
            using(CsvReader csr=new CsvReader(str, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 }))
            {
                listaMuncipios=csr.GetRecords<Municipio>().ToList(); 
                
            }
            //carga en memoria las lineas
            using (StreamReader strl = new StreamReader(archivoLineas))
            using (CsvReader csrl = new CsvReader(strl, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 }))
            {
                List<Linea> lista = csrl.GetRecords<Linea>().ToList();
                listaLineas = new ObservableCollection<Linea>(lista);
                lista.Clear();
            }
            //carga en memoria la paradas
            using (StreamReader strp = new StreamReader(archivoParadas))
            using (CsvReader csrp = new CsvReader(strp, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 }))
            {
                listaParadas = new ObservableCollection<Parada>(csrp.GetRecords<Parada>().ToList());
            }

        }
      
        //persistir  lineas en el csv
        public static void anaidirLineaCsv()
        {
            using (StreamWriter stw = new StreamWriter(archivoLineas))
            using (CsvWriter csw = new CsvWriter(stw, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 }))
            {
                csw.WriteRecords(listaLineas);
            }
        }
        //persistir parada en el csv
        public static void anaidirParadaCsv()
        {
            using (StreamWriter stw = new StreamWriter(archivoParadas))
            using (CsvWriter csw = new CsvWriter(stw, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 }))
            {
                csw.WriteRecords(listaParadas);
            }
        }
        //recupera el ultimo id para crear una linea nueva
        public static int ultimoid()
        {
            int ultimoId=0;
                foreach(Linea linea in listaLineas)
                {
                    if(linea.Id > ultimoId) { ultimoId=linea.Id;}
                }
            
            return ultimoId;
        }
        //comprueba que la linea no existe,que su municipio de salida y parada no sean igualea a otra linea
        public static Boolean existeLineaLista(Municipio munSalida , Municipio munLlegada)
        {
            foreach(Linea linea in listaLineas)
            {
                if (linea.MunLlegada.Equals(munLlegada.Nombre) && linea.MunSalida.Equals(munSalida.Nombre)) return true;
            }
            return false;
        }
        //añadir una linea a la lista
        public static void anaidirLinea(Linea linea) 
        { 
            listaLineas.Add(linea);
        }
        //eliminar una linea de la lista
        public  static void eliminarLinea(Linea linea) 
        {
            listaLineas.Remove(linea);
        }

        //recuperar una linea por su posicion en la lista
        public static Linea getLineaPosicion(int posicion)
        {
            return listaLineas.ElementAt(posicion);
        }
        //busca las paradas de una determinada linea y kas elimina
        public static void eliminarParadasLinea(Linea linea)
        {
           ObservableCollection<Parada> lista=new ObservableCollection<Parada>();
            foreach (Parada parada in listaParadas)
            {
                if (linea.Id == parada.IdLinea) { lista.Add(parada); }
            }
            foreach(Parada parada in lista) { 
                listaParadas.Remove(parada);
            }
            lista.Clear();
        }
        public static List<int> getListaHoras()
        {          
             List<int>   horas = new List<int>();
                for (int i = 0; i < 24; i++)
                {
                    horas.Add(i);
                }
              return horas;            
        }
        public static List <int> getListaMinutos() 
        {
            List<int> minutos = new List<int>();
                for (int i = 0; i < 60; i++)
                {
                    minutos.Add(i);
                }
                return minutos;
            }
       
        //retorna la lista de paradas correspondientes a una linea
        public static ObservableCollection<Parada> getParadasLinea(Linea linea)
        {
            ObservableCollection<Parada> lista = new ObservableCollection<Parada>();
            foreach(Parada parada in listaParadas)
            {
                if (linea.Id == parada.IdLinea) lista.Add(parada);
            }
            return lista;
        }

        //añade una parada a la lista
        public static void anaidirParadaLista(Parada parada)
        {
            listaParadas.Add(parada);
        }

        //elimina una parada de la lista
        public static void eliminarParada(Parada parada)
        {
            listaParadas.Remove(parada);
        }
       
    }
}
