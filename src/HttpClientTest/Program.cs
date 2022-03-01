using System;
using System.Device.WiFi;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace HttpClientTest
{
    public class Program
    {
        public static void Main()
        {
            var wifiAdapters = WiFiAdapter.FindAllAdapters();

            var ssid = <YourSSID>;
            var pwd = <YourWifiPwd>;

            while (true)
            {
                var result = wifiAdapters[0].Connect(ssid, WiFiReconnectionKind.Automatic, pwd);
                if (result.ConnectionStatus == WiFiConnectionStatus.Success) break;
            }

            var httpClient = new System.Net.Http.HttpClient
            {
                HttpsAuthentCert = new X509Certificate(Resources.GetBytes(
                    Resources.BinaryResources.DigiCertGlobalRootCA)),
                SslProtocols = System.Net.Security.SslProtocols.Tls12
            };

            var url1 = "http://neverssl.com/";

            var response1 = httpClient.Get(url1);
            var test1 = response1.Content.ReadAsString();

            var url2 = "https://www.google.com/";

            //fails with a SocketException - CLR_E_FAIL
            var response2 = httpClient.Get(url2);
            var test2 = response2.Content.ReadAsString();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
