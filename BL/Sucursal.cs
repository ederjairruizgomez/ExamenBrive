using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetIds()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var sucursales = context.GetIds().ToList();

                    result.Objects = new List<object>();

                    if (sucursales != null)
                    {
                        foreach (var idSucursal in sucursales)
                        {
                            var IdSucursal = int.Parse(idSucursal.ToString());
                            result.Objects.Add(IdSucursal);
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
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ERuizBriveEntities1 context = new DL.ERuizBriveEntities1())
                {
                    var sucursales = context.SucursalGetAll().ToList();

                    result.Objects = new List<object>();

                    if (sucursales != null)
                    {
                        foreach (var obj in sucursales)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = int.Parse(obj.IdSucursal.ToString());
                            sucursal.Nombre = obj.Nombre;

                            result.Objects.Add(sucursal);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Sucursal";
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
