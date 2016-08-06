using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Connection
{
    public class D_PrinterConfig
    {
        public bool ConfirmCommand(int Iduser, int IdCommand) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            int MODIFY = QTA.DeletePrintActionById(Iduser, IdCommand);
            if (MODIFY > 0)
            {
                return true;

            }
            else 
            {
                return false;
            }
        }

        public DataRow GetPrinterAction(int IdUser) 
        {
            GestionDataSetTableAdapters.GetPrintActionByIdUserTableAdapter TA = new GestionDataSetTableAdapters.GetPrintActionByIdUserTableAdapter();
            GestionDataSet.GetPrintActionByIdUserDataTable DT = new GestionDataSet.GetPrintActionByIdUserDataTable();
            TA.Fill(DT, IdUser);
            if (DT.Rows.Count > 0)
            {
                return DT.Rows[0];
            }
            else 
            {
                return null;
            }
        }
       
        public bool InsertPrintAction(int IdUser, PublicsEnum.Enum_Printer.PrintActions p_ENUM)
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            int MODIFY = QTA.InsertPrintAction(IdUser,p_ENUM.ToString());
            if (MODIFY > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }

        }

        public DataRow getPrintConfiguration(int IdUser) 
        {
            GestionDataSetTableAdapters.GetPrintConfigurationByIdUserTableAdapter TA = new GestionDataSetTableAdapters.GetPrintConfigurationByIdUserTableAdapter();
            GestionDataSet.GetPrintConfigurationByIdUserDataTable DT = new GestionDataSet.GetPrintConfigurationByIdUserDataTable();
            TA.Fill(DT, IdUser);
            if (DT.Rows.Count > 0)
            {
                return DT.Rows[0];
            }
            else 
            {
                return null;
            }
        }

        public bool insertPrintConfiguration(int IdUser, string Puerto, int Baudios, string Modelo) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            int change = QTA.InsertPrintConfiguration(IdUser, Puerto, Baudios, Modelo);
            if (change != 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool updatePrintConfiguration(int IdUser, string Puerto, int Baudios, string Modelo) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            int change = QTA.UpdatePrintConfigurationByIdUser(IdUser, Puerto, Baudios, Modelo);
            if (change != 0)
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
