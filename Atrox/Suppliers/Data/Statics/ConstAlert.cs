using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Data2.Statics
{
    public static class ConstAlert
    {
        public const string Art_Record_Success = "El articulo fue grabado con éxito!";
        public const string Art_Record_Error = "Problema al grabar el artículo, consulte a su administrador.";
        public const string Art_File_SuccessUploaded = "El archivo de actualizacion fue subido al servidor";
        public const string Art_File_ErrorUploaded = "Problema al subir el archivo al servidor, consulte a su administrador";
        public const string Art_File_SuccessProcessing = "El archivo fue procesado de forma exitosa.";
        public const string Art_File_ErrorProcessing = "Problema al procesar el archivo";
        public enum TypeMessage {Error,Alert,Message};

        public static void AddMessage(string p_Message,TypeMessage p_type, Control p_WebControl )
        {

            HtmlGenericControl _mydiv = new HtmlGenericControl("div");


            if (p_type == TypeMessage.Message) 
            {
                _mydiv.Attributes.Add("Class", "MessageSuccess MessageBox");
               
            }

            if (p_type == TypeMessage.Alert) 
            {
                _mydiv.Attributes.Add("Class", "MessageAlert MessageBox");
            }

            if (p_type == TypeMessage.Error)
            {
                _mydiv.Attributes.Add("Class", "MessageError MessageBox");
            }

            

            _mydiv.InnerText = p_Message;
            p_WebControl.Controls.Add(_mydiv);

        }

    }
}
