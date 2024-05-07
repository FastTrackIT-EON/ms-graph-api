using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsGraphApiSamples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string access_token = "<REPLACE-ME>";
            try
            {
                GraphApi.AccessToken = access_token;
                string json = await GraphApi.GetJsonAsync("/v1.0/me/messages");

                GetEmailMessagesResponse response = JsonConvert.DeserializeObject<GetEmailMessagesResponse>(json);
                if (response.EmailMessages != null)
                {
                    foreach (EmailMessage emailMessage in response.EmailMessages)
                    {
                        LogEmailMessage(emailMessage);
                    }
                }
                else
                {
                    Console.WriteLine("Null EmailMessages received");
                }
            }
            catch (Exception ex) 
            {
                LogException(ex);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        private static void LogException(Exception ex, int indent = 0)
        {
            string indentString = new string(' ', indent * 2);
            Console.WriteLine($"{indentString}{ex.GetType()} - {ex.Message}");
            if (ex is GraphApiException graphApiException)
            {
                Console.WriteLine($"{indentString}HTTP Status Code: {graphApiException.StatusCode}, reason: {graphApiException.ReasonPhrase}");
                LogDictionary(graphApiException.ErrorDetails, indent);
            }

            Console.WriteLine($"{indentString}Stack trace: ");
            Console.WriteLine($"{indentString}{ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"{indentString}Inner Exception:");
                LogException(ex.InnerException, indent + 1);  
            }
        }

        private static void LogDictionary(IDictionary<string, object> dictionary, int indent = 0) 
        {
            if (dictionary.Any())
            {
                string indentString = new string(' ', indent * 2);
                foreach (KeyValuePair<string, object> pair in dictionary)
                {
                    if (pair.Value is IDictionary<string, object> innerDictionary)
                    {
                        Console.WriteLine($"{indentString}{pair.Key} = {{");
                        LogDictionary(innerDictionary, indent + 1);
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

        private static void LogEmailMessage(EmailMessage message)
        {
            Console.WriteLine($"Conversation Id: {message.ConversationId}");
            Console.WriteLine($"From: {message.From.EmailAddress.Address}");
            string toRecipients = string.Join(",", message.ToRecipients.Select(x => x.EmailAddress.Address));
            Console.WriteLine($"To: {toRecipients}");
            Console.WriteLine($"Subject: {message.Subject}");
            Console.WriteLine($"Body Preview: {message.BodyPreview}");
            Console.WriteLine();
        }
    }
}
