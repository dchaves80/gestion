using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data2.Statics;

namespace Data2.Class
{

    

    public class Struct_Factura
    {

        //Para Parseo a comprobante.
        public bool IsRemito=false;
        public Struct_Remito Remito;
        //Para Parseo a comprobante.


        public enum TipoDeFactura { Null ,FacturaA, FacturaB, FacturaC, Presupuesto, FacturaX};
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
        public string serialAFIP;
        public string serialTICKET;
        public bool printed;
        public bool PrintAgain;
        public bool Transmitida;
        public DateTime Fecha;
        public int IdCuentaCorriente;
        public bool cheque;
        public string DNI;
        public int IdTarjeta;
        public string NumeroTarjeta;
        public decimal subtotal;
        public bool ivas;
        public decimal total;

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

        private void SetVendedor(int p_IdVendedor)
        {
            Connection.D_Factura F = new Connection.D_Factura();
            if (Id != 0 && p_IdVendedor != 0 && UserId != 0)
            {
                F.InsertVendedorEnFactura(UserId, p_IdVendedor, Id);
            }
        }

        public bool GuardarFactura(int p_IdVendedor, int Cliente = 0) 
        {
            if (MiDetalle == null || MiDetalle.Count == 0) 
            {
                return false;
            }

            Connection.D_Factura F = new Connection.D_Factura();

            
            bool RespInscripto = false;
            bool RespNoInscripto = false; 
            bool Exento = false;
            bool ConsumidorFinal = false; 
            bool RespMonotributo = false;
           // A CC
            Struct_Cliente SC = null;
            if (Cliente != 0) 
            {
                SC = Struct_Cliente.GetClient(Cliente, UserId);
            }
            // FIN A CC


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
                    SetVendedor(p_IdVendedor);
                    if (SC!=null)
                    {
                        SC.InsertDetail(this);
                    }
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
                    SetVendedor(p_IdVendedor);
                    if (SC != null)
                    {
                        SC.InsertDetail(this);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (FacturaTipo == TipoDeFactura.FacturaC)
            {
                int IdFactura = F.InsertFactura(UserId, "", "", DateTime.Now, "C", senores, domicilio, telefono, localidad, cuit, RespInscripto, RespNoInscripto, Exento, ConsumidorFinal, RespMonotributo, Contado, CtaCte, 0, false, "", false, 0, "", Observaciones, GetTotalSinIva(), true, GetTotalConIvaIncluido());

                if (IdFactura != 0)
                {
                    Id = IdFactura;
                    F.InsertarDetalleFactura(this);
                    SetVendedor(p_IdVendedor);
                    if (SC != null)
                    {
                        SC.InsertDetail(this);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (FacturaTipo == TipoDeFactura.FacturaX)
            {
                int IdFactura = F.InsertFactura(UserId, "", "", DateTime.Now, "X", senores, domicilio, telefono, localidad, cuit, RespInscripto, RespNoInscripto, Exento, ConsumidorFinal, RespMonotributo, Contado, CtaCte, 0, false, "", false, 0, "", Observaciones, GetTotalSinIva(), true, GetTotalConIvaIncluido());

                if (IdFactura != 0)
                {
                    Id = IdFactura;
                    F.InsertarDetalleFactura(this);
                    SetVendedor(p_IdVendedor);
                    if (SC != null)
                    {
                        SC.InsertDetail(this);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (FacturaTipo == TipoDeFactura.Presupuesto)
            {
                int IdFactura = F.InsertFactura(UserId, "", "", DateTime.Now, "P", senores, domicilio, telefono, localidad, cuit, RespInscripto, RespNoInscripto, Exento, ConsumidorFinal, RespMonotributo, Contado, CtaCte, 0, false, "", false, 0, "", Observaciones, GetTotalSinIva(), true, GetTotalConIvaIncluido());

                if (IdFactura != 0)
                {
                    Id = IdFactura;
                    F.InsertarDetalleFactura(this);
                    SetVendedor(p_IdVendedor);
                    if (SC != null)
                    {
                        SC.InsertDetail(this);
                    }
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


        public static Struct_Factura GetFacturaById(int p_userid, int p_idfactura) 
        {
            Connection.D_Factura _Conn = new Connection.D_Factura();
            DataRow _DR = _Conn.GetFacturaById(p_userid, p_idfactura);
            if (_DR != null)
            {
                return new Struct_Factura(_DR);
            }
            else 
            {
                return null;
            }
        }

        public static List<Struct_Factura> GetFacturasBetweenDates(DateTime START, DateTime END, int p_UserID, bool p_printed, TipoDeFactura TF)
        {
            string T;
            switch (TF)
            {
                case TipoDeFactura.FacturaA: T="A";break;
                case TipoDeFactura.FacturaB: T="B";break;
                case TipoDeFactura.FacturaC: T="C";break;
                case TipoDeFactura.FacturaX: T = "X"; break;
                case TipoDeFactura.Presupuesto: T = "P"; break;
                default: T = "0"; break;
            }
            Data2.Connection.D_Factura Conn = new Connection.D_Factura();
            List<Struct_Factura> FL = new List<Struct_Factura>();
            List<DataTable>  DT_L = Conn.GetFacturasBetweenDates(p_UserID, START, END,T,p_printed);
            if (DT_L[0] != null)
            {
                
                foreach (DataRow R in DT_L[0].Rows)
                {
                    FL.Add(new Struct_Factura(R));
                }
            }

            if (DT_L[1] != null) 
            {
              foreach (DataRow R in DT_L[1].Rows)
                {
                    Struct_Remito _Remito = new Struct_Remito(R);
                    Struct_Factura _F = new Struct_Factura(_Remito);
                    FL.Add(_F);
                }   
            }


            if (FL.Count > 0) 
            {
                return FL;
            } else
            {
                return null;
            }

            
        }

        public Struct_Factura(DataRow dr) 
        {
            Id=int.Parse(dr["Id"].ToString());
            UserId = int.Parse(dr["IdUser"].ToString());
            serialAFIP = dr["SerialAFIP"].ToString();
            serialTICKET = dr["SerialTICKET"].ToString();
            printed = Data2.Statics.Conversion.convertSQLToBoolean(dr["Printed"]);
            PrintAgain = Data2.Statics.Conversion.convertSQLToBoolean(dr["PrintAgain"]);
            Transmitida = Data2.Statics.Conversion.convertSQLToBoolean(dr["Transmitida"]);
            Fecha = DateTime.Parse(dr["Fecha"].ToString());
            switch (dr["Tipo"].ToString())
            {
                case "A": FacturaTipo=TipoDeFactura.FacturaA;break;
                case "B" :FacturaTipo=TipoDeFactura.FacturaB;break;
                case "C" :FacturaTipo=TipoDeFactura.FacturaC;break;
                case "X": FacturaTipo = TipoDeFactura.FacturaX; break;
                case "P": FacturaTipo = TipoDeFactura.Presupuesto; break;
            }
            senores = dr["Nombre"].ToString();
            domicilio = dr["Domicilio"].ToString();
            telefono = dr["Telefono"].ToString();
            localidad = dr["Localidad"].ToString();
            cuit = dr["Cuit"].ToString();
            if (Conversion.convertSQLToBoolean(dr["RepInsc"])) Condicion_IVA = CondicionIVA.RespInscripto;
            if (Conversion.convertSQLToBoolean(dr["RespNoInsc"])) Condicion_IVA = CondicionIVA.RespNoInscripto;
            if (Conversion.convertSQLToBoolean(dr["Exento"])) Condicion_IVA = CondicionIVA.Exento;
            if (Conversion.convertSQLToBoolean(dr["ConsumidorFinal"])) Condicion_IVA = CondicionIVA.ConsumidorFinal;
            if (Conversion.convertSQLToBoolean(dr["RespMonotributo"])) Condicion_IVA = CondicionIVA.RespMonotributo;
            if (Conversion.convertSQLToBoolean(dr["Contado"])) Pago = CondicionPago.Contado;
            if (Conversion.convertSQLToBoolean(dr["CtaCte"])) Pago = CondicionPago.CtaCte;
            IdCuentaCorriente = int.Parse(dr["IdCtaCte"].ToString());
            if (Conversion.convertSQLToBoolean(dr["Cheque"])) cheque=true;
            DNI = dr["Dni"].ToString();
            IdTarjeta = int.Parse(dr["Idtargeta"].ToString());
            NumeroTarjeta = dr["NumeroTarjeta"].ToString();
            Observaciones = dr["Observaciones"].ToString();
            subtotal = Conversion.GetDecimal(dr["SubTotal"].ToString());
            ivas = Conversion.convertSQLToBoolean(dr["Ivas"]);
            total = Conversion.GetDecimal(dr["Total"].ToString());

            CargarDetalleDesdeDB();

        }


        void CargarDetalleDesdeDB() 
        {
            GestionDataSet.GetDetalleFacturaDataTable DT = new GestionDataSet.GetDetalleFacturaDataTable();
            GestionDataSetTableAdapters.GetDetalleFacturaTableAdapter TA = new GestionDataSetTableAdapters.GetDetalleFacturaTableAdapter();
            TA.Fill(DT, Id, UserId);
            if (DT.Rows.Count > 0) 
            {
                MiDetalle = new List<Struct_DetalleFactura>();
                foreach (DataRow _Row in DT.Rows) 
                {
                    Struct_DetalleFactura _DetalleFactura = new Struct_DetalleFactura(_Row, UserId);

                    MiDetalle.Add(_DetalleFactura);
                
                }
            }
        }

        public Struct_Factura(Struct_Remito RemitoToParse)
        {
            IsRemito = true;
            Remito = RemitoToParse;
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

        public void AddDetail(string CodeProducto)
        {
            MiDetalle.Add(new Struct_DetalleFactura(CodeProducto, UserId));
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
