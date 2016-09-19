using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Data2.Class
{


    [Serializable]
    public class Struct_Cliente
    {
            public string RS;
            public string DNI;
            public string PAIS;
            public string PROVINCIA;
            public string LOCALIDAD;
            public string DOMICILIO;
            public string OBSERVACIONES;
            public string TIPOIVA;
            public decimal DESCUENTO;
            public string EMAIL;
            public int IDUSER;
            public int ID=0;


               

            public static List<Struct_Cliente> SearchClient(string searchstring, int IdUser) 
            {
                Connection.D_Clientes C = new Connection.D_Clientes();
                DataTable DT = C.Search_Cliente(searchstring, IdUser);
                if (DT != null && DT.Rows.Count > 0)
                {
                    List<Struct_Cliente> SCL = new List<Struct_Cliente>();
                    for (int a = 0; a < DT.Rows.Count; a++)
                    {
                        SCL.Add(new Struct_Cliente(DT.Rows[a]));
                    }
                    return SCL;
                }
                else 
                {
                    return null;
                }


            }

            public Struct_Cliente(DataRow DR) 
            {
                ID = int.Parse(DR["Id"].ToString());
                RS = DR["RazonSocial"].ToString();
                DNI = DR["DNI_CUIT_CUIL"].ToString();
                PAIS = DR["Pais"].ToString();
                PROVINCIA = DR["Provincia"].ToString();
                LOCALIDAD = DR["Localidad"].ToString();
                DOMICILIO = DR["Domicilio"].ToString();
                OBSERVACIONES = DR["Observaciones"].ToString();
                TIPOIVA = DR["TipoIva"].ToString();
                DESCUENTO = Statics.Conversion.GetDecimal(DR["Descuento"].ToString());
                EMAIL = DR["Email"].ToString();
                IDUSER = int.Parse(DR["IdUser"].ToString());
            }

            public Struct_Cliente
                (
                string p_RS,
                string p_DNI,
                string p_PAIS,
                string p_PROVINCIA,
                string p_LOCALIDAD,
                string p_DOMICILIO,
                string p_OBSERVACIONES,
                string p_TIPOIVA,
                decimal p_DESCUENTO,
                string p_EMAIL,
                int p_IDUSER
                )
            {
                RS = p_RS;
                DNI = p_DNI;
                PAIS = p_PAIS;
                PROVINCIA = p_PROVINCIA;
                LOCALIDAD = p_LOCALIDAD;
                DOMICILIO = p_DOMICILIO;
                OBSERVACIONES = p_OBSERVACIONES;
                TIPOIVA = p_TIPOIVA;
                DESCUENTO = p_DESCUENTO;
                EMAIL = p_EMAIL;
                IDUSER = p_IDUSER;
            }

            public bool Guardar() 
            {
                Data2.Connection.D_Clientes C = new Connection.D_Clientes();
                if (ID == 0)
                {
                   
                    int reg = C.insertarCliente(RS, DNI, PAIS, PROVINCIA, LOCALIDAD, DOMICILIO, OBSERVACIONES, TIPOIVA, DESCUENTO, EMAIL, IDUSER);
                    ID = reg;
                    
                    
                }
                else 
                {
                    C.actualizarCliente(RS, DNI, PAIS, PROVINCIA, LOCALIDAD, DOMICILIO, OBSERVACIONES, TIPOIVA, DESCUENTO, EMAIL, IDUSER,ID);
                    
                }

                if (ID != 0)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }

            


    }
}
