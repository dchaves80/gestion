using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Class
{
    public class Struct_Supplier
    {
        public int Id;
        public int IdUser;
        public string Nombre;
        public string NombreFantasia;
        public int Pais;
        public int Provincia;
        public string Localidad;
        public string Domicilio;
        public string Telefono1;
        public string Telefono2;
        public string MailContacto;
        public string MailPedidos;
        public int IdCategoriaAfip;
        public string IngresosBrutos;
        public int IdTipoDocumento;
        public string NroDocumento;

        public Struct_Supplier
            (
        int p_IdUser,
        string p_Nombre,
        string p_NombreFantasia,
        int p_Pais,
        int p_Provincia,
        string p_Localidad,
        string p_Domicilio,
        string p_Telefono1,
        string p_Telefono2,
        string p_MailContacto,
        string p_MailPedidos,
        int p_IdCategoriaAfip,
        string p_IngresosBrutos,
        int p_IdTipoDocumento,
        string p_NroDocumento
            )
 
        {
            Id = 0;
            IdUser=p_IdUser;
            Nombre=p_Nombre;
            NombreFantasia=p_NombreFantasia;
            Pais=p_Pais;
            Provincia=p_Provincia;
            Localidad=p_Localidad;
            Domicilio=p_Domicilio;
            Telefono1=p_Telefono1;
            Telefono2=p_Telefono2;
            MailContacto=p_MailContacto;
            MailPedidos=p_MailPedidos;
            IdCategoriaAfip=p_IdCategoriaAfip;
            IngresosBrutos=p_IngresosBrutos;
            IdTipoDocumento=p_IdTipoDocumento;
            NroDocumento=p_NroDocumento;
        }

        public Struct_Supplier(int p_IdUser, int p_IdSupplier) 
        {
            DataRow t_DR = Connection.D_Supplier.Get_SingleSupplier(p_IdUser, p_IdSupplier);
            if (t_DR != null) 
            {
                Id = int.Parse(t_DR["Id"].ToString());
                IdUser = int.Parse(t_DR["IdUser"].ToString());
                Nombre = t_DR["Nombre"].ToString();
                NombreFantasia = t_DR["NombreFantasía"].ToString();
                Pais = int.Parse(t_DR["País"].ToString());
                Provincia = int.Parse(t_DR["Provincia"].ToString());
                Localidad = t_DR["Localidad"].ToString();
                Domicilio = t_DR["Domicilio"].ToString();
                Telefono1 = t_DR["Teléfono1"].ToString();
                Telefono2 = t_DR["Teléfono2"].ToString();
                MailContacto = t_DR["MailContacto"].ToString();
                MailPedidos = t_DR["MailPedidos"].ToString();
                IdCategoriaAfip = int.Parse(t_DR["IdCategoriaAfip"].ToString());
                IngresosBrutos = t_DR["NroIngresosBrutos"].ToString();
                IdTipoDocumento = int.Parse(t_DR["IdTipoDocumento"].ToString());
                NroDocumento = t_DR["NroDocumento"].ToString();

            }
        }

        public void Borrar(int p_IdUser) 
        {
            if (Id != 0)
            {
                GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
                QTA.delete_Supplier(Id, p_IdUser);
            }
        }

        public void Guardar() 
        {
            if (Id == 0)
            {
                GestionDataSetTableAdapters.insert_SupplierTableAdapter TA = new GestionDataSetTableAdapters.insert_SupplierTableAdapter();
                GestionDataSet.insert_SupplierDataTable DT = new GestionDataSet.insert_SupplierDataTable();
                TA.Fill(DT, IdUser, Nombre, NombreFantasia, Pais, Provincia, Localidad, Domicilio, Telefono1, Telefono2, MailContacto, MailPedidos, IdCategoriaAfip, IngresosBrutos, IdTipoDocumento, NroDocumento);
                if (DT.Rows.Count > 0)
                {
                    Id = Convert.ToInt32(DT.Rows[0]["Id"].ToString());
                }
            }
        }

        public void Actualizar(int p_IdUser) 
        {
            if (Id != 0)
            {
                GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
                QTA.update_Supplier(p_IdUser, Id, Nombre, NombreFantasia, Pais, Provincia, Localidad, Domicilio, Telefono1, Telefono2, MailContacto, MailPedidos, IdCategoriaAfip, IngresosBrutos, IdTipoDocumento, NroDocumento);
            }
           
        }
    }
}
