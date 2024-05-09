using System.IO;
using System.Reflection;
using System;
using System.Linq;

namespace MsGraphApi.Tests
{
    internal static class JsonSamples
    {
        public static string ReadResourceJson(string resourceName)
        {
            // Get the assembly where the resource is located
            Assembly assembly = typeof(JsonSamples).Assembly;

            // find matching resource name
            string resolvedResourceName = assembly
                .GetManifestResourceNames()
                .Where(rs => rs.IndexOf(resourceName, StringComparison.OrdinalIgnoreCase) >= 0)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(resolvedResourceName))
            {
                return string.Empty;
            }

            // Read the embedded resource stream
            using (Stream stream = assembly.GetManifestResourceStream(resolvedResourceName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        return result;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
