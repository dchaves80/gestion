using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data2.Class
{
    [Serializable]
    public class Struct_PrintConfiguration
    {
       
        public int Id;
        public int IdUser;
        public string Puerto;
        public string Modelo;
        public int Baudios;

        public Struct_PrintConfiguration() { }

        public Struct_PrintConfiguration
            (
        int p_IdUser,
        string p_Puerto,
        string p_Modelo,
        int p_Baudios
            )
    {
        IdUser = p_IdUser;
        Puerto = p_Puerto;
        Modelo = p_Modelo;
        Baudios = p_Baudios;
    }

        public static string ProcessString(string a)
        {
            string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            string b = a;
            if (b.StartsWith(_byteOrderMarkUtf8))
            {
                b = b.Remove(0, _byteOrderMarkUtf8.Length);
            }
            b = b.Replace("\\", "");
            b = b.Replace("rn", "");

            b = b.Remove(b.Length - 1);
            return b;

        }
        public static Struct_PrintConfiguration Deserealize(string p_XML) 
        {
            XmlSerializer Ser = new XmlSerializer(typeof(Struct_PrintConfiguration));
            StringReader SR = new StringReader(p_XML);
            Struct_PrintConfiguration F = null;
            try
            {
                F = (Struct_PrintConfiguration)Ser.Deserialize(SR);
            }
            catch
            {
                F = Deserealize(ProcessString(p_XML));
            }

            return F;
        }

        public string GetSerializad()
        {

            XmlSerializer XMLS = new XmlSerializer(this.GetType());
            StringWriter SW = new StringWriter();
            XMLS.Serialize(SW, this);
            SW.Close();
            return SW.ToString();
        }

        public static Struct_PrintConfiguration GetPrintConfiguration(int IdUser)
        {
            Connection.D_PrinterConfig PC = new Connection.D_PrinterConfig();
            DataRow DR = PC.getPrintConfiguration(IdUser);
            if (DR!=null){
                Struct_PrintConfiguration PrintConfig = new Struct_PrintConfiguration(DR);
            return PrintConfig;
            } else 
            {
                return null;
            }
        }

        public Struct_PrintConfiguration Guardar() 
        {
            Connection.D_PrinterConfig PC = new Connection.D_PrinterConfig();

            if (Id == 0)
            {
                PC.insertPrintConfiguration(IdUser, Puerto, Baudios, Modelo);
                return Struct_PrintConfiguration.GetPrintConfiguration(IdUser);
                
            }
            else 
            {
                PC.updatePrintConfiguration(IdUser, Puerto, Baudios, Modelo);
                return this;
            }
        }

        public Struct_PrintConfiguration(DataRow p_DR) 
        {
            if (p_DR != null)
            {
                Id = int.Parse(p_DR["Id"].ToString());
                IdUser = int.Parse(p_DR["IdUser"].ToString());
                Puerto = p_DR["Puerto"].ToString();
                Baudios = int.Parse(p_DR["Baudios"].ToString());
                Modelo = p_DR["Modelo"].ToString();
            }

        }

    }
}
