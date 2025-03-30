using System;
using System.Reflection;

namespace Konso.Clients.Logging
{
    public static class VersionInfoHelper
    {
        public static string AppVersion()
        {
            Assembly? entryAssembly = Assembly.GetEntryAssembly();

            if (entryAssembly == null)
            {
                return "0.0.0";
            }

            string location = entryAssembly.Location;

            // AssemblyVersion
            Version? assemblyVersion = entryAssembly.GetName().Version;

            if(assemblyVersion != null)
                return assemblyVersion.ToString();

            // FileVersion
            //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(location);
            //string fileVersion = fvi.FileVersion;

            // InformationalVersion (e.g., Git hash or semantic version like 1.0.0-alpha)
            //string? informationalVersion = entryAssembly
            //    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            //    .InformationalVersion;

            return "0.0.0";
        }
    }
}
