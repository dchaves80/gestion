using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Data2.Class
{



    public class Struct_DetalleFactura
    {
        public int DETALLEINT=0;
        public decimal DETALLEDEC=0m;
        public Struct_Producto PRODUCTO;
        public string ACCESSKEY;
        public Boolean isdec;



        public void set_cant(string value) 
        {
            Struct_Unidades _U = new Struct_Unidades(PRODUCTO.IdUnidad);
            if (_U.Decimal == true)
            {
                DETALLEDEC = Statics.Conversion.GetDecimal(value);
            }
            else 
            {
                DETALLEINT = Convert.ToInt32(Statics.Conversion.GetDecimal(value));
            }
        }

        public Struct_DetalleFactura(DataRow p_DR, int p_IdUser) 
        {
            DETALLEINT = int.Parse(p_DR["CantidadINT"].ToString());
            DETALLEDEC = Statics.Conversion.GetDecimal(p_DR["CantidadDEC"].ToString());
            InitAccessKey();
            DataRow _DR = Connection.D_Articles.SelectSingleArticle(p_IdUser,int.Parse(p_DR["IdArticulo"].ToString()));
            if (_DR!=null)
            {
                PRODUCTO = Data2.Class.Struct_Producto.DataRowToProduct(_DR);

                if (PRODUCTO != null)
                {
                    PRODUCTO.PrecioCompra = Statics.Conversion.GetDecimal(p_DR["PrecioCompra"].ToString());
                    PRODUCTO.PorcentajeGanancia = Statics.Conversion.GetDecimal(p_DR["PorcentajeGanancia"].ToString());
                    PRODUCTO.PrecioFinal = Statics.Conversion.GetDecimal(p_DR["PrecioFinal"].ToString());
                    PRODUCTO.IVA = Statics.Conversion.GetDecimal(p_DR["IVA"].ToString());
                    PRODUCTO.PrecioNeto = Statics.Conversion.GetDecimal(p_DR["PrecioNeto"].ToString());
                    isdec = new Struct_Unidades(PRODUCTO.IdUnidad).Decimal;
                }
            }
            
        }

        public Struct_DetalleFactura(int IdProd, int IdUser) 
        {
            PRODUCTO = Struct_Producto.Get_SingleArticle(IdUser, IdProd);
            Struct_Unidades _U = new Struct_Unidades(PRODUCTO.IdUnidad);
            if (_U.Decimal == true)
            {
                isdec = true;
            }
            else 
            {
                isdec = false;
            }
            InitAccessKey();
            
            
        }

        public Struct_DetalleFactura(string CodeProd, int IdUser)
        {
            PRODUCTO = Struct_Producto.Get_SingleArticle(IdUser, CodeProd);
            Struct_Unidades _U = new Struct_Unidades(PRODUCTO.IdUnidad);
            if (_U.Decimal == true)
            {
                isdec = true;
            }
            else
            {
                isdec = false;
            }
            InitAccessKey();


        }

        private void InitAccessKey() 
        {
            Random R = new Random(DateTime.Now.Millisecond);
            string K = "";
            for (int a = 0; a < 20; a++) 
            {
                int MyR = R.Next(0, 21);
                string key = "";
                if (MyR == 0) key = "a";
                if (MyR == 1) key = "b";
                if (MyR == 2) key = "c";
                if (MyR == 3) key = "d";
                if (MyR == 4) key = "e";
                if (MyR == 5) key = "f";
                if (MyR == 6) key = "g";
                if (MyR == 7) key = "h";
                if (MyR == 8) key = "i";
                if (MyR == 9) key = "o";
                if (MyR == 10) key = "j";
                if (MyR == 11) key = "k";
                if (MyR == 12) key = "l";
                if (MyR == 13) key = "m";
                if (MyR == 14) key = "n";
                if (MyR == 15) key = "o";
                if (MyR == 16) key = "p";
                if (MyR == 17) key = "q";
                if (MyR == 18) key = "r";
                if (MyR == 19) key = "s";
                if (MyR == 20) key = "t";
                if (MyR == 21) key = "u";
                if (key == "") { key = "16"; }
                K = K + key;

            }
            ACCESSKEY = K + R.Next(1,5000).ToString();

        }


        public decimal getPrecioFinalSinIvaPredeterminado(decimal p_IVA) 
        {
            return (PRODUCTO.PrecioFinal / (1m + (p_IVA / 100m)));
        }

        public decimal getPrecioFinalSinIva() 
        {
         
                return (PRODUCTO.PrecioFinal / (1m + (PRODUCTO.IVA / 100m)));
           
        }

        public decimal getTotalSinIvaPred(decimal p_IvaPredeterminado)
        {
            if (isdec == true)
            {
                return (PRODUCTO.PrecioFinal / (1m + (p_IvaPredeterminado / 100m))) * DETALLEDEC;
            }
            else
            {
                return (PRODUCTO.PrecioFinal / (1m + (p_IvaPredeterminado / 100m))) * DETALLEINT;
            }
        }

        public decimal getTotalSinIva() 
        {
            if (isdec == true)
            {
                return (PRODUCTO.PrecioFinal / (1m + (PRODUCTO.IVA / 100m)))*DETALLEDEC;
            }
            else
            {
                return (PRODUCTO.PrecioFinal / (1m + (PRODUCTO.IVA / 100m)))*DETALLEINT;
            }
        }

        public decimal getTotalConIva() 
        {
            if (isdec==true)
            {
                return PRODUCTO.PrecioFinal * DETALLEDEC;
            }
            else 
            {
                return PRODUCTO.PrecioFinal * DETALLEINT;
            }
        }



        public void SetearCantidad(int Cantidad)
        {
            DETALLEINT = Cantidad;
        }

        public void SetearCantidad(decimal Cantidad)
        {
            DETALLEDEC = Cantidad;
        }

    }
}
