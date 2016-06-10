using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Class
{
    public class Struct_Unidades
    {
       public int Id;
       public string Nombre;
       public string Simbolo;
       public bool DisplaySimbol;
       public bool Decimal;

      

       public Struct_Unidades(int p_Id) 
       {
           DataRow DR = Connection.D_Unidades.GetSingleByID(p_Id);
           if (DR != null) 
           {
               Id = int.Parse(DR["Id"].ToString());
               Nombre = DR["Nombre"].ToString();
               Simbolo = DR["Simbolo"].ToString();
               DisplaySimbol = convertSQLToBoolean(DR["DisplaySimbol"]);
               Decimal = convertSQLToBoolean(DR["DisplaySimbol"]);
               
           }
       }

        public Struct_Unidades(DataRow p_DR)
        {
             if (p_DR != null) 
           {
               Id = int.Parse(p_DR["Id"].ToString());
               Nombre = p_DR["Nombre"].ToString();
               Simbolo = p_DR["Simbolo"].ToString();
               DisplaySimbol = convertSQLToBoolean(p_DR["DisplaySimbol"]);
               Decimal = convertSQLToBoolean(p_DR["DisplaySimbol"]);
               
           }
        }

       public Struct_Unidades(
       int p_Id,
       string p_Nombre,
       string p_Simbolo,
       bool p_DisplaySimbol,
       bool p_Decimal) 
       {
           Id = p_Id;
           Nombre = p_Nombre;
           Simbolo = p_Simbolo;
           DisplaySimbol = p_DisplaySimbol;
           Decimal = p_Decimal;
       }

    public static List<Struct_Unidades> GetAll()
    {
        DataTable t_DT = Connection.D_Unidades.GetAll();
        if (t_DT != null)
        {
            List<Struct_Unidades> MyUnitsLists = new List<Struct_Unidades>();
            for (int a = 0; a < t_DT.Rows.Count; a++) 
            {
                MyUnitsLists.Add(new Struct_Unidades(t_DT.Rows[a]));
            }
            return MyUnitsLists;
        }
        else 
        {
            return null;
        }
    }

       public static bool convertSQLToBoolean(object p_MySQLBOOLEAN)
       {
           string result = p_MySQLBOOLEAN.ToString().ToLower();
           switch (result)
           {
               case "1":
               case "true": { return true; }
               case "0":
               case "false": { return false; }
               default: return false;
           }
       }
    }
}
