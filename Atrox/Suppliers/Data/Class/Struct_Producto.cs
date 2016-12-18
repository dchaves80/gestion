using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data2.Statics;



namespace Data2.Class
{
    [Serializable]
    public class Struct_Producto
    {

        
        
        public int Id;
        public int IdUser;
        public int IdProveedor;
        public string CodigoInterno;
        public string CodigoBarra;
        public string Descripcion;
        public decimal PrecioNeto;
        public decimal IVA;
        public decimal PrecioCompra;
        public decimal PorcentajeGanancia;
        public decimal PrecioFinal;
        public int IdUnidad;
        public int CantidadINT;
        public decimal CantidadDEC;



        public class PagingArticles 
        {
            public List<Struct_Producto> Listado;
            public int NumeroDePaginas;

            public PagingArticles(List<Struct_Producto> p_List, int p_NumeroPaginas) 
            {
                Listado = p_List;
                NumeroDePaginas = p_NumeroPaginas;
            }

        }


        public class ProductDATASET 
        {
            public List<Struct_Producto> Listado;
            public DataTable MyDATATABLE;
            public ProductDATASET(List<Struct_Producto> p_List, DataTable p_MyDATATABLE) 
            {
                if (Listado == null) 
                {
                    Listado = new List<Struct_Producto>();
                }
                Listado = p_List;
                MyDATATABLE = p_MyDATATABLE;
            }
        }


        public Struct_Producto
            (
            
            int p_IdUser,
            int p_IdProveedor,
            string p_CodigoInterno,
            string p_CodigoBarra,
            string p_Descripcion,
            decimal p_PrecioNeto,
            decimal p_IVA,
            decimal p_PrecioCompra,
            decimal p_PorcentajeGanancia,
            decimal p_PrecioFinal,
            int p_IdUnidad
           
            ) 
        {

            
            IdUser = p_IdUser;
            IdProveedor = p_IdProveedor;
            CodigoInterno = p_CodigoInterno;
            CodigoBarra = p_CodigoBarra;
            Descripcion = p_Descripcion;
            PrecioNeto = p_PrecioNeto;
            IVA = p_IVA;
            PrecioCompra = p_PrecioCompra;
            PorcentajeGanancia = p_PorcentajeGanancia;
            PrecioFinal = p_PrecioFinal;
            IdUnidad = p_IdUnidad;
          




        }



        public Struct_Producto
           (
            int p_ID,
           int p_IdUser,
           int p_IdProveedor,
           string p_CodigoInterno,
           string p_CodigoBarra,
           string p_Descripcion,
           decimal p_PrecioNeto,
           decimal p_IVA,
           decimal p_PrecioCompra,
           decimal p_PorcentajeGanancia,
           decimal p_PrecioFinal,
           int p_IdUnidad,
            decimal p_CantDecimal,
            int p_CantInteger

           )
        {

            Id = p_ID;
            IdUser = p_IdUser;
            IdProveedor = p_IdProveedor;
            CodigoInterno = p_CodigoInterno;
            CodigoBarra = p_CodigoBarra;
            Descripcion = p_Descripcion;
            PrecioNeto = p_PrecioNeto;
            IVA = p_IVA;
            PrecioCompra = p_PrecioCompra;
            PorcentajeGanancia = p_PorcentajeGanancia;
            PrecioFinal = p_PrecioFinal;
            IdUnidad = p_IdUnidad;
            CantidadINT = p_CantInteger;
            CantidadDEC = p_CantDecimal;
            
            




        }


        public bool Borrar(int IdUser) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            int t_int = QTA.delete_article(Id, IdUser);
            if (t_int == 1)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool Actualizar(int p_IdUSER) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            int Cant = QTA.update_ArticleData(Id, p_IdUSER, IdProveedor, CodigoInterno, CodigoBarra, Descripcion, PrecioNeto, IVA, PrecioCompra, PorcentajeGanancia, PrecioFinal, IdUnidad);
            if (Cant == 1)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        


        public bool Guardar()
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            if (
            QTA.insert_Articulo(
            IdUser, 
            IdProveedor, 
            CodigoInterno,
            CodigoBarra, 
            Descripcion, 
            PrecioNeto, 
            IVA, 
            PrecioCompra, 
            PorcentajeGanancia, 
            PrecioFinal, 
            IdUnidad) > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }


        public bool? ActualizarPrecios() 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            int Changes = QTA.update_articlePrice(IdUser, Id, PrecioNeto, IVA, PrecioCompra, PorcentajeGanancia, PrecioFinal);
            if (Changes == 1)
            {
                return true;
            }
            else 
            {
                if (Changes == 0)
                {
                    return false;
                }
                else 
                {
                    return null;
                }
            }
            
            
            
        }

        public static Struct_Producto DataRowToProduct(DataRow p_MyDR)
        {

            DataRow _DR = p_MyDR;
            if (_DR != null)
            {
                int _id = int.Parse(_DR["Id"].ToString());
                int _idUser = int.Parse(_DR["IdUser"].ToString());
                int _idProveedor = int.Parse(_DR["IdProveedor"].ToString());
                string _CodigoInterno = _DR["CodigoInterno"].ToString();
                string _CodigoBarra = _DR["CodigoInterno"].ToString();
                string _Descripcion = _DR["Descripcion"].ToString();
                decimal _PrecioNeto = Statics.Conversion.GetDecimal(_DR["PrecioNeto"].ToString());
                decimal _IVA = Statics.Conversion.GetDecimal(_DR["IVA"].ToString());
                decimal _PrecioCompra = Statics.Conversion.GetDecimal(_DR["PrecioCompra"].ToString());
                decimal _PorcentajeGanancia = Statics.Conversion.GetDecimal(_DR["PorcentajeGanancia"].ToString());
                decimal _PrecioFinal = Statics.Conversion.GetDecimal(_DR["PrecioFinal"].ToString());
                int _IdUnidad = int.Parse(_DR["IdUnidad"].ToString());
                int _CantInt = int.Parse(_DR["CantidadINT"].ToString());
                decimal _CantDec = Statics.Conversion.GetDecimal( _DR["CantidadDEC"].ToString());
                Struct_Producto _Product = new Struct_Producto(_id, _idUser, _idProveedor, _CodigoInterno, _CodigoBarra, _Descripcion, _PrecioNeto, _IVA, _PrecioCompra, _PorcentajeGanancia, _PrecioFinal, _IdUnidad,_CantDec,_CantInt);
                return _Product;

            }
            else { return null; }


        }

        public static Struct_Producto SelectSingleArticle(int p_IdUser, int p_IdProveedor, string p_CodigoInterno) 
        {

            DataRow _DR =  Connection.D_Articles.SelectSingleArticle(p_IdUser, p_IdProveedor, p_CodigoInterno);
            if (_DR != null) 
            {
                int _id = int.Parse(_DR["Id"].ToString());
                int _idUser = int.Parse(_DR["IdUser"].ToString());
                int _idProveedor = int.Parse(_DR["IdProveedor"].ToString());
                string _CodigoInterno = _DR["CodigoInterno"].ToString();
                string _CodigoBarra = _DR["CodigoInterno"].ToString();
                string _Descripcion = _DR["Descripcion"].ToString();
                decimal _PrecioNeto = Statics.Conversion.GetDecimal(_DR["PrecioNeto"].ToString());
                decimal _IVA = Statics.Conversion.GetDecimal(_DR["IVA"].ToString());
                decimal _PrecioCompra = Statics.Conversion.GetDecimal(_DR["PrecioCompra"].ToString());
                decimal _PorcentajeGanancia = Statics.Conversion.GetDecimal(_DR["PorcentajeGanancia"].ToString());
                decimal _PrecioFinal = Statics.Conversion.GetDecimal(_DR["PrecioFinal"].ToString());
                int _IdUnidad = int.Parse(_DR["IdUnidad"].ToString());
                int _CantInt = int.Parse(_DR["CantidadINT"].ToString());
                decimal _CantDec = Statics.Conversion.GetDecimal(_DR["CantidadDEC"].ToString());
                Struct_Producto _Product = new Struct_Producto(_id, _idUser, _idProveedor, _CodigoInterno, _CodigoBarra, _Descripcion, _PrecioNeto, _IVA, _PrecioCompra, _PorcentajeGanancia, _PrecioFinal, _IdUnidad,_CantDec,_CantInt);
                return _Product;

            } else { return null; }


        }

        public bool UpdateStock(string p_newcant) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            
            Struct_Unidades UNIT = new Struct_Unidades(IdUnidad);
            int cantcambios = 0;
            if (UNIT.Decimal == true)
            {

                cantcambios = QTA.update_ArticleCantidad(IdUser, Id, 0, Conversion.GetDecimal(p_newcant), true);


            }
            else 
            {
                cantcambios = QTA.update_ArticleCantidad(IdUser, Id, int.Parse(p_newcant), 0m, false);
            }

            if (cantcambios == 0)
            {
                return false;
            }
            else 
            {
                return true;
            }

        }

        public static List<Struct_Producto> SearchProducto(int p_IdUser, string p_SearchString, Connection.D_Articles.SearchCondition p_SearchCondition, int IdProvider=-1) 
        {
            DataTable DTResult = Connection.D_Articles.SearchArticle(p_IdUser, p_SearchString, p_SearchCondition,IdProvider);

            if (DTResult!=null && DTResult.Rows.Count > 0)
            {
                List<Struct_Producto> _list = new List<Struct_Producto>();
                foreach (DataRow R in DTResult.Rows)
                {
                    _list.Add(DataRowToProduct(R));
                }
                return _list;
            }
            else 
            {
                return null;
            }
        }

        public static Struct_Producto Get_SingleArticle(int p_IdUser, int p_IdArticle) 
        {
            DataRow DR = Connection.D_Articles.SelectSingleArticle(p_IdUser, p_IdArticle);
            if (DR != null) 
            {
                return DataRowToProduct(DR);
            } else 
            {
                return null;
            }
        }

        public static Struct_Producto Get_SingleArticle(int p_IdUser, string Codes)
        {
            DataRow DR = Connection.D_Articles.SelectSingleArticle(p_IdUser, Codes);
            if (DR != null)
            {
                return DataRowToProduct(DR);
            }
            else
            {
                return null;
            }
        }

        public static ProductDATASET Get_ArticleFORSTOCK(int p_IdUser, int p_IdProvider, string p_Code, string p_Name) 
        {
           DataTable DT =  Connection.D_Articles.SelectArticlesForStock(p_IdUser, p_IdProvider, p_Code, p_Name);

           if (DT != null) 
           {
               List<Struct_Producto> t_list = new List<Struct_Producto>();

               for (int a = 0; a < DT.Rows.Count; a++) 
               {
                   t_list.Add((DataRowToProduct(DT.Rows[a])));

               }

               ProductDATASET PDS = new ProductDATASET(t_list, DT);
               Statics.Log.ADD("Listado registros:" + PDS.Listado.Count.ToString(), null);
               return PDS;
           } 
           else 
           { return null; }

        }

        public static PagingArticles GetArticlesPaging(string p_Letter, int p_Page, int p_Limit, int p_IdUser, int p_IdProvider) 
        {
            Connection.D_Articles.PagingInfo _PI = Connection.D_Articles.GetArticlesPaging(p_Letter, p_Page, p_Limit, p_IdUser, p_IdProvider);
            List<Struct_Producto> ListadoProductos = new List<Struct_Producto>();
            if (_PI.DATA != null && _PI.DATA.Rows.Count > 0)
            {


                for (int a = 0; a < _PI.DATA.Rows.Count; a++)
                {
                    int _id = int.Parse(_PI.DATA.Rows[a]["Id"].ToString());
                    int _idUser = int.Parse(_PI.DATA.Rows[a]["IdUser"].ToString());
                    int _idProveedor = int.Parse(_PI.DATA.Rows[a]["IdProveedor"].ToString());
                    string _CodigoInterno = _PI.DATA.Rows[a]["CodigoInterno"].ToString();
                    string _CodigoBarra = _PI.DATA.Rows[a]["CodigoInterno"].ToString();
                    string _Descripcion = _PI.DATA.Rows[a]["Descripcion"].ToString();
                    decimal _PrecioNeto = Statics.Conversion.GetDecimal( _PI.DATA.Rows[a]["PrecioNeto"].ToString());
                    decimal _IVA = Statics.Conversion.GetDecimal(_PI.DATA.Rows[a]["IVA"].ToString());
                    decimal _PrecioCompra = Statics.Conversion.GetDecimal(_PI.DATA.Rows[a]["PrecioCompra"].ToString());
                    decimal _PorcentajeGanancia = Statics.Conversion.GetDecimal(_PI.DATA.Rows[a]["PorcentajeGanancia"].ToString());
                    decimal _PrecioFinal = Statics.Conversion.GetDecimal(_PI.DATA.Rows[a]["PrecioFinal"].ToString());
                    int _IdUnidad = int.Parse(_PI.DATA.Rows[a]["IdUnidad"].ToString());
                    int _CantInt = int.Parse(_PI.DATA.Rows[a]["CantidadINT"].ToString());
                    decimal _CantDec = Statics.Conversion.GetDecimal(_PI.DATA.Rows[a]["CantidadDEC"].ToString());

                    ListadoProductos.Add(new Struct_Producto(_id, _idUser, _idProveedor, _CodigoInterno, _CodigoBarra, _Descripcion, _PrecioNeto, _IVA, _PrecioCompra, _PorcentajeGanancia, _PrecioFinal, _IdUnidad,_CantDec,_CantInt));



                }

                PagingArticles PA = new PagingArticles(ListadoProductos, _PI.CantidadPaginas);
                return PA;

            }
            else 
            {
                return null;
            }
        }

        public static string GetProductsCodeByCode(string p_chain, int p_userid, int p_TOP)
        {
            GestionDataSet.select_top5productsByCodigoDataTable DT = new GestionDataSet.select_top5productsByCodigoDataTable();
            GestionDataSetTableAdapters.select_top5productsByCodigoTableAdapter TA = new GestionDataSetTableAdapters.select_top5productsByCodigoTableAdapter();

            TA.Fill(DT, p_userid, p_TOP, p_chain);
            if (DT.Rows.Count > 0)
            {

                string _mystring = "";
                for (int a = 0; a < DT.Rows.Count; a++)
                {

                    _mystring = _mystring + DT.Rows[a]["CodigoInterno"].ToString();
                    if (a != DT.Rows.Count - 1)
                    {
                        _mystring = _mystring + "/";
                    }
                }
                return _mystring;

            }
            else
            {
                return "null";
            }


        }

        public static string GetProductsNameByName(string p_chain, int p_userid, int p_TOP)
        {
            GestionDataSet.select_top5productsByNameDataTable DT = new GestionDataSet.select_top5productsByNameDataTable();
            GestionDataSetTableAdapters.select_top5productsByNameTableAdapter TA = new GestionDataSetTableAdapters.select_top5productsByNameTableAdapter();

            TA.Fill(DT, p_userid, p_TOP, p_chain);
            if (DT.Rows.Count > 0)
            {

                string _mystring = "";
                for (int a = 0; a < DT.Rows.Count; a++) 
                {

                    _mystring = _mystring + DT.Rows[a]["Descripcion"].ToString();
                    if (a != DT.Rows.Count - 1) 
                    {
                        _mystring = _mystring + "/";
                    }
                }
                return _mystring;

            }
            else 
            {
                return "null";
            }

            
        }


    }
}
