using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace LicenceChecker
{
    public class Checker
    {
        public string KeyDirectory { get; set; }

        public Checker(string keyDirectory)
        {
            KeyDirectory = keyDirectory;
        }

        public bool Check(Licence licencia, DateTime? fechaActual)
        {
            var valida = true;
            if (fechaActual == null)
            {
                fechaActual = DateTime.Now;
            }
            valida = CheckIntegrity(licencia);
            if (valida)
                valida = CheckExpirationDate(licencia, fechaActual.Value);
            return valida;
        }

        public bool CheckIntegrity(Licence licencia)
        {
            byte[] publica = File.ReadAllBytes(KeyDirectory + "public.pub");
            TextReader tx = new StreamReader(new MemoryStream(publica));
            RsaKeyParameters param = (RsaKeyParameters)new PemReader(tx).ReadObject();
            ISigner signer = SignerUtilities.GetSigner("SHA256withRSA");
            signer.Init(false, param);
            var hash = Encoding.UTF8.GetBytes(licencia.GetKey);
            signer.BlockUpdate(hash, 0, hash.Length);
            bool result = signer.VerifySignature(licencia.LicenceHash);
            return result;
        }

        public bool CheckExpirationDate(Licence licencia, DateTime fecha)
        {
            return licencia.ExpirationDate >= fecha;
        }
    }
}