using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace KB.Helpers.ClassLibrary
{
    class PasswordSecurityHelper
    {
        private DES des = DES.Create();
        private UnicodeEncoding ByteConverterUE = new UnicodeEncoding();
        private byte[] ByteConverter(string value)
        {
            return ByteConverterUE.GetBytes(value);
        }
        private byte[] Byte8(string value)
        {
            char[] arrayChar = value.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }
        private string ByteToString(byte[] value)
        {
            return ByteConverterUE.GetString(value);
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

    }
}

