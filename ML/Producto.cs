using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int CodigoDeBarras { get; set; }
        public decimal PrecioUnitario { get; set; }
        public List<object> Productos { get; set; }
        public byte[] Imagen { get; set; }
        public ML.VentaProducto VentaProducto { get; set; }
        public ML.Venta Venta { get; set; }
    }
}
