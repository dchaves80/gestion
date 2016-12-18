using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Connection
{
    public static class D_Articles
    {

        

        public class PagingInfo
        {
            public DataTable DATA;
            public int CantidadPaginas;
            

            public PagingInfo(DataTable p_DATA, int p_CantidadPaginas)
            {
                DATA = p_DATA;
                CantidadPaginas = p_CantidadPaginas;
            }

        }

        public enum SearchCondition {PorDescripcion, PorCodigoInterno, PorCodigoBarra}

        public static DataTable SearchArticle(int p_iduser, string p_searchstring, SearchCondition p_searchcondition, int IdProvider=-1) 
        {
            string strsearchcondition;
            switch (p_searchcondition) 
            {
                case SearchCondition.PorCodigoBarra:
                    strsearchcondition = "codb";
                    break;
                case SearchCondition.PorCodigoInterno:
                    strsearchcondition = "codi";
                    break;
                case SearchCondition.PorDescripcion:
                    strsearchcondition = "desc";
                    break;
                default:
                    strsearchcondition = "desc";
                    break;
            }
            GestionDataSetTableAdapters.SearchArticleTableAdapter TA = new GestionDataSetTableAdapters.SearchArticleTableAdapter();
            GestionDataSet.SearchArticleDataTable DT = new GestionDataSet.SearchArticleDataTable();
            TA.Fill(DT, p_iduser, p_searchstring, strsearchcondition,IdProvider);
            if (DT.Rows.Count > 0)
            {
                return DT;
            }
            else 
            {
                return null;
            }

        }

        public static DataTable SelectArticlesForStock(int p_iduser, int p_idProvider, string p_CODEstring, string p_NAMEstring) 
        {
            GestionDataSet.select_SearchArticleForSTOCKDataTable DT = new GestionDataSet.select_SearchArticleForSTOCKDataTable();
            GestionDataSetTableAdapters.select_SearchArticleForSTOCKTableAdapter TA = new GestionDataSetTableAdapters.select_SearchArticleForSTOCKTableAdapter();
            TA.Fill(DT, p_iduser, p_CODEstring, p_NAMEstring, p_idProvider);
            if (DT.Rows.Count > 0)
            {
                Statics.Log.ADD("Statable registros:" + DT.Rows.Count.ToString(),null);
                return DT;
            }
            else 
            {
                return null;
            }
        }

        public static DataRow SelectSingleArticle(int p_IdUser, int p_IdArticle)
        {
            GestionDataSet.select_singlearticlebaseDataTable DT = new GestionDataSet.select_singlearticlebaseDataTable();
            GestionDataSetTableAdapters.select_singlearticlebaseTableAdapter TA = new GestionDataSetTableAdapters.select_singlearticlebaseTableAdapter();
            TA.Fill(DT, p_IdUser, p_IdArticle);
            if (DT.Rows.Count>0)
            {
                return DT.Rows[0];
            } else 
            {
                return null;
            }
        }

        public static DataRow SelectSingleArticle(int p_IdUser, string Codes)
        {
            GestionDataSet.select_singlearticlebaseByCodesDataTable DT = new GestionDataSet.select_singlearticlebaseByCodesDataTable();
            GestionDataSetTableAdapters.select_singlearticlebaseByCodesTableAdapter TA = new GestionDataSetTableAdapters.select_singlearticlebaseByCodesTableAdapter();
            TA.Fill(DT, p_IdUser, Codes);
            if (DT.Rows.Count > 0)
            {
                return DT.Rows[0];
            }
            else
            {
                return null;
            }
        }

        public static DataRow SelectSingleArticle(int p_IdUser,int p_IdProveedor,string p_CodigoInterno) 
        {
            GestionDataSet.select_singleArticleDataTable DT = new GestionDataSet.select_singleArticleDataTable();
            GestionDataSetTableAdapters.select_singleArticleTableAdapter TA = new GestionDataSetTableAdapters.select_singleArticleTableAdapter();
            TA.Fill(DT, p_IdUser,p_IdProveedor ,p_CodigoInterno);
            if (DT.Rows.Count == 1)
            {
                return DT.Rows[0];
            }
            else 
            {
                return null;
            }
        }

        public static PagingInfo GetArticlesPaging(string p_Letter, int p_Page, int p_Lenght, int p_IdUsuario, int p_IdProvider) 
        {
            GestionDataSet.select_allArticlesByPagingDataTable DT = new GestionDataSet.select_allArticlesByPagingDataTable();
            GestionDataSetTableAdapters.select_allArticlesByPagingTableAdapter TA = new GestionDataSetTableAdapters.select_allArticlesByPagingTableAdapter();
            TA.Fill(DT, p_Letter, p_Page, p_Lenght, p_IdUsuario, p_IdProvider);

            GestionDataSet.select_allArticlesByPagingINFODataTable DTinfo = new GestionDataSet.select_allArticlesByPagingINFODataTable();
            GestionDataSetTableAdapters.select_allArticlesByPagingINFOTableAdapter TAinfo = new GestionDataSetTableAdapters.select_allArticlesByPagingINFOTableAdapter();
            TAinfo.Fill(DTinfo, p_Letter, p_Page, p_Lenght, p_IdUsuario, p_IdProvider);

            
            PagingInfo _PI = new PagingInfo(DT,Convert.ToInt32( DTinfo.Rows[0][0].ToString()));
            return _PI;
            
           
            
        }
    }
}
