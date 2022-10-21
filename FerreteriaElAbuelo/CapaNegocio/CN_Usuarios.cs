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

        //Capa en donde aplicamos todas las reglas del negocio
        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            if(string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Apellido del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio, ingresa un nombre";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                //se aplica la logica para enviar el correo al usuario

                //string clave = "test123";   //mas tarde creo un metodo que devuelva una autogenerada
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creación de Cuenta, Bienvenido a la Ferreteria 'El Abuelo' ";
                string mensajeCorreo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";

                mensajeCorreo = mensajeCorreo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensajeCorreo);

                if(respuesta)
                {
                    obj.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out Mensaje);
                } 
                else{
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
                obj.Clave = CN_Recursos.ConvertirSha256(clave);

                return objCapaDato.Registrar(obj, out Mensaje);
            } else
            {
                return 0;
            }            
        }



        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacio, ingresa un nombre";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Los Apellidos del usuario no puede ser vacio, ingresa un apellido";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio, ingresa un nombre";
            }



            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            } else
            {
                return false;
            }
        }


        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

    }
}
