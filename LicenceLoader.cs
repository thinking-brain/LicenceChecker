using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LicenceChecker
{
    public static class LicenceLoader
    {
        public static Licence LoadFromFile(string file)
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Licence licencia;
                using (Stream inpud = File.Open(file, FileMode.Open))
                {
                    licencia = (Licence)binaryFormatter.Deserialize(inpud);
                }
                return licencia;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}