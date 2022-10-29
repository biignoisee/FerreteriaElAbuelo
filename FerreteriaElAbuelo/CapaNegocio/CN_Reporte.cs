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
    public class CN_Reporte
    {
        private CD_Reporte objCapaDato = new CD_Reporte();

        //Capa en donde aplicamos todas las reglas del negocio
        public Dashboard VerDashboard()
        {
            return objCapaDato.VerDashboard();
        }


    }
}
