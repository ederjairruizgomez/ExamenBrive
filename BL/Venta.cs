using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;

namespace BL
{
    public class Venta
    {
        public static ML.Result AddEF(ML.Venta venta, List<object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    ObjectParameter output = new ObjectParameter("IdVenta", typeof(int));

                    var query = context.VentaAdd(output, venta.Total);

                    venta.IdVenta = (int)output.Value;

                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La Venta no se pudo insertar correctamente.";
                    }

                    foreach (ML.ProductoSucursal productoSucursal in Objects)
                    {
                        productoSucursal.Venta = venta;

                        BL.VentaProducto.AddEF(productoSucursal);
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
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var ventas = context.VentaGetAll().ToList();

                    result.Objects = new List<object>();

                    if (ventas != null)
                    {
                        foreach (var obj in ventas)
                        {
                            ML.Venta venta = new ML.Venta();
                            venta.IdVenta = int.Parse(obj.IdVenta.ToString());
                            venta.Total = decimal.Parse(obj.Total.ToString());
                            venta.Fecha = DateTime.Parse(obj.Fecha.ToString());
                            result.Objects.Add(venta);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Venta";
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
        public static ML.Result GetByIdSucursal(ML.Venta venta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var sucursales = context.VentaProductoGetByIdSucursal(venta.VentaProducto.ProductoSucursal.Sucursal.IdSucursal);

                    result.Objects = new List<object>();
                    if (sucursales != null)
                    {
                        foreach (var obj in sucursales)
                        {
                            ML.Venta ventas = new ML.Venta();
                            ventas.VentaProducto = new ML.VentaProducto();
                            ventas.IdVenta = obj.IdVenta;
                            ventas.Total = decimal.Parse(obj.Total.ToString());
                            ventas.Fecha = DateTime.Parse(obj.Fecha.ToString());
                            ventas.VentaProducto.ProductoSucursal = new ML.ProductoSucursal();
                            ventas.VentaProducto.ProductoSucursal.Sucursal = new ML.Sucursal();
                            ventas.VentaProducto.ProductoSucursal.Sucursal.IdSucursal = obj.IdSucursal;
                            ventas.VentaProducto.ProductoSucursal.Sucursal.Nombre = obj.Nombre;
                            result.Objects.Add(ventas);
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
