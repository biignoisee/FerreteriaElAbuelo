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
    public class CD_Usuarios
    {

        public List<Usuario> Listar()
        {
            List<Usuario> listaUsuario = new List<Usuario>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.connection))
                {
                    string query = "select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from Usuario";

                    SqlCommand command = new SqlCommand(query, oconexion);
                    command.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader datareader = command.ExecuteReader())
                    {
                        while (datareader.Read())
                        {
                            listaUsuario.Add(
                                new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(datareader["IdUsuario"]),
                                    Nombres = datareader["Nombres"].ToString(),
                                    Apellidos = datareader["Apellidos"].ToString(),
                                    Correo = datareader["Correo"].ToString(),
                                    Clave = datareader["Clave"].ToString(),  
                                    Reestablecer = Convert.ToBoolean(datareader["Reestablecer"]),
                                    Activo = Convert.ToBoolean(datareader["Activo"])
                                });
                        }
                    }
                } 
            }
            catch
            {
                listaUsuario = new List<Usuario>();
            }

            return listaUsuario;

        }

    }
}
