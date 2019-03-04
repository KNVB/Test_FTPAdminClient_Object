using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLibrary
{
    class MessageCoder
    {
        ICryptoTransform encryptor, decryptor;
        CryptoStream crypto_Stream = null;
        MemoryStream memStream = null;
        RijndaelManaged Crypto = null;
        UTF8Encoding Byte_Transform = new UTF8Encoding();
        byte[] messageKeyArray, ivTextArray;
        public string messageKey { get; set; }
        public string ivText { get; set; }
        public void init()
        {
            
            messageKeyArray = System.Convert.FromBase64String(messageKey);
            ivTextArray = System.Convert.FromBase64String(ivText);
        }
        public string encode(string plainText)
        {
            memStream = new MemoryStream();
            byte[] PlainBytes = Byte_Transform.GetBytes(plainText);
         
            try
            {
                Crypto = new RijndaelManaged();
                Crypto.Key = messageKeyArray;
                Crypto.IV = ivTextArray;
                //Calling the method create encryptor method Needs both the Key and IV these have to be from the original Rijndael call
                //If these are changed nothing will work right.
                encryptor = Crypto.CreateEncryptor(Crypto.Key, Crypto.IV);

                //The big parameter here is the cryptomode.write, you are writing the data to memory to perform the transformation
                crypto_Stream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);

                //The method write takes three params the data to be written (in bytes) the offset value (int) and the length of the stream (int)
                crypto_Stream.Write(PlainBytes, 0, PlainBytes.Length);
            }
            finally
            {
                //if the crypto blocks are not clear lets make sure the data is gone
                if (Crypto != null)
                    Crypto.Clear();
                //Close because of my need to close things when done.
                crypto_Stream.Close();
            }
            return System.Convert.ToBase64String(memStream.ToArray());
        }
        public string decode(string cipherText)
        {
            var encryptBytes = System.Convert.FromBase64String(cipherText);
            Crypto = new RijndaelManaged();
            Crypto.Key = messageKeyArray;
            Crypto.IV = ivTextArray;
            Crypto.Mode = System.Security.Cryptography.CipherMode.CBC;
            Crypto.Padding = System.Security.Cryptography.PaddingMode.None;
            decryptor = Crypto.CreateDecryptor();
            return System.Text.Encoding.UTF8.GetString(decryptor.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));
        }
    }
}
