using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KB.Helpers.ClassLibrary
{
    class SecurityHelper
    {
        private Aes aes = Aes.Create();
        private DES des = DES.Create();
        private RC2 rc2 = RC2.Create();
        private Rijndael rijndael = Rijndael.Create();
        private TripleDES tripleDES = TripleDES.Create();
        private UnicodeEncoding ByteConverterUE = new UnicodeEncoding();
        public byte[] ByteConverter(string value)
        {
            return ByteConverterUE.GetBytes(value);
        }
        public byte[] Byte8(string value)
        {
            char[] arrayChar = value.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }
        public string ByteToString(byte[] value)
        {
            return ByteConverterUE.GetString(value);
        }
        public string CreateAESKey()
        {
            aes.GenerateKey();
            return ByteToString(aes.Key);
        }
        public string CreateAESIV()
        {
            aes.GenerateIV();
            return ByteToString(aes.IV);
        }
        public string CreateDESKey()
        {
            des.GenerateKey();
            return ByteToString(des.Key);
        }
        public string CreateDESIV()
        {
            des.GenerateIV();
            return ByteToString(des.IV);
        }
        public string CreateRC2Key()
        {
            rc2.GenerateKey();
            return ByteToString(rc2.Key);
        }
        public string CreateRC2IV()
        {
            rc2.GenerateIV();
            return ByteToString(rc2.IV);
        }
        public string CreateRijndaelKey()
        {
            rijndael.GenerateKey();
            return ByteToString(rijndael.Key);
        }
        public string CreateRijndaelIV()
        {
            rijndael.GenerateIV();
            return ByteToString(rijndael.IV);
        }
        public string CreateTripleDESKey()
        {
            tripleDES.GenerateKey();
            return ByteToString(tripleDES.Key);
        }
        public string CreateTripleDESIV()
        {
            tripleDES.GenerateIV();
            return ByteToString(tripleDES.IV);
        }
        public string GetAESKeyAndIV()
        {
            return "key:" + CreateAESKey() + "|iv:" + CreateAESIV();
        }
        public string GetDESKeyAndIV()
        {
            return "key:" + CreateDESKey() + "|iv:" + CreateDESIV();
        }
        public string GetRC2KeyAndIV()
        {
            return "key:" + CreateRC2Key() + "|iv:" + CreateRC2IV();
        }
        public string GetRijndaelKeyAndIV()
        {
            return "key:" + CreateRijndaelKey() + "|iv:" + CreateRijndaelIV();
        }
        public string GetTripleDESKeyAndIV()
        {
            return "key:" + CreateTripleDESKey() + "|iv:" + CreateTripleDESIV();
        }
        public string MD5(string strPass)
        {
            if (strPass == "" || strPass == null)
            {
                throw new ArgumentNullException("No pass!");
            }
            else
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] aryPass = ByteConverter(strPass);
                byte[] aryHash = md5.ComputeHash(aryPass);
                return BitConverter.ToString(aryHash);
            }
        }
        public string SHA1(string strPass)
        {
            if (strPass == "" || strPass == null)
            {
                throw new ArgumentNullException("No Pass!");
            }
            else
            {
                SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                byte[] aryPass = ByteConverter(strPass);
                byte[] aryHash = sha1.ComputeHash(aryPass);
                return BitConverter.ToString(aryHash);
            }
        }
        public string SHA256(string strPass)
        {
            if (strPass == "" || strPass == null)
            {
                throw new ArgumentNullException("No Pass!");
            }
            else
            {
                SHA256Managed sha256 = new SHA256Managed();
                byte[] aryPass = ByteConverter(strPass);
                byte[] aryHash = sha256.ComputeHash(aryPass);
                return BitConverter.ToString(aryHash);
            }
        }
        public string SHA384(string strPass)
        {
            if (strPass == "" || strPass == null)
            {
                throw new ArgumentNullException("No Pass!");
            }
            else
            {
                SHA384Managed sha384 = new SHA384Managed();
                byte[] aryPass = ByteConverter(strPass);
                byte[] aryHash = sha384.ComputeHash(aryPass);
                return BitConverter.ToString(aryHash);
            }
        }
        public string SHA512(string strPass)
        {
            if (strPass == "" || strPass == null)
            {
                throw new ArgumentNullException("Şifrelenecek veri yok.");
            }
            else
            {
                SHA512Managed sha512 = new SHA512Managed();
                byte[] aryPass = ByteConverter(strPass);
                byte[] aryHash = sha512.ComputeHash(aryPass);
                return BitConverter.ToString(aryHash);
            }
        }
        public string DESEncrypt(string strInput, string keyInput, string ivInput)
        {
            string output = "";
            if ((keyInput != null || keyInput != null) && (ivInput != null || ivInput != ""))
            {
                if (strInput == "" || strInput == null)
                {
                    throw new ArgumentNullException("No Input!");
                }
                else
                {
                    byte[] aryKey = Byte8(keyInput);
                    byte[] aryIV = Byte8(ivInput);
                    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
                    StreamWriter writer = new StreamWriter(cs);
                    writer.Write(strInput);
                    writer.Flush();
                    cs.FlushFinalBlock();
                    writer.Flush();
                    output = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                    writer.Dispose();
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            else
            {
                throw new ArgumentNullException("No Key!");
            }
            return output;
        }
        public string DESDecrypt(string strInput, string keyInput, string ivInput)
        {
            string strOutput = "";
            if (strInput == "" || strInput == null || keyInput == "" || keyInput == null || ivInput == "" || ivInput == null)
            {
                throw new ArgumentNullException("No Input!");
            }
            else
            {
                byte[] aryKey = Byte8(keyInput);
                byte[] aryIV = Byte8(ivInput);
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(strInput));
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cs);
                strOutput = reader.ReadToEnd();
                reader.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return strOutput;
        }
        public string RC2Encrypt(string strInput, string keyInput, string ivInput)
        {
            string output = "";
            if ((keyInput != null || keyInput != null) && (ivInput != null || ivInput != ""))
            {
                if (strInput == "" || strInput == null)
                {
                    throw new ArgumentNullException("No Input!");
                }
                else
                {
                    byte[] aryKey = Byte8(keyInput);
                    byte[] aryIV = Byte8(ivInput);
                    RC2CryptoServiceProvider dec = new RC2CryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, dec.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
                    StreamWriter writer = new StreamWriter(cs);
                    writer.Write(strInput);
                    writer.Flush();
                    cs.FlushFinalBlock();
                    writer.Flush();
                    output = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                    writer.Dispose();
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            return output;
        }
        public string RC2Decrypt(string strInput, string keyInput, string ivInput)
        {
            string strOutput = "";
            if (strInput == "" || strInput == null || keyInput == "" || keyInput == null || ivInput == "" || ivInput == null)
            {
                throw new ArgumentNullException("No Input!");
            }
            else
            {
                byte[] aryKey = Byte8(keyInput);
                byte[] aryIV = Byte8(ivInput);
                RC2CryptoServiceProvider cp = new RC2CryptoServiceProvider();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(strInput));
                CryptoStream cs = new CryptoStream(ms, cp.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cs);
                strOutput = reader.ReadToEnd();
                reader.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return strOutput;
        }
        public string RijndaelEncrypt(string strInput, string keyInput, string ivInput)
        {
            string output = "";
            if ((keyInput != null || keyInput != null) && (ivInput != null || ivInput != ""))
            {
                if (strInput == "" || strInput == null)
                {
                    throw new ArgumentNullException("No Input!");
                }
                else
                {
                    byte[] aryKey = Byte8(keyInput);
                    byte[] aryIV = Byte8(ivInput);
                    RijndaelManaged dec = new RijndaelManaged();
                    dec.Mode = CipherMode.CBC;
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, dec.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
                    StreamWriter writer = new StreamWriter(cs);
                    writer.Write(strInput);
                    writer.Flush();
                    cs.FlushFinalBlock();
                    writer.Flush();
                    output = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                    writer.Dispose();
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            return output;
        }
        public string RijndaelDecrypt(string strInput, string keyInput, string ivInput)
        {
            string strOutput = "";
            if (strInput == "" || strInput == null || keyInput == "" || keyInput == null || ivInput == "" || ivInput == null)
            {
                throw new ArgumentNullException("No Input!");
            }
            else
            {
                byte[] aryKey = Byte8(keyInput);
                byte[] aryIV = Byte8(ivInput);
                RijndaelManaged cp = new RijndaelManaged();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(strInput));
                CryptoStream cs = new CryptoStream(ms, cp.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cs);
                strOutput = reader.ReadToEnd();
                reader.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return strOutput;
        }
        public string TripleDESEncrypt(string strInput, string keyInput, string ivInput)
        {
            string output = "";
            if ((keyInput != null || keyInput != null) && (ivInput != null || ivInput != ""))
            {
                if (strInput == "" || strInput == null)
                {
                    throw new ArgumentNullException("No Input!");
                }
                else
                {
                    byte[] aryKey = Byte8(keyInput);
                    byte[] aryIV = Byte8(ivInput);
                    TripleDESCryptoServiceProvider dec = new TripleDESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, dec.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
                    StreamWriter writer = new StreamWriter(cs);
                    writer.Write(strInput);
                    writer.Flush();
                    cs.FlushFinalBlock();
                    writer.Flush();
                    output = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                    writer.Dispose();
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            return output;
        }
        public string TripleDESDecrypt(string strInput, string keyInput, string ivInput)
        {
            string strOutput = "";
            if (strInput == "" || strInput == null || keyInput == "" || keyInput == null || ivInput == "" || ivInput == null)
            {
                throw new ArgumentNullException("No Input!");
            }
            else
            {
                byte[] aryKey = Byte8(keyInput);
                byte[] aryIV = Byte8(ivInput);
                TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(strInput));
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cs);
                strOutput = reader.ReadToEnd();
                reader.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return strOutput;
        }
        public string RSAEncrypt(string strInput, out RSAParameters prm)
        {
            string strOutput = "";
            if (strInput == "")
            {
                throw new ArgumentNullException("No Input!");
            }
            else
            {
                byte[] arySequence = ByteConverter(strInput);
                RSACryptoServiceProvider dec = new RSACryptoServiceProvider();
                prm = dec.ExportParameters(true);
                byte[] aryBack = dec.Encrypt(arySequence, false);
                strOutput = Convert.ToBase64String(aryBack);
            }
            return strOutput;
        }
        public string RSADecrypt(string strInput, RSAParameters prm)
        {
            string strOutput = "";
            if (strInput == "" || strInput == null)
            {
                throw new ArgumentNullException("No Input!");
            }
            else
            {
                RSACryptoServiceProvider dec = new RSACryptoServiceProvider();
                byte[] arySequence = Convert.FromBase64String(strInput);
                UnicodeEncoding UE = new UnicodeEncoding();
                dec.ImportParameters(prm);
                byte[] aryBack = dec.Decrypt(arySequence, false);
                strOutput = UE.GetString(aryBack);
            }
            return strOutput;
        }
    }
}
