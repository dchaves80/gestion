using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Class
{
    public class Struct_TipoDocumento
    {

        int id;
        int IdTipoDocumento;
        string nombre;

        public Struct_TipoDocumento(int p_id, int p_IdTipoDocumento, string p_nombre)
        {
            id = p_id;
            IdTipoDocumento = p_IdTipoDocumento;
            nombre = p_nombre;
        }
        public int get_Id() { return id; }
        public int get_IdTipoDocumento() { return IdTipoDocumento; }
        public string get_Nombre() { return nombre; }

        public static List<Struct_TipoDocumento> GetAll() 
        {

            List<String> t_stringlist = Connection.D_TipoDocumento.getALL();

            if (t_stringlist != null)
            {
                List<Struct_TipoDocumento> T_AFIPList = new List<Struct_TipoDocumento>();
                for (int a = 0; a < t_stringlist.Count; a++)
                {
                    string[] splitter = { "," };
                    string[] splitted = t_stringlist[a].Split(splitter, StringSplitOptions.None);
                    int t_id = int.Parse(splitted[0]);
                    int t_idTipoDocumento = int.Parse(splitted[1]);
                    string t_nombre = splitted[2];
                    T_AFIPList.Add(new Struct_TipoDocumento(t_id, t_idTipoDocumento, t_nombre));
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
