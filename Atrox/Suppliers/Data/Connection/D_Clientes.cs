using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Connection
{
    public class D_Clientes
    {
        public bool actualizarCliente(string p_RS,
            string p_DNI,
            string p_PAIS,
            string p_PROVINCIA,
            string p_LOCALIDAD,
            string p_DOMICILIO,
            string p_OBSERVACIONES,
            string p_TIPOIVA,
            decimal p_DESCUENTO,
            string p_EMAIL,
            int p_IDUSER,
            int p_ID)
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            int reg = QTA.Update_Cliente(p_ID,p_RS, p_DNI, p_DNI, p_PROVINCIA, p_LOCALIDAD, p_DOMICILIO, p_OBSERVACIONES, p_TIPOIVA, p_DESCUENTO, p_EMAIL, p_IDUSER);
            if (reg != 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public DataTable Search_Cliente(string searchstring, int IDUSER) 
        {
            GestionDataSetTableAdapters.Search_Cliente_BeginTableAdapter TA = new GestionDataSetTableAdapters.Search_Cliente_BeginTableAdapter();
            GestionDataSet.Search_Cliente_BeginDataTable DT = new GestionDataSet.Search_Cliente_BeginDataTable();
            TA.Fill(DT, IDUSER, searchstring);
            if (DT.Rows.Count > 0)
            {
                return DT;
            }
            else 
            {
                return null;
            }
        }


        public int insertarCliente
            (
            string p_RS,
            string p_DNI,
            string p_PAIS,
            string p_PROVINCIA,
            string p_LOCALIDAD,
            string p_DOMICILIO,
            string p_OBSERVACIONES,
            string p_TIPOIVA,
            decimal p_DESCUENTO,
            string p_EMAIL,
            int p_IDUSER
            ) 
        {
            GestionDataSetTableAdapters.Insert_ClienteTableAdapter TA = new GestionDataSetTableAdapters.Insert_ClienteTableAdapter();
            GestionDataSet.Insert_ClienteDataTable DT = new GestionDataSet.Insert_ClienteDataTable();
            TA.Fill(DT, p_RS, p_DNI, p_PAIS, p_PROVINCIA, p_LOCALIDAD, p_DOMICILIO, p_OBSERVACIONES, p_TIPOIVA, p_DESCUENTO, p_EMAIL, p_IDUSER);
            int reg = 0;
            if (DT!=null && DT.Rows.Count>0)
            {
                reg = int.Parse(DT.Rows[0]["Id"].ToString());
            }
            
            if (reg != 0)
            {
                return reg;
            }
            else 
            {
                return 0;
            }

        }
    }
}
