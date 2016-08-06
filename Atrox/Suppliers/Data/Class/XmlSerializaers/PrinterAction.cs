using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace Data2.Class.XmlSerializaers
{
    [Serializable]
    public class PrinterAction
    {
        public int Id;
        public string Action;

        public PrinterAction() { }

        public static PrinterAction GetLastPrinterAction(int IdUser)
        {
            Connection.D_PrinterConfig PC = new Connection.D_PrinterConfig();
            DataRow DR = PC.GetPrinterAction(IdUser);
            if (DR != null)
            {
                XmlSerializaers.PrinterAction PA = new PrinterAction();
                PA.Action = DR["PrintAction"].ToString();
                PA.Id = int.Parse(DR["Id"].ToString());
                return PA;

            }
            else 
            {
                return null;
            }
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

        public static PrinterAction Deserealiza(string StringXML)
        {


            XmlSerializer ser = new XmlSerializer(typeof(PrinterAction));


            StringReader SR = new StringReader(StringXML);
            PrinterAction F = null;
            try
            {
                F = (PrinterAction)ser.Deserialize(SR);
            }
            catch
            {
                F = Deserealiza(ProcessString(StringXML));
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

    }

}