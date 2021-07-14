﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ProductoSucursal
    {
        public int IdProductoSucursal { get; set; }
        public int Stock { get; set; }
        public ML.Producto Producto { get; set; }
        public ML.Sucursal Sucursal { get; set; }
        public List<object> ProductoSucursales { get; set; }
        public ML.VentaProducto VentaProducto { get; set; }
        public ML.Venta Venta { get; set; }

    }
}
