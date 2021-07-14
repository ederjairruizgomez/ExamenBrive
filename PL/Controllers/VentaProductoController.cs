using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
    {
        public ActionResult GetByIdVenta(int IdVenta)
        {
            ML.Result result = BL.VentaProducto.GetByIdVenta(IdVenta);
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ventaProducto.VentaProductos = result.Objects;
            ventaProducto.ProductoSucursal = new ML.ProductoSucursal();
            ventaProducto.ProductoSucursal.Producto = new ML.Producto();
            return View(ventaProducto);
        }
    }
}