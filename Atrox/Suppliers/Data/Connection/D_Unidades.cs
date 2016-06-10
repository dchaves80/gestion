using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Data2.Connection
{
    public static class D_Unidades
    {
        public static DataTable GetAll() 
        {
            GestionDataSet.select_allUnidadesDataTable DT = new GestionDataSet.select_allUnidadesDataTable();
            GestionDataSetTableAdapters.select_allUnidadesTableAdapter TA = new GestionDataSetTableAdapters.select_allUnidadesTableAdapter();
            TA.Fill(DT);
            if (DT.Rows.Count > 0)
            {
                return DT;
            }
            else 
            {
                return null;
            }
            
        }

        public static DataRow GetSingleByID(int p_idUnit) 
        {
            GestionDataSet.select_singleUnidadesDataTable DT = new GestionDataSet.select_singleUnidadesDataTable();
            GestionDataSetTableAdapters.select_singleUnidadesTableAdapter TA = new GestionDataSetTableAdapters.select_singleUnidadesTableAdapter();
            TA.Fill(DT,p_idUnit);
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
