﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Statics
{
    public static class Configurations
    {
        public static string GetCS() 
        {
            GestionDataSetTableAdapters.GET_UserConfiguracionTableAdapter TA  = new GestionDataSetTableAdapters.GET_UserConfiguracionTableAdapter();

            return "DS:" + TA.Connection.DataSource;
            
        }
    }
}
