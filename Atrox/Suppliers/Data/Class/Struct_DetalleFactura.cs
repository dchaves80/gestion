using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Class
{
    public class Struct_DetalleFactura
    {
        public int DETALLEINT;
        public decimal DETALLEDEC;
        public Struct_Producto PRODUCTO;

        public Struct_DetalleFactura(int IdProd, int IdUser) 
        {
            PRODUCTO = Struct_Producto.Get_SingleArticle(IdUser, IdProd);
        }

        public void SetearCantidad(int Cantidad)
        {
            DETALLEINT = Cantidad;
        }

        public void SetearCantidad(decimal Cantidad)
        {
            DETALLEDEC = Cantidad;
        }

    }
}
