using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNuke.Entities.Modules;

namespace Data2.Statics
{
    public static class Log
    {
        public static void ADD(string message, PortalModuleBase MODULE)
        {
            DotNetNuke.Services.Log.EventLog.EventLogController ELC = new DotNetNuke.Services.Log.EventLog.EventLogController();

            if (MODULE != null)
            {

                ELC.AddLog(MODULE.UserInfo.DisplayName + " - " + MODULE.ModuleConfiguration.DesktopModule.FriendlyName, message, DotNetNuke.Services.Log.EventLog.EventLogController.EventLogType.ADMIN_ALERT);
            }
            else 
            {
                ELC.AddLog("Alerta Sistem:", message, DotNetNuke.Services.Log.EventLog.EventLogController.EventLogType.ADMIN_ALERT);
            }
        }
    }
}
