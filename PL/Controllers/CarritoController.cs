using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class CarritoController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];
            return View(result);
        }
        [HttpGet]
        public ActionResult AddProducto(int Id)
        {
            ML.Producto producto = new ML.Producto();
            if (Session["Carrito"] == null)
            {

                producto.IdProducto = Id;
                var result = BL.Producto.GetByIdEF(producto.IdProducto);


                ML.Producto productoItem = new ML.Producto();
                productoItem = (ML.Producto)result.Object;
                productoItem.VentaProducto = new ML.VentaProducto();
                productoItem.VentaProducto.Cantidad = 1;

                productoItem.Venta = new ML.Venta();


                result.Objects = new List<Object>();
                result.Objects.Add(result.Object);
                Session["Carrito"] = result.Objects; //Boxing


                return View("GetAll", result);
            }
            else
            {
                producto.IdProducto = Id;
                var result = BL.Producto.GetByIdEF(producto.IdProducto);

                ML.Producto productoItem = new ML.Producto();
                productoItem = (ML.Producto)result.Object;
                productoItem.VentaProducto = new ML.VentaProducto();
                productoItem.VentaProducto.Cantidad = 1;

                productoItem.Venta = new ML.Venta();

                result.Objects = (List<Object>)Session["Carrito"];

                int pos = 0;
                bool comprobar = false;

                foreach (ML.Producto productos in result.Objects.ToList())
                {
                    if (productos.IdProducto == Id)
                    {
                        comprobar = true;
                        pos = productos.IdProducto;
                    }

                }


                if (comprobar == true)
                {
                    foreach (ML.Producto productos in result.Objects.ToList())
                    {
                        if (productos.IdProducto == pos)
                        {
                            productos.VentaProducto.Cantidad++;
                            productos.Venta.Total = productos.VentaProducto.Cantidad * productos.PrecioUnitario;
                            break;
                        }
                    }
                }
                else
                {
                    result.Objects.Add(result.Object);
                    Session["Carrito"] = result.Objects;
                }

                return View("GetAll", result);

            }
        }
        public ActionResult DeleteProducto(int Id)
        {

            TempData["alertMessage"] = "Producto Eliminado";
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];
            int pos = 0;

            if (result.Objects.Count == 0)
            {
                TempData["alertMessage"] = "No Existen Productos Agrega uno";
                return View("GetAll", result);
            }
            else
            {
                foreach (ML.Producto producto in result.Objects.ToList())
                {
                    pos++;
                    if (producto.IdProducto == Id)
                    {
                        break;
                    }
                }

                result.Objects.RemoveAt(pos - 1);

                return View("GetAll", result);
            }

        }
        public ActionResult IncrementaProducto(int Id)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];



            foreach (ML.Producto producto in result.Objects.ToList())
            {
                if (producto.IdProducto == Id)
                {
                    producto.VentaProducto.Cantidad++;
                    producto.Venta.Total = producto.VentaProducto.Cantidad * producto.PrecioUnitario;
                    break;
                }
            }

            return View("GetAll", result);

        }
        public ActionResult DecrementaProducto(int id)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];

            int pos = 0;
            bool comprobar = false;
            foreach (ML.Producto producto in result.Objects.ToList())
            {
                pos++;
                if (producto.IdProducto == id)
                {
                    producto.VentaProducto.Cantidad--;
                    producto.Venta.Total = producto.VentaProducto.Cantidad * producto.PrecioUnitario;
                    comprobar = false;
                    if (producto.VentaProducto.Cantidad <= 0)
                    {
                        comprobar = true;
                    }
                    break;
                }

            }

            if (comprobar == true)
            {
                result.Objects.RemoveAt(pos - 1);
            }

            return View("GetAll", result);

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult ListProduct(List<ML.ProductoSucursal> productoSucursal)
        {
            ML.ProductoSucursal productoSucursales = new ML.ProductoSucursal();
            productoSucursales.Producto = new ML.Producto();


            if (Session["Carrito"] == null)
            {
                var result = new ML.Result();
                result.Objects = new List<Object>();
                //Existe
                foreach (ML.ProductoSucursal productoItem in productoSucursal)
                {
                    productoItem.Producto = new ML.Producto();
                    if (productoItem.Producto.IdProducto != null)
                    {
                        productoSucursales.Producto.IdProducto = productoItem.Producto.IdProducto;

                        var consult = BL.ProductoSucursal.GetByIdEF(productoSucursales.Producto.IdProducto);


                        result.Objects.Add(consult.Object);
                    }
                    else
                    {
                        //No esta Seleccionado
                    }

                }

                Session["Carrito"] = result.Objects;
                return View("GetAll", result);
            }

            else
            {
                var result = new ML.Result();
                result.Objects = new List<Object>();
                result.Objects = (List<Object>)Session["Carrito"];
                int pos = 0;


                foreach (ML.ProductoSucursal productoItem in productoSucursal.ToList())
                {
                    productoItem.Producto = new ML.Producto();
                    if (productoItem.Producto.IdProducto != null)
                    {

                        if (productoItem.Producto.IdProducto == productoSucursal[pos].Producto.IdProducto)
                        {
                            productoSucursal[pos].VentaProducto = new ML.VentaProducto();
                            productoSucursal[pos].VentaProducto.Cantidad++;
                            productoSucursal[pos].VentaProducto.Venta.Total = productoSucursal[pos].VentaProducto.Cantidad * productoSucursal[pos].Producto.PrecioUnitario;
                        }
                        else
                        {
                            productoSucursales.Producto.IdProducto = productoItem.Producto.IdProducto;
                            var consult = BL.ProductoSucursal.GetByIdEF(productoSucursales.Producto.IdProducto);
                            result.Objects.Add(consult.Object);
                            Session["Carrito"] = result.Objects;
                        }


                    }
                    else
                    {
                        //No esta Seleccionado
                    }
                    pos++;
                }
                return View("GetAll", result);

            }
        }
        [HttpGet]
        public ActionResult ProcesarCompra(List<ML.ProductoSucursal> productoSucursal)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.Objects = (List<object>)Session["Carrito"];

            ML.Venta venta = new ML.Venta();
            BL.Venta.AddEF(venta, result.Objects);
            return View("GetByID", result);
        }
    }
}