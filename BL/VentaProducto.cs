using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class VentaProducto
    {
        public static ML.Result AddEF(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var query = context.VentaProductoAdd(productoSucursal.Venta.IdVenta, productoSucursal.VentaProducto.Cantidad, productoSucursal.IdProductoSucursal);

                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La VentaProducto no se pudo insertar correctamente.";
                    }
                }

            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;


        }
        public static ML.Result GetByIdVenta(int IdVenta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var ventas = context.GetByIdVenta(IdVenta);

                    result.Objects = new List<object>();
                    if (ventas != null)
                    {
                        foreach (var obj in ventas)
                        {
                            ML.VentaProducto ventaProducto = new ML.VentaProducto();
                            ventaProducto.IdVentaProducto = obj.IdVentaProducto;
                            ventaProducto.Cantidad = int.Parse(obj.Cantidad.ToString());
                            ventaProducto.Venta = new ML.Venta();
                            ventaProducto.Venta.IdVenta = obj.IdVenta;
                            ventaProducto.Venta.Total = decimal.Parse(obj.Total.ToString());
                            ventaProducto.Venta.Fecha = DateTime.Parse(obj.Fecha.ToString());
                            ventaProducto.ProductoSucursal = new ML.ProductoSucursal();
                            ventaProducto.ProductoSucursal.IdProductoSucursal = obj.IdProductoSucursal;
                            ventaProducto.ProductoSucursal.Producto = new ML.Producto();
                            ventaProducto.ProductoSucursal.Producto.IdProducto = obj.IdProducto;
                            ventaProducto.ProductoSucursal.Producto.Nombre = obj.Nombre;
                            ventaProducto.ProductoSucursal.Producto.PrecioUnitario = decimal.Parse(obj.PrecioUnitario.ToString());
                            ventaProducto.ProductoSucursal.Producto.Imagen = obj.Imagen;
                            ventaProducto.ProductoSucursal.Producto.CodigoDeBarras = int.Parse(obj.CodigoDeBarras.ToString());
                            ventaProducto.Subtotal = ventaProducto.Cantidad * ventaProducto.ProductoSucursal.Producto.PrecioUnitario;

                            result.Objects.Add(ventaProducto);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Venta Producto";
                    }

                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
        
    }
}
