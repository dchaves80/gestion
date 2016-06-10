using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Christoc.Modules.Suppliers.Static
{
    public static class CmbController
    {
        public static void SelectValue(DropDownList p_MyDDL,string p_value)
        {
            for (int a=0;a<p_MyDDL.Items.Count;a++)
            {
                if (p_MyDDL.Items[a].Value == p_value)
                {
                    p_MyDDL.SelectedIndex=a;
                    break;
                }
            }
        }
    }
}