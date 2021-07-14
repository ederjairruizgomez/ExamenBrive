using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductoSucursalController : Controller
    {
        // GET: ProductoSucursal
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Sucursal.GetAllEF();

            ML.ProductoSucursal ProductoSucursal = new ML.ProductoSucursal();
            ProductoSucursal.Sucursal = new ML.Sucursal();
            ProductoSucursal.Producto = new ML.Producto();
            ProductoSucursal.Producto.Productos = new List<object>();
            ProductoSucursal.ProductoSucursales = new List<object>();
            ProductoSucursal.Sucursal.Sucursales = result.Objects;

            return View(ProductoSucursal);

        }
        [HttpPost]
        public ActionResult GetAll(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();
            result = BL.ProductoSucursal.GetByIdSucursal(productoSucursal);
            ML.Result resultSucursal = BL.Sucursal.GetAllEF();
            productoSucursal.Sucursal = new ML.Sucursal();
            productoSucursal.Sucursal.Sucursales = resultSucursal.Objects;
            productoSucursal.ProductoSucursales = result.Objects;
            
            return View(productoSucursal);

        }
        [HttpGet]//Mostrar el formulario
        public ActionResult Form(int? IdProductoSucursal) //Update
        {
            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
            ML.Result result = BL.ProductoSucursal.GetByIdEF(IdProductoSucursal.Value);

            if (result.Correct)
            {
                productoSucursal.IdProductoSucursal = ((ML.ProductoSucursal)result.Object).IdProductoSucursal;
                productoSucursal.Stock = ((ML.ProductoSucursal)result.Object).Stock;
                
                return View(productoSucursal);
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return PartialView("Modal");
            }

            

        }
        [HttpPost] //Recibir datos del formulario
        public ActionResult Form(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();

            result = BL.ProductoSucursal.UpdateStockEF(productoSucursal);
            ViewBag.Message = "El Producto se Actualizo su Stock ";
            

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo actulizar " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdProductoSucursal)
        {
            ML.Result result = BL.ProductoSucursal.DeleteEF(IdProductoSucursal);

            return RedirectToAction("GetAll");
        }
    }
}