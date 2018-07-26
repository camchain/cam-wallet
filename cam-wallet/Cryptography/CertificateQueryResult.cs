using System;
using System.Security.Cryptography.X509Certificates;

namespace Cam.Cryptography
{
    internal class CertificateQueryResult : IDisposable
    {
        public CertificateQueryResultType Type;
        public X509Certificate2 Certificate;
        public string subject;

        public void Dispose()
        {
            if (Certificate != null)
            {
                Certificate.Dispose();
            }
        }
    }
}
