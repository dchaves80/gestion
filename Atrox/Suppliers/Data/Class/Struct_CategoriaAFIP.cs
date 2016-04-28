using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Class
{
    public class Struct_CategoriaAFIP
    {

        int id;
        int IdCategoriaAFIP;
        string nombre;

        public Struct_CategoriaAFIP(int p_id, int p_IdCategoriaAFIP, string p_nombre)
        {
            id = p_id;
            IdCategoriaAFIP = p_IdCategoriaAFIP;
            nombre = p_nombre;
        }
        public int get_Id() { return id; }
        public int get_IdCategoriaAFIP() { return IdCategoriaAFIP; }
        public string get_Nombre() { return nombre; }

        public static List<Struct_CategoriaAFIP> GetAll() 
        {

            List<String> t_stringlist = Connection.D_CategoriaAFIP.getALL();

            if (t_stringlist != null)
            {
                List<Struct_CategoriaAFIP> T_AFIPList = new List<Struct_CategoriaAFIP>();
                for (int a = 0; a < t_stringlist.Count; a++)
                {
                    string[] splitter = { "," };
                    string[] splitted = t_stringlist[a].Split(splitter, StringSplitOptions.None);
                    int t_id = int.Parse(splitted[0]);
                    int t_idCategoriaAFIP = int.Parse(splitted[1]);
                    string t_nombre = splitted[2];
                    T_AFIPList.Add(new Struct_CategoriaAFIP(t_id, t_idCategoriaAFIP,t_nombre));
                }
                return T_AFIPList;
            }
            else
            {
                return null;
            };
        }

    }
}
