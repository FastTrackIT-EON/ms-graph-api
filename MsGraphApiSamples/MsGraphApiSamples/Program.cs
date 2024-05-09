using MsGraphApi.Api;
using MsGraphApi.Api.Http;
using MsGraphApi.Infrastructure;
using MsGraphApi.Models;
using MsGraphApiSamples.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsGraphApiSamples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string access_token = "EwCAA8l6BAAUbDba3x2OMJElkF7gJ4z/VbCPEz0AAT4II4sh7kQZxDeXyiFeb74ZWE62nO2gnlra2jjOq/yGI1r3IL5VNqjvUe9rH7mETaVTDObl1LD1aVKEgKp9T0wbsXgpO1GlwLTOr41fS2VDrC7TaeFRFi54jsB0TdVBbrpoKVsf2+7p2vbPBlk6w9nl91hbUargbwtIH8+DsCqtPY+Hv3kDywBqilXXLjV4SnQIiBZ5pNAWcmARHJX0eelpEGJEGLga8MVjrogkhJPDluEsiLW5sI9t7Si8/RhAFRXMoX0+RioO6VUgZr9+qVqM9avC4PIWG0A/Z4C01rTIpPwx70I6yF14JDnHbP2BVVTMFe6KyJ6QzDThB2Se0RkDZgAACIlMWZlmuk5MUAIL3Wkz3PoNS7Z2cjNQUsA+gjxdbVM6mS2UyXAWYQuZWzehY99BvptUNatCdtGOfhtyKIvnbGZtyx4odb/6JqUqYoWqsZClYu3nBcZDv26QZOQ2eAZDj4L8HklmF8wWmeNZwFLCops2i2mhVRMYCrFid6X+pNF/kPL5su6tFByevh8y6thukrMCLotfBXaP7zGi0DNEnpHrg/kzE3ue64FqPu3Mrk3JlxrfIGytNYiJC3kFWh+kGL4FQZPuDrSVLhZmkZ4p5j34gwdE07TIDqYcWCZtfvrd3XTyvbMk7kZ8xSZfoEYeWeIeMj+w4P1Wj4A4xMr1pZTKUIVeRp8QuNoHCwLq8UxvOPyaY9yn1L44KuEsFn1HiEuM2flfHug3P7CZPJ8f+vzgUlqwPTKN8n2MfxUX94pOmo/IRNtVA0oWh5W7WSL0t135tCF1bTRC7BW4kVg3DsYApyobfwQtrXB+na7J6W2lmVORB5xP/aTd/JlbAiW1K7yVo3ZUqJt6Gz80Mqp78GwI241SK0ak/StGX6uXSLOVtzpvbj42DmsjuGPj3anEuQaXffAAp8eFGTJMA16UxmWTT4IqbYJS7G7OSNhm2Azjj7vep9bqUbqYQD/TnPHeedS3teNUQd7Cj/ijvqilG1C7eK3G/PVYZJ20kCFmDiTcavbsYecciUGjSF+8nGa1p79O1fwJwvmc/BSO4eo5a8XNcoXQoctTAA0M0QvxljpoJQZb9YNnw+/LxvgfEKR2HP2K4PMO6mp0lWe04SDU43ckiZ254zFv4BX+mgI=";

            ILogger logger = new ConsoleLogger();

            try
            {
                GraphApi graphApi = new GraphApi(
                HttpClientFactory.CreateRealHttpClient(),
                logger);

                graphApi.EstablishAuthenticationSession(access_token);

                GetEmailMessagesResponse myEmails = await graphApi.GetMyEmails();

                if (myEmails.Values != null)
                {
                    foreach (EmailMessage emailMessage in myEmails.Values)
                    {
                        PrintEmailMessage(emailMessage);
                    }
                }
                else
                {
                    Console.WriteLine("Null EmailMessages received");
                }
            }
            catch (Exception e)
            {
                logger.LogException(e);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        private static void PrintEmailMessage(EmailMessage message)
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
