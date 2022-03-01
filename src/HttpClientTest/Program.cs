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

            var ssid = < YourSSID >;
            var pwd = < YourWifiPwd >;

            while (true)
            {
                var result = wifiAdapters[0].Connect(ssid, WiFiReconnectionKind.Automatic, pwd);
                if (result.ConnectionStatus == WiFiConnectionStatus.Success) break;
            }

            var httpClient = new System.Net.Http.HttpClient
            {
                HttpsAuthentCert = GetGoogleCertificate(),
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

        private static X509Certificate GetGlobalSignCertificate()
        {
            return new X509Certificate(@"-----BEGIN CERTIFICATE-----
MIIDdTCCAl2gAwIBAgILBAAAAAABFUtaw5QwDQYJKoZIhvcNAQEFBQAwVzELMAkG
A1UEBhMCQkUxGTAXBgNVBAoTEEdsb2JhbFNpZ24gbnYtc2ExEDAOBgNVBAsTB1Jv
b3QgQ0ExGzAZBgNVBAMTEkdsb2JhbFNpZ24gUm9vdCBDQTAeFw05ODA5MDExMjAw
MDBaFw0yODAxMjgxMjAwMDBaMFcxCzAJBgNVBAYTAkJFMRkwFwYDVQQKExBHbG9i
YWxTaWduIG52LXNhMRAwDgYDVQQLEwdSb290IENBMRswGQYDVQQDExJHbG9iYWxT
aWduIFJvb3QgQ0EwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDaDuaZ
jc6j40+Kfvvxi4Mla+pIH/EqsLmVEQS98GPR4mdmzxzdzxtIK+6NiY6arymAZavp
xy0Sy6scTHAHoT0KMM0VjU/43dSMUBUc71DuxC73/OlS8pF94G3VNTCOXkNz8kHp
1Wrjsok6Vjk4bwY8iGlbKk3Fp1S4bInMm/k8yuX9ifUSPJJ4ltbcdG6TRGHRjcdG
snUOhugZitVtbNV4FpWi6cgKOOvyJBNPc1STE4U6G7weNLWLBYy5d4ux2x8gkasJ
U26Qzns3dLlwR5EiUWMWea6xrkEmCMgZK9FGqkjWZCrXgzT/LCrBbBlDSgeF59N8
9iFo7+ryUp9/k5DPAgMBAAGjQjBAMA4GA1UdDwEB/wQEAwIBBjAPBgNVHRMBAf8E
BTADAQH/MB0GA1UdDgQWBBRge2YaRQ2XyolQL30EzTSo//z9SzANBgkqhkiG9w0B
AQUFAAOCAQEA1nPnfE920I2/7LqivjTFKDK1fPxsnCwrvQmeU79rXqoRSLblCKOz
yj1hTdNGCbM+w6DjY1Ub8rrvrTnhQ7k4o+YviiY776BQVvnGCv04zcQLcFGUl5gE
38NflNUVyRRBnMRddWQVDf9VMOyGj/8N7yy5Y0b2qvzfvGn9LhJIZJrglfCm7ymP
AbEVtQwdpf5pLGkkeB6zpxxxYu7KyJesF12KwvhHhm4qxFYxldBniYUr+WymXUad
DKqC5JlR3XC321Y9YeRq4VzW9v493kHMB65jUr9TU/Qr6cf9tveCX4XSQRjbgbME
HMUfpIBvFSDJ3gyICh3WZlXi/EjJKSZp4A==
-----END CERTIFICATE-----");
        }

        private static X509Certificate GetGoogleCertificate()
        {
            return new X509Certificate(@"-----BEGIN CERTIFICATE-----
MIIN2DCCDMCgAwIBAgIRANbE0xOHmpgXCgAAAAE3h3YwDQYJKoZIhvcNAQELBQAw
RjELMAkGA1UEBhMCVVMxIjAgBgNVBAoTGUdvb2dsZSBUcnVzdCBTZXJ2aWNlcyBM
TEMxEzARBgNVBAMTCkdUUyBDQSAxQzMwHhcNMjIwMjE3MTAyMjAwWhcNMjIwNTEy
MTAyMTU5WjAXMRUwEwYDVQQDDAwqLmdvb2dsZS5jb20wWTATBgcqhkjOPQIBBggq
hkjOPQMBBwNCAAQ5Dm/AqrKZbcPS9Phal8dl4LjaXdq9fhD8gvG49brjI++A8sdz
+VysLEBbTIf1EbW2+LCX3OFXFTP41ax+DBomo4ILuTCCC7UwDgYDVR0PAQH/BAQD
AgeAMBMGA1UdJQQMMAoGCCsGAQUFBwMBMAwGA1UdEwEB/wQCMAAwHQYDVR0OBBYE
FG/JTG67OE+UjcxhKQmr0BNFJtTPMB8GA1UdIwQYMBaAFIp0f6+Fze6VzT2c0OJG
FPNxNR0nMGoGCCsGAQUFBwEBBF4wXDAnBggrBgEFBQcwAYYbaHR0cDovL29jc3Au
cGtpLmdvb2cvZ3RzMWMzMDEGCCsGAQUFBzAChiVodHRwOi8vcGtpLmdvb2cvcmVw
by9jZXJ0cy9ndHMxYzMuZGVyMIIJaAYDVR0RBIIJXzCCCVuCDCouZ29vZ2xlLmNv
bYIWKi5hcHBlbmdpbmUuZ29vZ2xlLmNvbYIJKi5iZG4uZGV2ghIqLmNsb3VkLmdv
b2dsZS5jb22CGCouY3Jvd2Rzb3VyY2UuZ29vZ2xlLmNvbYIYKi5kYXRhY29tcHV0
ZS5nb29nbGUuY29tggsqLmdvb2dsZS5jYYILKi5nb29nbGUuY2yCDiouZ29vZ2xl
LmNvLmlugg4qLmdvb2dsZS5jby5qcIIOKi5nb29nbGUuY28udWuCDyouZ29vZ2xl
LmNvbS5hcoIPKi5nb29nbGUuY29tLmF1gg8qLmdvb2dsZS5jb20uYnKCDyouZ29v
Z2xlLmNvbS5jb4IPKi5nb29nbGUuY29tLm14gg8qLmdvb2dsZS5jb20udHKCDyou
Z29vZ2xlLmNvbS52boILKi5nb29nbGUuZGWCCyouZ29vZ2xlLmVzggsqLmdvb2ds
ZS5mcoILKi5nb29nbGUuaHWCCyouZ29vZ2xlLml0ggsqLmdvb2dsZS5ubIILKi5n
b29nbGUucGyCCyouZ29vZ2xlLnB0ghIqLmdvb2dsZWFkYXBpcy5jb22CDyouZ29v
Z2xlYXBpcy5jboIRKi5nb29nbGV2aWRlby5jb22CDCouZ3N0YXRpYy5jboIQKi5n
c3RhdGljLWNuLmNvbYIPZ29vZ2xlY25hcHBzLmNughEqLmdvb2dsZWNuYXBwcy5j
boIRZ29vZ2xlYXBwcy1jbi5jb22CEyouZ29vZ2xlYXBwcy1jbi5jb22CDGdrZWNu
YXBwcy5jboIOKi5na2VjbmFwcHMuY26CEmdvb2dsZWRvd25sb2Fkcy5jboIUKi5n
b29nbGVkb3dubG9hZHMuY26CEHJlY2FwdGNoYS5uZXQuY26CEioucmVjYXB0Y2hh
Lm5ldC5jboIQcmVjYXB0Y2hhLWNuLm5ldIISKi5yZWNhcHRjaGEtY24ubmV0ggt3
aWRldmluZS5jboINKi53aWRldmluZS5jboIRYW1wcHJvamVjdC5vcmcuY26CEyou
YW1wcHJvamVjdC5vcmcuY26CEWFtcHByb2plY3QubmV0LmNughMqLmFtcHByb2pl
Y3QubmV0LmNughdnb29nbGUtYW5hbHl0aWNzLWNuLmNvbYIZKi5nb29nbGUtYW5h
bHl0aWNzLWNuLmNvbYIXZ29vZ2xlYWRzZXJ2aWNlcy1jbi5jb22CGSouZ29vZ2xl
YWRzZXJ2aWNlcy1jbi5jb22CEWdvb2dsZXZhZHMtY24uY29tghMqLmdvb2dsZXZh
ZHMtY24uY29tghFnb29nbGVhcGlzLWNuLmNvbYITKi5nb29nbGVhcGlzLWNuLmNv
bYIVZ29vZ2xlb3B0aW1pemUtY24uY29tghcqLmdvb2dsZW9wdGltaXplLWNuLmNv
bYISZG91YmxlY2xpY2stY24ubmV0ghQqLmRvdWJsZWNsaWNrLWNuLm5ldIIYKi5m
bHMuZG91YmxlY2xpY2stY24ubmV0ghYqLmcuZG91YmxlY2xpY2stY24ubmV0gg5k
b3VibGVjbGljay5jboIQKi5kb3VibGVjbGljay5jboIUKi5mbHMuZG91YmxlY2xp
Y2suY26CEiouZy5kb3VibGVjbGljay5jboIRZGFydHNlYXJjaC1jbi5uZXSCEyou
ZGFydHNlYXJjaC1jbi5uZXSCHWdvb2dsZXRyYXZlbGFkc2VydmljZXMtY24uY29t
gh8qLmdvb2dsZXRyYXZlbGFkc2VydmljZXMtY24uY29tghhnb29nbGV0YWdzZXJ2
aWNlcy1jbi5jb22CGiouZ29vZ2xldGFnc2VydmljZXMtY24uY29tghdnb29nbGV0
YWdtYW5hZ2VyLWNuLmNvbYIZKi5nb29nbGV0YWdtYW5hZ2VyLWNuLmNvbYIYZ29v
Z2xlc3luZGljYXRpb24tY24uY29tghoqLmdvb2dsZXN5bmRpY2F0aW9uLWNuLmNv
bYIkKi5zYWZlZnJhbWUuZ29vZ2xlc3luZGljYXRpb24tY24uY29tghZhcHAtbWVh
c3VyZW1lbnQtY24uY29tghgqLmFwcC1tZWFzdXJlbWVudC1jbi5jb22CC2d2dDEt
Y24uY29tgg0qLmd2dDEtY24uY29tggtndnQyLWNuLmNvbYINKi5ndnQyLWNuLmNv
bYILMm1kbi1jbi5uZXSCDSouMm1kbi1jbi5uZXSCFGdvb2dsZWZsaWdodHMtY24u
bmV0ghYqLmdvb2dsZWZsaWdodHMtY24ubmV0ggxhZG1vYi1jbi5jb22CDiouYWRt
b2ItY24uY29tgg0qLmdzdGF0aWMuY29tghQqLm1ldHJpYy5nc3RhdGljLmNvbYIK
Ki5ndnQxLmNvbYIRKi5nY3BjZG4uZ3Z0MS5jb22CCiouZ3Z0Mi5jb22CDiouZ2Nw
Lmd2dDIuY29tghAqLnVybC5nb29nbGUuY29tghYqLnlvdXR1YmUtbm9jb29raWUu
Y29tggsqLnl0aW1nLmNvbYILYW5kcm9pZC5jb22CDSouYW5kcm9pZC5jb22CEyou
Zmxhc2guYW5kcm9pZC5jb22CBGcuY26CBiouZy5jboIEZy5jb4IGKi5nLmNvggZn
b28uZ2yCCnd3dy5nb28uZ2yCFGdvb2dsZS1hbmFseXRpY3MuY29tghYqLmdvb2ds
ZS1hbmFseXRpY3MuY29tggpnb29nbGUuY29tghJnb29nbGVjb21tZXJjZS5jb22C
FCouZ29vZ2xlY29tbWVyY2UuY29tgghnZ3BodC5jboIKKi5nZ3BodC5jboIKdXJj
aGluLmNvbYIMKi51cmNoaW4uY29tggh5b3V0dS5iZYILeW91dHViZS5jb22CDSou
eW91dHViZS5jb22CFHlvdXR1YmVlZHVjYXRpb24uY29tghYqLnlvdXR1YmVlZHVj
YXRpb24uY29tgg95b3V0dWJla2lkcy5jb22CESoueW91dHViZWtpZHMuY29tggV5
dC5iZYIHKi55dC5iZYIaYW5kcm9pZC5jbGllbnRzLmdvb2dsZS5jb22CG2RldmVs
b3Blci5hbmRyb2lkLmdvb2dsZS5jboIcZGV2ZWxvcGVycy5hbmRyb2lkLmdvb2ds
ZS5jboIYc291cmNlLmFuZHJvaWQuZ29vZ2xlLmNuMCEGA1UdIAQaMBgwCAYGZ4EM
AQIBMAwGCisGAQQB1nkCBQMwPAYDVR0fBDUwMzAxoC+gLYYraHR0cDovL2NybHMu
cGtpLmdvb2cvZ3RzMWMzL2ZWSnhiVi1LdG1rLmNybDCCAQUGCisGAQQB1nkCBAIE
gfYEgfMA8QB3ACl5vvCeOTkh8FZzn2Old+W+V32cYAr4+U1dJlwlXceEAAABfwdq
9rIAAAQDAEgwRgIhAIE0L7A9sMArrCjWhEqwVijFZbhUw06y6Diatb9rVKRaAiEA
sbuGyS4hbkvjqU7+4OsjOByFAZuLbWIKg+yaxXvfAbsAdgDfpV6raIJPH2yt7rhf
Tj5a6s2iEqRqXo47EsAgRFwqcwAAAX8Hava9AAAEAwBHMEUCIGN/uAfSygpZ8EWC
FrMFiADW7MbIcBapR9onXFGXeDf1AiEAzSnEMEP+kqAT9DCbCtVdHw38XOrSLAmi
+z6uqIP5oH8wDQYJKoZIhvcNAQELBQADggEBAGF6HW6K1/I7iYW3/2gE7zNJcbwe
zdtpqZoC6N77Ot+Jn2BS79kyGtgFjkAyTX4jPmpMZvUZX6RlXY93Xmji/0lS6cbF
NzZ1GDwQzzH25yELNzrUKwW3fUPt4xyS6BUinI3KC9F2ELPwccIjTdgMgNrYMHV3
Tn6f4P5lR4aFuwFYcz2d+P9/2cYNVD42Yy/3L6XxA1vD4edvdFDZoOay3Q6p0XOx
kKwiSywCkh7o9PtJVE5xCyeX5EvQJinodDdllgCJFwQ0qTEoBmrdiEhiQkheq5rl
oIupGoAjtQcI79DiGzygdH07nLandHQoNr9UL44XLzd5NeTncV+aam7k5Hk=
-----END CERTIFICATE-----");
        }
    }
}
