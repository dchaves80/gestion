using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Class
{
    public class Struct_Provincia
    {
        
        int id;
        string name;


        public Struct_Provincia(int p_id, string p_name) 
        {
            id = p_id;
            name = p_name;
        }

        public int getId() { return id; }
        public string getName() { return name; }

        public static List<Struct_Provincia> GetAll()
        {
            List<string> t_stringlist = Connection.D_Provincia.getALL();
            if (t_stringlist != null) 
            {
                List<Struct_Provincia> T_ProvinciasList = new List<Struct_Provincia>();
                for (int a = 0; a < t_stringlist.Count; a++) 
                {
                    string[] splitter = { "," };
                    string[] splitted = t_stringlist[a].Split(splitter,StringSplitOptions.None);
                    int t_id = int.Parse(splitted[0]);
                    string t_name = splitted[1];
                    T_ProvinciasList.Add(new Struct_Provincia(t_id,t_name));
                }
                return T_ProvinciasList;
            } else {
                return null;
            };
        }
        

    }
}
