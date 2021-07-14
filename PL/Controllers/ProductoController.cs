using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAllEF();

            ML.Producto producto = new ML.Producto();
            producto.Productos = result.Objects;

            return View(producto);

        }

        [HttpGet]//Mostrar el formulario
        public ActionResult Form(int? IdProducto) //Add , Update
        {
            ML.Producto producto = new ML.Producto();

            if (IdProducto == null)//Add
            {
                return View(producto);
            }
            else //Update
            {
                ML.Result result = BL.Producto.GetByIdEF(IdProducto.Value);

                if (result.Correct)
                {
                    producto.IdProducto = ((ML.Producto)result.Object).IdProducto;
                    producto.Nombre = ((ML.Producto)result.Object).Nombre;
                    producto.PrecioUnitario = ((ML.Producto)result.Object).PrecioUnitario;
                    producto.CodigoDeBarras = ((ML.Producto)result.Object).CodigoDeBarras;
                    producto.Imagen = ((ML.Producto)result.Object).Imagen;

                    return View(producto);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }
            }


        }

        [HttpPost] //Recibir datos del formulario
        public ActionResult Form(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            HttpPostedFileBase file = Request.Files["ImagenData"];
            if (file.ContentLength > 0)
            {
                producto.Imagen = ConvertToBytes(file);
            }
            if (producto.IdProducto == 0)//Add
            {
                result = BL.Producto.AddEF(producto);
                
                ViewBag.Message = "El Producto se agregó correctamente ";

            }
            else //Update
            {
                result = BL.Producto.UpdateEF(producto);
                ViewBag.Message = "El Producto se actualizó correctamente ";
            }

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo agregar correctamente el producto " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }

        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }

    }
}