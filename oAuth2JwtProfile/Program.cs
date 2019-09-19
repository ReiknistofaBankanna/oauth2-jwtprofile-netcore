using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace oAuth2JwtProfile
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 5)
            {
                Console.WriteLine("Usage: .. adfsUrl certificatePath certificatePassword clientId resource");
                return;
            }

            var url = args[0];
            var cert_path = args[1];
            var cert_password = args[2];
            var clientId = args[3];
            var resource = args[4];

            X509Certificate2 certificate = CertificateUtil.GetCertificate(cert_path, cert_password);
            var securityKey = new Microsoft.IdentityModel.Tokens.X509SecurityKey(certificate);
            var header = new JwtHeader(new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, "RS256"));

            DateTime foo = DateTime.UtcNow;
            var nbf = ((DateTimeOffset)foo).ToUnixTimeSeconds();

            var payload = new JwtPayload
            {
                { "aud", url}, //adfs á að fá tokenið
                { "iss", clientId}, //clientId sem er skráð í adfs
                { "sub", clientId }, //clientId sem er skráð í adfs
                { "nbf", nbf },
                { "exp", nbf + 600}, //gildir í 600 sek
                { "jti", Guid.NewGuid().ToString() } //Identifier sem þarf að vera
           };

            var token = new JwtSecurityToken(header, payload);

            var handler = new JwtSecurityTokenHandler();
            var cielToken = handler.WriteToken(token);

            var dict = new Dictionary<string, string>();
            dict.Add("client_id", clientId);
            dict.Add("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer");
            dict.Add("grant_type", "client_credentials");
            dict.Add("resource", resource);
            dict.Add("client_assertion", cielToken);

            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) };

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage res = client.SendAsync(req).Result;
                var result = res.Content.ReadAsStringAsync().Result;
                if (res.IsSuccessStatusCode)
                {
                    AuthResult authResult = JsonConvert.DeserializeObject<AuthResult>(result);
                    Console.WriteLine("Access token: {0}", authResult.AccessToken);
                    Console.WriteLine("Token type  : {0}", authResult.TokenType);
                    Console.WriteLine("Expires in  : {0}", authResult.ExpiresIn);
                }
                else
                {
                    Console.WriteLine("Error");
                    Console.WriteLine("StatusCode  : {0}", res.StatusCode);
                    Console.WriteLine("Response    : {0}", result);
                }
            }
        }
    }
}
