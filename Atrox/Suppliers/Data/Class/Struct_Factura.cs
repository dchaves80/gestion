using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Class
{

    

    public class Struct_Factura
    {

        public enum TipoDeFactura { FacturaA, FacturaB, FacturaC};
        public enum CondicionIVA { RespInscripto, RespNoInscripto, Exento, ConsumidorFinal, RespMonotributo }
        public enum CondicionPago { Contado, CtaCte }
        
        public int Id=0;
        public int UserId;
        List<Struct_DetalleFactura> MiDetalle;
        public TipoDeFactura FacturaTipo;
        public CondicionIVA Condicion_IVA;
        public CondicionPago Pago;
        public string senores="";
        public string domicilio="";
        public string telefono="";
        public string localidad="";
        public string cuit="";
        public decimal IvaPredValor=0m;
        public bool IvaPredeterminado;
        public string Observaciones="";

        public List<decimal> GetIvasInscriptos() 
        {
            List<decimal> Ivas = new List<decimal>();

            for (int a = 0; a < MiDetalle.Count; a++) 
            {
                Boolean findit = false;
                for (int b = 0; b < Ivas.Count; b++) 
                {
                    if (MiDetalle[a].PRODUCTO.IVA == Ivas[b]) 
                    {
                        findit = true;
                    }
                }
                if (findit == false) 
                {
                    Ivas.Add(MiDetalle[a].PRODUCTO.IVA);
                }
            }

            if (Ivas.Count > 0)
            {
                return Ivas;
            }
            else 
            {
                return null;
            }

        }


        public decimal GetTotalConIvaIncluido() 
        {
            decimal total = 0m;
            for (int a = 0; a < MiDetalle.Count; a++) 
            {
                total = total + MiDetalle[a].getTotalConIva();
            }
            return total;
        }

        public decimal GetTotalSinIva() 
        {
            decimal total = 0m;
            for (int a = 0; a < MiDetalle.Count; a++)
            {
                total = total + MiDetalle[a].getTotalSinIva();
            }
            return total;
        }

        public decimal GetTotalIvaPredterminado(decimal p_IVA) 
        {
            decimal total = 0m;
            for (int a = 0; a < MiDetalle.Count; a++)
            {
                total = total + MiDetalle[a].getTotalSinIvaPred(p_IVA);
            }
            return total;
        }

        public bool GuardarFactura() 
        {
            Connection.D_Factura F = new Connection.D_Factura();

            
            bool RespInscripto = false;
            bool RespNoInscripto = false; 
            bool Exento = false;
            bool ConsumidorFinal = false; 
            bool RespMonotributo = false;

            if (Condicion_IVA==CondicionIVA.RespInscripto) RespInscripto = true;
            if (Condicion_IVA==CondicionIVA.RespNoInscripto) RespNoInscripto = true;
            if (Condicion_IVA==CondicionIVA.Exento) Exento = true;
            if (Condicion_IVA==CondicionIVA.ConsumidorFinal) ConsumidorFinal = true;
            if (Condicion_IVA==CondicionIVA.RespMonotributo) RespMonotributo = true;

            bool Contado = false;
            bool CtaCte = false;

            if (Pago==CondicionPago.Contado) Contado=true;
            if (Pago==CondicionPago.CtaCte) CtaCte=true;


            if (FacturaTipo == TipoDeFactura.FacturaA)
            {
                int IdFactura = F.InsertFactura(UserId, "", "", DateTime.Now, "A", senores, domicilio, telefono, localidad, cuit, RespInscripto, RespNoInscripto, Exento, ConsumidorFinal, RespMonotributo, Contado, CtaCte, 0, false, "", false, 0, "", Observaciones, GetTotalSinIva(), true, GetTotalConIvaIncluido());

                if ( IdFactura != 0)
                {
                    Id = IdFactura;
                    F.InsertarDetalleFactura(this);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (FacturaTipo == TipoDeFactura.FacturaB) 
            {
                int IdFactura = F.InsertFactura(UserId, "", "", DateTime.Now, "B", senores, domicilio, telefono, localidad, cuit, RespInscripto, RespNoInscripto, Exento, ConsumidorFinal, RespMonotributo, Contado, CtaCte, 0, false, "", false, 0, "", Observaciones, GetTotalSinIva(), true, GetTotalConIvaIncluido());

                if (IdFactura != 0)
                {
                    Id = IdFactura;
                    F.InsertarDetalleFactura(this);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public decimal GetTotalDeInsceripcionIva(decimal IvaFiltro) 
        {
            decimal total = 0m;
            for (int a = 0; a < MiDetalle.Count; a++)
            {
                if (MiDetalle[a].PRODUCTO.IVA == IvaFiltro)
                {
                    total = total + (MiDetalle[a].getTotalConIva() - MiDetalle[a].getTotalSinIva());
                }
            }
            return total;
        }

        public decimal GetTotalesDeIvas() 
        {
            decimal total = 0m;
            for (int a = 0; a < MiDetalle.Count; a++)
            {
                total = total + ( MiDetalle[a].getTotalConIva()-MiDetalle[a].getTotalSinIva() ) ;
            }
            return total;
        }

        public decimal GetTotalesDeIvasPredeterminado(decimal p_IVA) 
        {
            decimal total = 0m;
            for (int a = 0; a < MiDetalle.Count; a++)
            {
                total = total + (MiDetalle[a].getTotalConIva() - MiDetalle[a].getTotalSinIvaPred(p_IVA));
            }
            return total;
        }


      

        public void BorrarDetalle(string p_key)
        {
            for (int a = 0; a < MiDetalle.Count; a++) 
            {
                if (MiDetalle[a].ACCESSKEY == p_key) 
                {
                    MiDetalle.Remove(MiDetalle[a]);
                    break;
                }
            }
        }


        public void setFacturaTipo(TipoDeFactura p_tf)
        {
            FacturaTipo = p_tf;
        }

       

        public Struct_Factura(int p_userId) 
        {
            UserId = p_userId;
            MiDetalle = new List<Struct_DetalleFactura>();
            FacturaTipo = TipoDeFactura.FacturaA;
            Condicion_IVA = CondicionIVA.RespInscripto;
            Pago = CondicionPago.Contado;
            IvaPredeterminado = false;
        }

        public List<Struct_DetalleFactura> GetDetalle()
        {
            if (MiDetalle == null)
            {
                return null;
            }
            else 
            {
                if (MiDetalle.Count == 0) 
                {
                    return null;
                } else 
                {
                    return MiDetalle;
                }
            }
        }

        public void SetCant(string value, string k)
        {
            Struct_DetalleFactura _Detalle = null;
            for (int a = 0; a < MiDetalle.Count; a++) 
            {
                if (MiDetalle[a].ACCESSKEY == k) 
                {
                    _Detalle = MiDetalle[a];
                }
            }
            if (_Detalle != null) 
            {
                _Detalle.set_cant(value);
            }
        }

        public void AddDetail(int IdProducto)
        {
            MiDetalle.Add(new Struct_DetalleFactura(IdProducto,UserId));
        }

        /*public void RemoveDetail(int IdDetail) 
        {
            for (int a = 0; a < MiDetalle.Count; a++) 
            {
                if (MiDetalle[a].PRODUCTO.Id == IdDetail) 
                {
                    MiDetalle.Remove(MiDetalle[a]);
                    break;
                }
            }
        }*/

    }
}
