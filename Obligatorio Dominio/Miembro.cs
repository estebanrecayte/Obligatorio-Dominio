using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Miembro
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Bloqueado { get; set; }
        public List<Miembro> ListaAmigos { get; set; }

        public Miembro(int id,string nombre,string apellido,DateTime fechaNacimiento, bool bloqueado, List<Miembro> listaAmigos)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Bloqueado = bloqueado;
            ListaAmigos = listaAmigos;
        }
    }
}
