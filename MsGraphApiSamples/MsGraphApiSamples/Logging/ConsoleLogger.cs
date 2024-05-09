using MsGraphApi.Exceptions;
using MsGraphApi.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MsGraphApiSamples.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void LogException(Exception exception)
        {
            InternalLogException(exception);
        }

        private static void InternalLogException(Exception ex, int indent = 0)
        {
            string indentString = new string(' ', indent * 2);
            Console.WriteLine($"{indentString}{ex.GetType()} - {ex.Message}");
            if (ex is GraphApiException graphApiException)
            {
                Console.WriteLine($"{indentString}HTTP Status Code: {graphApiException.StatusCode}, reason: {graphApiException.ReasonPhrase}");
                InternalLogDictionary(graphApiException.ErrorDetails, indent);
            }

            Console.WriteLine($"{indentString}Stack trace: ");
            Console.WriteLine($"{indentString}{ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"{indentString}Inner Exception:");
                InternalLogException(ex.InnerException, indent + 1);
            }
        }

        private static void InternalLogDictionary(IDictionary<string, object> dictionary, int indent = 0)
        {
            if (dictionary.Any())
            {
                string indentString = new string(' ', indent * 2);
                foreach (KeyValuePair<string, object> pair in dictionary)
                {
                    if (pair.Value is IDictionary<string, object> innerDictionary)
                    {
                        Console.WriteLine($"{indentString}{pair.Key} = {{");
                        InternalLogDictionary(innerDictionary, indent + 1);
                        Console.WriteLine($"{indentString}}}");
                    }
                    else if (pair.Value is IEnumerable collection)
                    {
                        Console.WriteLine($"{indentString}{pair.Key} = [" + string.Join(",", collection) + "]");
                    }
                    else
                    {
                        Console.WriteLine($"{indentString}{pair.Key} = {pair.Value}");
                    }
                }
            }
        }

        
    }
}
