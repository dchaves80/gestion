using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Connection
{
    public static class D_CategoriaAFIP
    {
        public static List<string> getALL()
        {
            GestionDataSet.select_allCategoriaAFIPDataTable DT = new GestionDataSet.select_allCategoriaAFIPDataTable();
            GestionDataSetTableAdapters.select_allCategoriaAFIPTableAdapter TA = new GestionDataSetTableAdapters.select_allCategoriaAFIPTableAdapter();
            TA.Fill(DT);
            if (DT.Rows.Count > 0)
            {
                List<string> t_list = new List<string>();
                for (int a = 0; a < DT.Rows.Count; a++)
                {
                    t_list.Add(
                        DT.Rows[a]["Id"].ToString() + "," 
                        + DT.Rows[a]["IdCategoriaAFIP"].ToString()
                        + ","
                        + DT.Rows[a]["Nombre"].ToString());
                }
                return t_list;
            }
            else
            {
                return null;
            }
        }
    }
}
