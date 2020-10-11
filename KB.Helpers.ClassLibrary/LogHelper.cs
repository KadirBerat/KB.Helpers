using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;

namespace KB.Helpers.ClassLibrary
{
    class LogHelper
    {
        public static string GetIpAddress()
        {
            try
            {
                string LogIp = "";
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    LogIp = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else if (System.Web.HttpContext.Current.Request.UserHostAddress.Length != 0)
                {
                    LogIp = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                else if (string.IsNullOrEmpty(LogIp))
                {
                    LogIp = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return LogIp;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string GetMacAddress()
        {
            try
            {
                String macAddress = string.Empty;
                string mac = null;
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    OperationalStatus ot = nic.OperationalStatus;
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddress = nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
                for (int i = 0; i <= macAddress.Length - 1; i++)
                {
                    mac = mac + ":" + macAddress.Substring(i, 2);
                    i++;
                }
                mac = mac.Remove(0, 1);
                return mac;
            }
            catch
            {
                return "";
            }
        }
        public static string GetUserName()
        {
            return Environment.UserName;
        }
        public static string GetOSVersion()
        {
            return Environment.OSVersion.ToString();
        }
        public static string GetMachineName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
