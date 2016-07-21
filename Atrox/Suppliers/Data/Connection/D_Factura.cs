using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Connection
{
    public class D_Factura
    {

        public void InsertarDetalleFactura(Class.Struct_Factura p_F) 
        {
            for (int a = 0; a < p_F.GetDetalle().Count; a++) 
            {
                GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
                QTA.Insert_DetalleFactura(
                    p_F.Id, 
                    p_F.GetDetalle()[a].PRODUCTO.Id, 
                    p_F.GetDetalle()[a].PRODUCTO.PrecioNeto, 
                    p_F.GetDetalle()[a].PRODUCTO.IVA, 
                    p_F.GetDetalle()[a].PRODUCTO.PrecioCompra, 
                    p_F.GetDetalle()[a].PRODUCTO.PorcentajeGanancia, 
                    p_F.GetDetalle()[a].PRODUCTO.PrecioFinal, 
                    p_F.GetDetalle()[a].DETALLEINT, 
                    p_F.GetDetalle()[a].PRODUCTO.CantidadDEC);
            }
        }

        public int InsertFactura(
            int p_IdUser,
            string p_SerialAFIP,
            string p_SerialTICKET,
            DateTime p_Fecha,
            string p_Tipo,
            string p_Nombre,
            string p_Domicilio,
            string p_Telefono,
            string p_Localidad,
            string p_Cuit,
            bool p_RespInsc,
            bool p_RespNoInsc,
            bool p_Exento,
            bool p_ConsumidorFinal,
            bool p_RespMonotributo,
            bool p_Contado,
            bool p_CtaCte,
            int p_IdCtaCte,
            bool p_Cheque,
            string p_DNI,
            bool p_Tarjeta,
            int p_IdTarjeta,
            string p_NumeroTarjeta,
            string p_Observaciones,
            decimal p_SubTotal,
            bool p_Ivas,
            decimal p_Total
            
            ) 
        {
            /*@IdUser bigint,
@SerialAFIP varchar(255),@SerialTICKET varchar(255),
@Printed bit,@PrintAgain bit,
@Transmitida bit,@Fecha datetime,
@Tipo varchar(5),@Nombre varchar(255),
@Domicilio varchar(255),@Telefono varchar(255),
@Localidad varchar(255),@Cuit varchar(255),
@RepInsc bit,@RespNoInsc bit,
@Exento bit,@ConsumidorFinal bit,
@RespMonotributo bit,@Contado bit,
@CtaCte bit,@IdCtaCte bigint,
@Cheque bit,@Dni varchar(255),
@Tarjeta bit,@Idtargeta bigint,
@NumeroTarjeta varchar(50),@Observaciones text,
@SubTotal decimal(18,4),@Ivas bit,
@Total decimal(18,4))*/

            GestionDataSet.insert_FacturaDataTable DT = new GestionDataSet.insert_FacturaDataTable();
            GestionDataSetTableAdapters.insert_FacturaTableAdapter TA = new GestionDataSetTableAdapters.insert_FacturaTableAdapter();
            try
            {
                TA.Fill(DT, p_IdUser, p_SerialAFIP, p_SerialTICKET, false, false, false, DateTime.Now, p_Tipo, p_Nombre, p_Domicilio, p_Telefono, p_Localidad, p_Cuit, p_RespInsc, p_RespNoInsc, p_Exento, p_ConsumidorFinal, p_RespMonotributo, p_Contado, p_CtaCte, p_IdCtaCte, p_Cheque, p_DNI, p_Tarjeta, p_IdTarjeta, p_NumeroTarjeta, p_Observaciones, p_SubTotal, p_Ivas, p_Total);
                if (DT.Rows.Count > 0)
                {
                    return int.Parse( DT.Rows[0][0].ToString());
                }
                else { return 0; }
            }
            catch (Exception E)
            {
                Statics.Log.ADD("[" + E.Message + "]" + E.StackTrace, null);
                return 0;
            }
            
            
        }
    }
}
