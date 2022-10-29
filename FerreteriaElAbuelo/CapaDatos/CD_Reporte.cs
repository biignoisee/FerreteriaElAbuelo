using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Añadimos la referencia
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public Dashboard VerDashboard()
        {
            Dashboard objeto = new Dashboard();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.connection))
                {
                    SqlCommand command = new SqlCommand("sp_ReporteDashboard", oconexion);
                    command.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader datareader = command.ExecuteReader())
                    {
                        while (datareader.Read())
                        {
                            objeto=new Dashboard()
                            {
                                TotalCliente = Convert.ToInt32(datareader["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(datareader["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(datareader["TotalProducto"]),
                            };
                        }
                    }
                }
            }
                catch
                {
                objeto = new Dashboard();
            }
        return objeto;
        }
    }




}
