using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea2Eval.dto
{
    public class Municipio
    {
        private int codigo;
        private string nombre;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public Municipio() { }
        public Municipio(int codigo, string nombre)
        {
            this.codigo = codigo;
            this.nombre = nombre;
        }
        public override string ToString()
        {
            return nombre;
        }
    }
}
