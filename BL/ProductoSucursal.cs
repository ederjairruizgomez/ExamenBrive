using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProductoSucursal
    {
        public static ML.Result AddEF(int IdProducto,List<object> Sucursales,int Stock)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    
                    foreach (ML.Sucursal sucursal in Sucursales)
                    {
                        context.ProductoSucursalAdd(IdProducto,sucursal.IdSucursal,Stock);
                    }
                        result.Correct = true;

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
                    var productoSucursales = context.ProductoSucursalGetAll().ToList();

                    result.Objects = new List<object>();

                    if (productoSucursales != null)
                    {
                        foreach (var obj in productoSucursales)
                        {
                            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
                            productoSucursal.IdProductoSucursal = obj.IdProductoSucursal;
                            productoSucursal.Producto = new ML.Producto();
                            productoSucursal.Producto.IdProducto = int.Parse(obj.IdProducto.ToString());
                            productoSucursal.Sucursal = new ML.Sucursal();
                            productoSucursal.Sucursal.IdSucursal = int.Parse(obj.IdSucursal.ToString());
                            productoSucursal.Stock = int.Parse(obj.Stock.ToString());
                            result.Objects.Add(productoSucursal);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Producto Sucursal";
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
        public static ML.Result GetByIdSucursal(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var sucursales = context.ProductoSucursalGetByIdSucursal(productoSucursal.Sucursal.IdSucursal);

                    result.Objects = new List<object>();
                    if (sucursales != null)
                    {
                        foreach (var Obj in sucursales)
                        {
                            ML.ProductoSucursal productoSucursales = new ML.ProductoSucursal();
                            productoSucursales.IdProductoSucursal = Obj.IdProductoSucursal;
                            productoSucursales.Stock = Obj.Stock.Value;
                            productoSucursales.Producto = new ML.Producto();
                            productoSucursales.Producto.IdProducto = Obj.IdProducto;
                            productoSucursales.Producto.CodigoDeBarras = Obj.CodigoDeBarras.Value;
                            productoSucursales.Producto.Nombre = Obj.NombreProducto;
                            productoSucursales.Producto.PrecioUnitario = Obj.PrecioUnitario.Value;
                            productoSucursales.Producto.Imagen = Obj.Imagen;
                            productoSucursales.Sucursal = new ML.Sucursal();
                            productoSucursales.Sucursal.IdSucursal = Obj.IdSucursal;
                            productoSucursales.Sucursal.Nombre = Obj.NombreSucursal;
                            result.Objects.Add(productoSucursales);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Departamento";
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
        public static ML.Result UpdateStockEF(ML.ProductoSucursal productoSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var query = context.ProductoSucursalUpdateStock(productoSucursal.IdProductoSucursal,productoSucursal.Stock);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El Producto no se pudo actualizar correctamente.";
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
        public static ML.Result DeleteEF(int IdProductoSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var query = context.ProductoSucursalDelete(IdProductoSucursal);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El Producto no se pudo borrar correctamente.";
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
        public static ML.Result GetByIdEF(int IdProductoSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var productosSucursales = context.ProductoSucursalGetById(IdProductoSucursal).SingleOrDefault();



                    if (productosSucursales != null)
                    {
                        ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
                        productoSucursal.Producto = new ML.Producto();
                        productoSucursal.Sucursal = new ML.Sucursal();
                        productoSucursal.IdProductoSucursal = int.Parse(productosSucursales.IdProductoSucursal.ToString());
                        productoSucursal.Stock = productosSucursales.Stock.Value;

                        result.Object = productoSucursal;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Departamento";
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
