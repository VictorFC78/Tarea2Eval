using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tarea2Eval.dto
{
    public class Linea:INotifyPropertyChanged
    {
        private int id;
        private string munSalida;
        private string munLlegada;
        [Format("HH:mm")]
        private TimeOnly horaSalida;
        [Format("HH:mm")]
        private TimeOnly intervalo;
      
        public event PropertyChangedEventHandler? PropertyChanged;

        public Linea()
        {
        }

        public Linea(int id,string munSalida, string munLlegada, TimeOnly horaSalida, TimeOnly intervalo)
        {
            this.munSalida = munSalida;
            this.munLlegada = munLlegada;
            this.horaSalida = horaSalida;
            this.intervalo = intervalo;
            this.id = id;
        }

        public int Id { get => id; set => id = value; }
        public string MunSalida {
            get { return munSalida; } 
            set
            {
                if(munSalida!=value) {
                    munSalida = value;
                    OnPropertyChanged(nameof(munSalida));
                }
            } 
        }
       
        public string MunLlegada 
        { 
            get { return munLlegada; }
            set 
            {
                if (munLlegada!=value)
                    {
                    munLlegada = value;
                    OnPropertyChanged(nameof(munLlegada));
                    }
                } 
        }


        public TimeOnly HoraSalida
        {
            get { return horaSalida; } 
            set
            {
                if (horaSalida!=value)
                {
                    horaSalida = value;
                    OnPropertyChanged(nameof(horaSalida));
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
                    intervalo = value;
                    OnPropertyChanged(nameof(intervalo));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName) 
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

       

    }
    
}
