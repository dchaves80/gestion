using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Connection
{
    public class D_StaticWebService
    {


        public string UpdateFacturaTicket(int IdUser, int IdFactura, string FacturaSerial) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            if (QTA.UpdateFacturaTicket(IdUser, IdFactura, FacturaSerial) > 0)
            {
                return "ok";
            }
            else 
            {
                return "null";
            }
        }

        public void GetDetalleFactura(int IdUser, int IdFactura, ref Class.XmlSerializaers.Factura F)
        {
            GestionDataSet.GetDetalleFacturaDataTable DT = new GestionDataSet.GetDetalleFacturaDataTable();
            GestionDataSetTableAdapters.GetDetalleFacturaTableAdapter TA = new GestionDataSetTableAdapters.GetDetalleFacturaTableAdapter();
            TA.Fill(DT, IdFactura, IdUser);
            if (DT.Rows.Count > 0) 
            {
                F.MiDetalle = new List<Class.XmlSerializaers.Detalle>();
                if (DT.Rows[0][0].ToString() != "null")
                {
                    for (int a = 0; a < DT.Rows.Count; a++) 
                    {
                        F.MiDetalle.Add(new Class.XmlSerializaers.Detalle(DT.Rows[a]));
                    }

                }
                
            } 

        }

        public Class.XmlSerializaers.Factura GetDatosFacturas(int IdUser, int IdFactura) 
        {
            GestionDataSet.GetFacturaFromIdDataTable DT = new GestionDataSet.GetFacturaFromIdDataTable();
            GestionDataSetTableAdapters.GetFacturaFromIdTableAdapter TA = new GestionDataSetTableAdapters.GetFacturaFromIdTableAdapter();
            TA.Fill(DT, IdUser, IdFactura);
            if (DT.Rows.Count != 0)
            {
                
                Class.XmlSerializaers.Factura F = new Class.XmlSerializaers.Factura(DT.Rows[0]);
                GetDetalleFactura(IdUser, IdFactura, ref F);
                return F;
            }
            else 
            {
                return null;
            }
        }

        public string GetFacturasDisponibles(int IdUser) 
        {
            GestionDataSet.GetFacturasDisponiblesDataTable DT = new GestionDataSet.GetFacturasDisponiblesDataTable();
            GestionDataSetTableAdapters.GetFacturasDisponiblesTableAdapter TA = new GestionDataSetTableAdapters.GetFacturasDisponiblesTableAdapter();
            TA.Fill(DT, IdUser);
            if (DT.Rows.Count != 0) 
            {
                string listadofacturas="";
                for (int a = 0; a < DT.Rows.Count; a++) 
                {
                    listadofacturas = listadofacturas + DT.Rows[a][0].ToString();
                    if (a < DT.Rows.Count - 1)
                    {
                        listadofacturas = listadofacturas + ",";
                    }
                    

                }
                return listadofacturas;
            } else { return "null"; }
        }

        public string GetPrivateKeyByIdUser(int IdUser)
        {
            GestionDataSet.GetKeyUserDataTable DT = new GestionDataSet.GetKeyUserDataTable();
            GestionDataSetTableAdapters.GetKeyUserTableAdapter TA = new GestionDataSetTableAdapters.GetKeyUserTableAdapter();
            TA.Fill(DT, IdUser);
            if (DT.Rows.Count != 0)
            {
                try
                {
                    return DT[0][1].ToString();
                }
                catch
                {
                    return "null";
                }
            }
            else
            {
                return "null";
            }
        }

        public int GetUserByPrivateKey(string PrivateKey) 
        {
            GestionDataSet.GetIdUserDataTable DT = new GestionDataSet.GetIdUserDataTable();
            GestionDataSetTableAdapters.GetIdUserTableAdapter TA = new GestionDataSetTableAdapters.GetIdUserTableAdapter();
            TA.Fill(DT, PrivateKey);
            if (DT.Rows.Count != 0)
            {
                try
                {
                    return int.Parse(DT[0][0].ToString());
                } catch
                {
                    return 0;
                }
            }
            else 
            {
                return 0;
            }
        }
    }
}
