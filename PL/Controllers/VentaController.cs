using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        [HttpGet]
        public ActionResult ProductoGetAll()
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
        public ActionResult ProductoGetAll(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();
            result = BL.ProductoSucursal.GetByIdSucursal(productoSucursal);
            ML.Result resultSucursal = BL.Sucursal.GetAllEF();
            productoSucursal.Sucursal = new ML.Sucursal();
            productoSucursal.Sucursal.Sucursales = resultSucursal.Objects;
            productoSucursal.ProductoSucursales = result.Objects;
            return View(productoSucursal);
        }

        [HttpGet]
        public ActionResult GetByIdSucursal()
        {
            ML.Result result = BL.Sucursal.GetAllEF();

            ML.Venta venta = new ML.Venta();
            venta.VentaProducto = new ML.VentaProducto();
            venta.VentaProducto.ProductoSucursal = new ML.ProductoSucursal();
            venta.VentaProducto.ProductoSucursal.Sucursal = new ML.Sucursal();
            venta.Ventas = new List<object>();
            venta.VentaProducto.ProductoSucursal.Sucursal.Sucursales = result.Objects;

            return View(venta);

        }
        [HttpPost]
        public ActionResult GetByIdSucursal(ML.Venta venta)
        {
            ML.Result result = new ML.Result();
            result = BL.Venta.GetByIdSucursal(venta);
            venta.Ventas = result.Objects;
            venta.VentaProducto  = new ML.VentaProducto();
            venta.VentaProducto.ProductoSucursal = new ML.ProductoSucursal();
            venta.VentaProducto.ProductoSucursal.Sucursal = new ML.Sucursal();
            venta.VentaProducto.ProductoSucursal.Sucursal.Sucursales = new List<object>();
            ML.Result resultSucursales = BL.Sucursal.GetAllEF();
            venta.VentaProducto.ProductoSucursal.Sucursal.Sucursales = resultSucursales.Objects;

            return View(venta);

        }
    }
}