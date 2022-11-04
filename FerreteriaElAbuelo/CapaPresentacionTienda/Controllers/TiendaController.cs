using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;

using System.IO;
using Antlr.Runtime.Misc;
using System.Web.Services.Description;

namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetalleProducto(int idProducto = 0)
        {
            Producto oProducto = new Producto();
            bool conversion;

            oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == idProducto).FirstOrDefault();

            if (oProducto != null)
            {
                oProducto.Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);
                oProducto.Extension = Path.GetExtension(oProducto.NombreImagen);
            }

            return View(oProducto);
        }



        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            listaCategorias = new CN_Categoria().Listar();

            return Json(new { data = listaCategorias }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMarcaPorCategoria(int idCategoria)
        {
            List<Marca> listaMarca = new List<Marca>();

            listaMarca = new CN_Marca().ListarMarcaPorCategoria(idCategoria);

            return Json(new { data = listaMarca }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListarProducto(int idCategoria, int idMarca)
        {
            List<Producto> listaProducto = new List<Producto>();

            bool conversion;

            listaProducto = new CN_Producto().Listar().Select(p => new Producto()
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oCategoria = p.oCategoria,
                oMarca = p.oMarca,
                Precio = p.Precio,
                Stock = p.Stock,
                RutaImagen = p.RutaImagen,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
                Extension = Path.GetExtension(p.NombreImagen),
                Activo = p.Activo
            }).Where(p =>
                p.oCategoria.IdCategoria == (idCategoria == 0 ? p.oCategoria.IdCategoria : idCategoria) &&
                p.oMarca.IdMarca == (idMarca == 0 ? p.oMarca.IdMarca : idMarca) &&
                p.Stock > 0 && p.Activo == true
            ).ToList();

            var jsonResult = Json(new { data = listaProducto }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }



        [HttpPost]
        public JsonResult AgregarCarrito(int idProducto)
        {
            int idCliente = ((Cliente)Session["Cliente"]).IdCliente;

            bool existe = new CN_Carrito().ExisteCarrito(idCliente, idProducto);

            bool respuesta = false;

            string mensaje = string.Empty;

            if (existe)
            {
                mensaje = "El producto ya existe en el carrito";
            }
            else
            {
                respuesta = new CN_Carrito().OperacionCarrito(idCliente, idProducto, true, out mensaje);
            }

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CantidadEnCarrito()
        {
            int idCliente = ((Cliente)Session["Cliente"]).IdCliente;
            int cantidad = new CN_Carrito().CantidadEnCarrito(idCliente);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult ListarProductosCarrito()
        {
            int idCliente = ((Cliente)Session["Cliente"]).IdCliente;

            List<Carrito> oLista = new List<Carrito>();

            bool conversion;

            oLista = new CN_Carrito().ListarProducto(idCliente).Select(oc => new Carrito()
            {
                oProducto = new Producto()
                {
                    IdProducto = oc.oProducto.IdProducto,
                    Nombre = oc.oProducto.Nombre,
                    oMarca = oc.oProducto.oMarca,
                    Precio = oc.oProducto.Precio,
                    RutaImagen = oc.oProducto.RutaImagen,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.RutaImagen, oc.oProducto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.NombreImagen)
                },
                Cantidad = oc.Cantidad
            }).ToList();


            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult OperacionCarrito(int idProducto, bool sumar)
        {
            int idCliente = ((Cliente)Session["Cliente"]).IdCliente;

            string mensaje = string.Empty;

            bool respuesta = new CN_Carrito().OperacionCarrito(idCliente, idProducto, true, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult EliminarCarrito(int idProducto)
        {
            int idCliente = ((Cliente)Session["Cliente"]).IdCliente;
            bool respuesta = false;
            string mensaje = string.Empty;


            respuesta = new CN_Carrito().EliminarCarrito(idCliente, idProducto);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }





        //TRABAJAMOS CON LA UBICACION

        [HttpPost]
        public JsonResult ObtenerDepartamento()
        {
            List<Departamento> oLista = new List<Departamento>();

            oLista = new CN_Ubicacion().ObtenerDepartamento();

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public JsonResult ObtenerProvincia(string idDepartamento)
        {
            List<Provincia> oLista = new List<Provincia>();

            oLista = new CN_Ubicacion().ObtenerProvincia(idDepartamento);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult ObtenerDistritos(string idDepartamento, string idProvincia)
        {
            List<Distrito> oLista = new List<Distrito>();

            oLista = new CN_Ubicacion().ObtenerDistritos(idDepartamento, idProvincia);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Carrito()
        {
            return View();
        }

    }
}