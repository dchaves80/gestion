using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;


namespace Data2.Connection
{
    public class D_UserConfig
    {



        public DataRow Get_UserConfig(int IdUser)
        {
            GestionDataSet.GET_UserConfiguracionDataTable DT = new GestionDataSet.GET_UserConfiguracionDataTable();
            GestionDataSetTableAdapters.GET_UserConfiguracionTableAdapter TA = new GestionDataSetTableAdapters.GET_UserConfiguracionTableAdapter();
            TA.Fill(DT, IdUser);
            if (DT.Rows.Count > 0) 
            {
                return DT.Rows[0];
            } else 
            {
                return null;
            }
        }

        public bool Update_UserConfig(int IdUser, string nombreNegocio, bool MostrarLogo, string facturaPorDefecto, string PIN, bool Kiosco)
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            if (QTA.UPDATE_UserConfiguration(IdUser, nombreNegocio, MostrarLogo, facturaPorDefecto, PIN, Kiosco) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Insert_UserConfig(int IdUser, string nombreNegocio,bool MostrarLogo, string facturaPorDefecto, string PIN, bool Kiosco) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            if (QTA.InsertUserConfiguration(IdUser, nombreNegocio, MostrarLogo, facturaPorDefecto, PIN, Kiosco) > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }

        }

    }
}

