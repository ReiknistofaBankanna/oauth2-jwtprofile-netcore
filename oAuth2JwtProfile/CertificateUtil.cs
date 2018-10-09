using System.Security.Cryptography.X509Certificates;

namespace oAuth2JwtProfile
{
    public class CertificateUtil
    {

        public static X509Certificate2 GetCertificate(string path, string password = "")
        {
            X509Certificate2 cert = null;
            if (password == "")
                cert = new X509Certificate2(path);
            else
            {
                cert = new X509Certificate2(path, password);
            }

            return cert;
        }
    }
}
