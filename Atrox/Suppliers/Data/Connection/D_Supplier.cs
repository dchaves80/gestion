using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Connection
{
    public static class D_Supplier
    {

        public static DataTable Get_AllNames(int p_Iduser) 
        {
            GestionDataSet.select_allSupliersNamesDataTable DT = new GestionDataSet.select_allSupliersNamesDataTable();
            GestionDataSetTableAdapters.select_allSupliersNamesTableAdapter TA = new GestionDataSetTableAdapters.select_allSupliersNamesTableAdapter();
            TA.Fill(DT, p_Iduser);
            if (DT.Rows.Count > 0)
            {
                return DT;
            }
            else 
            {
                return null;
            }
        }

       public static DataTable Get_AllShort(int p_IdUser)
       {
           
           

           GestionDataSet.select_allSuppliersDataTable DT = new GestionDataSet.select_allSuppliersDataTable();
           GestionDataSetTableAdapters.select_allSuppliersTableAdapter TA = new GestionDataSetTableAdapters.select_allSuppliersTableAdapter();
           TA.Fill(DT, p_IdUser);
           if (DT.Rows.Count > 0)
           {
               return DT;
           }
           else 
           {
               return null;
           }
       }



       public static DataRow Get_SingleSupplier(int p_IdUser, int p_IdSupplier) 
       {
           GestionDataSet.select_SingleSupplierDataTable DT = new GestionDataSet.select_SingleSupplierDataTable();
           GestionDataSetTableAdapters.select_SingleSupplierTableAdapter TA = new GestionDataSetTableAdapters.select_SingleSupplierTableAdapter();
           TA.Fill(DT, p_IdUser, p_IdSupplier);
           if (DT.Rows.Count > 0)
           {
               return DT.Rows[0];
           }
           else 
           {
               return null;
           }

           

       }

    }
}
