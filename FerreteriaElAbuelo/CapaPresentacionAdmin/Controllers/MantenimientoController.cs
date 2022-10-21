using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Agregamos las referencias
using CapaNegocio;
using CapaEntidad;




namespace CapaPresentacionAdmin.Controllers
{
    public class MantenimientoController : Controller
    {
        // Trabajar con el Controlador de Categorias

        public ActionResult Categorias()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();

            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }




        // Trabajar con el Controlador de Marcas




        public ActionResult Marcas()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<Marca> oLista = new List<Marca>();

            oLista = new CN_Marca().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GuardaMarca(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdMarca == 0)
            {
                resultado = new CN_Marca().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Marca().Eliminar(id, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }






        public ActionResult Productos()
        {
            return View();
        }

    }
}