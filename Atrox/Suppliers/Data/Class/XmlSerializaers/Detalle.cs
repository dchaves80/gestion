using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data2.Class.XmlSerializaers
{
    [Serializable]

   



    public class Detalle
    {

        public int Id;
        public int IdFactura;
        public decimal IVA;
        public decimal PrecioFinal;
        public int CantidadINT;
        public decimal CantidadDEC;
        public string Descripcion;

        public Detalle() { }
        public Detalle(DataRow DR) 
        {
            Id = int.Parse( DR["Id"].ToString());
            IdFactura = int.Parse( DR["IdFactura"].ToString());
            IVA =Statics.Conversion.GetDecimal( DR["IVA"].ToString());
            PrecioFinal = Statics.Conversion.GetDecimal(DR["PrecioFinal"].ToString());
            CantidadINT = int.Parse(DR["CantidadINT"].ToString());
            CantidadDEC = Statics.Conversion.GetDecimal(DR["CantidadDEC"].ToString());
            Descripcion = DR["Descripcion"].ToString();
        }



    }
}
