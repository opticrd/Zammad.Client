using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Zammad.Client.Core.Internal
{
    public static class DebugUtility
    {
        public static async void Print(HttpResponseMessage httpResponse, string id, HttpRequestMessage httpRequest = null)
        {
#if DEBUG
            if(httpRequest != null)
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("HTTP REQUEST:");
                Console.WriteLine(httpRequest.ToString());
            }

            var x = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine(id + " HTTP RESPOSE:");
            Console.WriteLine(x);
            Console.WriteLine("---------------------------------------------------------------------");
#endif
        }
    }
}
