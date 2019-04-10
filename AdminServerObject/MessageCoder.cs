using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AdminServerObject
{
    public class MessageCoder
    {
        int RSAKeySize = 2048;
        AsymmetricCipherKeyPair rsaKeyPair;
        IBufferedCipher aesCipher=null;
        ICipherParameters cipherParameters;
        UTF8Encoding Byte_Transform = new UTF8Encoding();
        public MessageCoder()
        {
            RsaKeyPairGenerator g = new RsaKeyPairGenerator();
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            g.Init(new KeyGenerationParameters(new SecureRandom(), RSAKeySize));
            rsaKeyPair = g.GenerateKeyPair();
        }
        public string getRSAPublicKey()
        {
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(rsaKeyPair.Public);
            byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
            string serializedPublic = Convert.ToBase64String(serializedPublicBytes);
            return serializedPublic;
        }
        public string decodeRSAMessage(string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            Pkcs1Encoding eng = new Pkcs1Encoding(new RsaEngine());
            eng.Init(false, rsaKeyPair.Private);
            int length = cipherTextBytes.Length;
            int blockSize = eng.GetInputBlockSize();
            List<byte> plainTextBytes = new List<byte>();
            for (int chunkPosition = 0;
                chunkPosition < length;
                chunkPosition += blockSize)
            {
                int chunkSize = Math.Min(blockSize, length - chunkPosition);
                plainTextBytes.AddRange(eng.ProcessBlock(
                    cipherTextBytes, chunkPosition, chunkSize
                ));
            }
            return Encoding.UTF8.GetString(plainTextBytes.ToArray());
        }
        public void initAESCodec(string messageKey, string ivText)
        {
            byte [] messageKeyArray = System.Convert.FromBase64String(messageKey);
            byte []ivTextArray = System.Convert.FromBase64String(ivText);
            aesCipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            KeyParameter keyParameter = ParameterUtilities.CreateKeyParameter("AES", messageKeyArray);
            cipherParameters = new ParametersWithIV(keyParameter, ivTextArray,0,16);
            
        }
        public string aesEncode(string plainText)
        {
            byte[] plainBytes = Byte_Transform.GetBytes(plainText);
            byte[] outputBytes = new byte[aesCipher.GetOutputSize(plainBytes.Length)];
            aesCipher.Reset();
            aesCipher.Init(true, cipherParameters);
            int length = aesCipher.ProcessBytes(plainBytes, outputBytes, 0);
            aesCipher.DoFinal(outputBytes, length); //Do the final block
            return Convert.ToBase64String(outputBytes);
        }
        public string aesDecode(string cipherText)
        {
            byte[]encryptBytes = System.Convert.FromBase64String(cipherText);
            byte[] comparisonBytes = new byte[aesCipher.GetOutputSize(encryptBytes.Length)];
            
            aesCipher.Reset();
            aesCipher.Init(false, cipherParameters);
            int length = aesCipher.ProcessBytes(encryptBytes, comparisonBytes, 0);
            aesCipher.DoFinal(comparisonBytes,length); //Do the final block
            
            return Encoding.UTF8.GetString(comparisonBytes);
        }

    }
}
