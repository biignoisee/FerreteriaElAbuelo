using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregamos las respectivas referencias
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();


        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }


        //Capa en donde aplicamos todas las reglas del negocio


    }
}
