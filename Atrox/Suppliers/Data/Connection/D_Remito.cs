using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Connection
{
    public class D_Remito
    {

        public bool insert_DetalleRemito(int IdRemito, int IdArt, decimal costo, decimal cantdec, int cantint,  decimal total) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
           int conf =  QTA.Insert_DetalleRemito(IdRemito, IdArt, costo, cantdec, cantint, total);
           if (conf != 0)
           {
               return true;
           }
           else 
           {
               return false;
           }
        }

        public System.Data.DataTable Select_allRemitos(int UserID) 
        {
            GestionDataSet.Select_AllRemitosDataTable DT = new GestionDataSet.Select_AllRemitosDataTable();
            GestionDataSetTableAdapters.Select_AllRemitosTableAdapter TA = new GestionDataSetTableAdapters.Select_AllRemitosTableAdapter();
            TA.Fill(DT, UserID);
            if (DT.Rows.Count != 0)
            {
                return DT;
            }
            else 
            {
                return null;
            }
        }

        public int insert_Remito(int IdUser,int IdProveedor,string NroRemito, DateTime Fecha, decimal total) 
        {
            GestionDataSet.Insert_RemitoDataTable DT = new GestionDataSet.Insert_RemitoDataTable();
            GestionDataSetTableAdapters.Insert_RemitoTableAdapter TA = new GestionDataSetTableAdapters.Insert_RemitoTableAdapter();
            string sqlfecha = Data2.Statics.Conversion.DateTimeToSql(Fecha);
            TA.Fill(DT, IdUser, IdProveedor, NroRemito, sqlfecha, total);
            if (DT.Rows.Count > 0)
            {
                return int.Parse(DT.Rows[0][0].ToString());
            }
            else 
            {
                return 0;
            }

        }
    }
}
