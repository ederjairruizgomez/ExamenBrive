using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class Producto
    {
        public static ML.Result AddEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var query = context.ProductoAdd(producto.Nombre, producto.PrecioUnitario, producto.CodigoDeBarras, producto.Imagen);

                    if (query != null)
                    {
                        result.Correct = true;
                        ML.Sucursal sucursal = new ML.Sucursal();
                        result = BL.Sucursal.GetAllEF();
                        sucursal.Sucursales = result.Objects;
                        ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
                        productoSucursal.Stock = 0;
                        ML.Producto productoItem = BL.Producto.GetByCodigoDeBarrasEF(producto.CodigoDeBarras);
                        BL.ProductoSucursal.AddEF(productoItem.IdProducto, sucursal.Sucursales, productoSucursal.Stock);
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El Departamento no se pudo insertar correctamente.";
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
                    var productos = context.ProductoGetAll().ToList();

                    result.Objects = new List<object>();

                    if (productos != null)
                    {
                        foreach (var obj in productos)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = int.Parse(obj.IdProducto.ToString());
                            producto.Nombre = obj.Nombre;
                            producto.PrecioUnitario = decimal.Parse(obj.PrecioUnitario.ToString());
                            producto.CodigoDeBarras = int.Parse(obj.CodigoDeBarras.ToString());
                            producto.Imagen = obj.Imagen;
                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Productos";
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
        public static ML.Result DeleteEF(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var query = context.ProductoDelete(IdProducto);

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
        public static ML.Result UpdateEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var query = context.ProductoUpdate(producto.IdProducto, producto.Nombre, producto.CodigoDeBarras, producto.PrecioUnitario, producto.Imagen);

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
        public static ML.Result GetByIdEF(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var productos = context.ProductoGetById(IdProducto).SingleOrDefault();



                    if (productos != null)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = int.Parse(productos.IdProducto.ToString());
                        producto.Nombre = productos.Nombre;
                        producto.PrecioUnitario = decimal.Parse(productos.PrecioUnitario.ToString());
                        producto.CodigoDeBarras = int.Parse(productos.CodigoDeBarras.ToString());
                        producto.Imagen = productos.Imagen;
                        result.Object = producto;

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
        public static ML.Producto GetByCodigoDeBarrasEF(int CodigoDeBarras)
        {
            ML.Producto result = new ML.Producto();
            ML.Result result1 = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var productos = context.ProductoGetByCodigoDeBarras(CodigoDeBarras).SingleOrDefault();

                    if (productos != null)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = int.Parse(productos.IdProducto.ToString());
                        producto.Nombre = productos.Nombre;
                        producto.PrecioUnitario = decimal.Parse(productos.PrecioUnitario.ToString());
                        producto.CodigoDeBarras = int.Parse(productos.CodigoDeBarras.ToString());
                        producto.Imagen = productos.Imagen;
                        result = producto;
                        result1.Correct = true;
                    }
                    else
                    {
                        result1.Correct = false;
                        result1.ErrorMessage = "No existen registros en la tabla Departamento";
                    }

                }
            }

            catch (Exception ex)
            {
                result1.Correct = false;
                result1.ErrorMessage = ex.Message;

            }
            return result;
        }
    }
}
