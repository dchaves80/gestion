using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Data2.Class
{
    public class Struct_UserConfig
    {
        public static Struct_UserConfig getUserConfig(int Iduser) 
        {
            Connection.D_UserConfig UC = new Connection.D_UserConfig();
            DataRow DR = UC.Get_UserConfig(Iduser);

            if (DR != null) 
            {
                return new Struct_UserConfig(DR);

            } else return null;
        }

        public int Id;
        public int IdUser;
        public string NombreNegocio;
        public bool MostrarLogoNegocio;
        public string FacturaPorDefecto;
        public string PIN;
        public bool MostrarKiosco;
        
        public bool Guardar(int p_IdUser)
        {
            if (IdUser != 0 && Id != 0)
            {
                Connection.D_UserConfig D = new Connection.D_UserConfig();
                return D.Update_UserConfig(IdUser, NombreNegocio, MostrarLogoNegocio, FacturaPorDefecto, PIN, MostrarKiosco);


            }
            else 
            {
                Connection.D_UserConfig D = new Connection.D_UserConfig();
                IdUser = p_IdUser;
                return  D.Insert_UserConfig(IdUser, NombreNegocio, MostrarLogoNegocio, FacturaPorDefecto, PIN, MostrarKiosco);
            }
        }

        public Struct_UserConfig() 
        {

        }

        public Struct_UserConfig(DataRow UC) 
        {
            Id = int.Parse(UC["Id"].ToString());
            IdUser = int.Parse(UC["IdUser"].ToString());
            NombreNegocio = UC["NombreNegocio"].ToString();
            MostrarLogoNegocio = Statics.Conversion.convertSQLToBoolean(UC["MostrarLogoNegocio"]);
            MostrarKiosco = Statics.Conversion.convertSQLToBoolean(UC["Kiosco"]);
            FacturaPorDefecto = UC["FacturaPorDefecto"].ToString();
            PIN = UC["PIN"].ToString();


        }

    }
}
