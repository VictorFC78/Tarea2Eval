using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea2Eval.dto
{
    public class Parada:INotifyPropertyChanged
    {
        private int  idLinea;
        private string municipio;
        [Format("HH:mm")]
        private TimeOnly intervalo;

        public Parada()
        {
        }

        public Parada(int idLinea, string municipio, TimeOnly intervalo)
        {
            this.idLinea = idLinea;
            this.municipio = municipio;
            this.intervalo = intervalo;
        }

        public int IdLinea
        {
            get{ return idLinea; }
            set 
            { 

                if (idLinea != value)
                {
                    idLinea = value;
                    OnPropertyChanged(nameof(idLinea));

                }
            }
        }

        public string Municipio
        {
            get { return municipio; }
            set 
            {
                if (municipio != value) { 
                    municipio=value; 
                    OnPropertyChanged(nameof(municipio));
                }
            }
        }

        public TimeOnly Intervalo
        {
            get { return intervalo; }
            set 
            {
                if (intervalo != value)
                {
                    intervalo=value;
                    OnPropertyChanged(nameof(intervalo));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
