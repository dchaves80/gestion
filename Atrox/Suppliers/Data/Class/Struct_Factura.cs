using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Class
{
    public class Struct_Factura
    {
        public int UserId;
        List<Struct_DetalleFactura> MiDetalle;
        

        public Struct_Factura(int p_userId) 
        {
            UserId = p_userId;
            MiDetalle = new List<Struct_DetalleFactura>();
        }

        public void AddDetail(int IdProducto)
        {
            MiDetalle.Add(new Struct_DetalleFactura(IdProducto,UserId));
        }

        public void RemoveDetail(int IdDetail) 
        {
            for (int a = 0; a < MiDetalle.Count; a++) 
            {
                if (MiDetalle[a].PRODUCTO.Id == IdDetail) 
                {
                    MiDetalle.Remove(MiDetalle[a]);
                    break;
                }
            }
        }

    }
}
