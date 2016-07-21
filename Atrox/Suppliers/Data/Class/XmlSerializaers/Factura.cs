using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using System.Text;
using System.Xml.Serialization;
using System.Xml;
namespace Data2.Class.XmlSerializaers
{

    
    [Serializable]
    [XmlRoot("Factura")]
    public class Factura
    {


        public int Id = 0;
        public List<Detalle> MiDetalle;
        public string FacturaTipo;
        private string respinsc;
        private string respnoinsc;
        private string exento;
        private string consumidorfinal;
        private string respmonotributo;
        /// <summary>
        /// 1 = RespInsc<para />
        /// 2 = RespNoInsc<para />
        /// 3 = exento<para />
        /// 4 = cosumidorfinal<para />
        /// 5 = respmonotributo<para />
        /// 6 = que te pasa gato?<para />
        /// </summary>
        public int PosicionIva = 0;

        public string contado;
        public string ctacte;
        public string Nombre = "";
        public string domicilio = "";
        public string telefono = "";
        public string localidad = "";
        public string cuit = "";
        public decimal SubTotal;
        public decimal Ivas;
        public decimal Total;
        public DateTime Fecha;

        public Factura() { }

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

        public static Factura Deserealiza(string StringXML)
        {
            

            XmlSerializer ser = new XmlSerializer(typeof(Factura));


            StringReader SR = new StringReader(StringXML);
            Factura F = null;
            try
            {
                F = (Factura)ser.Deserialize(SR);
            }
            catch
            {
                F = Deserealiza(ProcessString(StringXML));
            }

            return F;
        }

        public Factura(DataRow DR)
        {
            
            
            Id = Convert.ToInt32(DR["Id"].ToString());
            Fecha = Convert.ToDateTime(DR["Fecha"]);
            FacturaTipo = DR["Tipo"].ToString();
            Nombre = DR["Nombre"].ToString();
            domicilio = DR["Domicilio"].ToString();
            telefono = DR["Telefono"].ToString();
            localidad = DR["Localidad"].ToString();
            cuit = DR["Cuit"].ToString();



            respinsc = DR["RepInsc"].ToString();
            respnoinsc = DR["RespNoInsc"].ToString();
            exento = DR["Exento"].ToString();
            consumidorfinal = DR["ConsumidorFinal"].ToString();
            respmonotributo = DR["RespMonotributo"].ToString();

           
            if (respinsc.ToLower() == "true") PosicionIva = 1;
            if (respnoinsc.ToLower() == "true") PosicionIva = 2;
            if (exento.ToLower() == "true") PosicionIva = 3;
            if (consumidorfinal.ToLower() == "true") PosicionIva = 4;
            if (respmonotributo.ToLower() == "true") PosicionIva = 5;
            

            

            contado = DR["Contado"].ToString();
            ctacte = DR["CtaCte"].ToString();
            SubTotal = Statics.Conversion.GetDecimal(DR["SubTotal"].ToString());
            Ivas = Statics.Conversion.GetDecimal(DR["Ivas"].ToString());
            Total = Statics.Conversion.GetDecimal(DR["Total"].ToString());
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
