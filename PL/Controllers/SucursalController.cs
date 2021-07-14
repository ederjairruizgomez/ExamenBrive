using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SucursalController : Controller
    {
        [HttpGet]//Mostrar el formulario
        public ActionResult Form(int? IdSucursal) //Add
        {
            ML.Sucursal sucursal = new ML.Sucursal();

                return View(sucursal);

        }

    }
}