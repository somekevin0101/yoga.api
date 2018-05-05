using System;
using System.Reflection;
using System.Web.Mvc;

namespace YogaApi.Helpers
{
    public static class ApplicationDetails
    {
        /// <summary>     
        /// Return the Current Version from the AssemblyInfo.cs file.     
        /// </summary>     
        public static string CurrentVersion(this HtmlHelper helper)
        {
            try
            {
                var version = "";
                Assembly assembly = Assembly.GetExecutingAssembly();
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (customAttributes.Length > 0)
                {
                    version = ((AssemblyFileVersionAttribute)customAttributes[0]).Version;
                }
                if (string.IsNullOrEmpty(version))
                {
                    version = string.Empty;
                }
                return version;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Returns the Current CompanyName from the AssemblyInfo.cs file.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string CompanyName(this HtmlHelper helper)
        {
            try
            {
                var companyName = "";
                Assembly assembly = Assembly.GetExecutingAssembly();
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (customAttributes.Length > 0)
                {
                    companyName = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
                }
                if (string.IsNullOrEmpty(companyName))
                {
                    companyName = string.Empty;
                }
                return companyName;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}