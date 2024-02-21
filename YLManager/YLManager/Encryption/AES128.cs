using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Createkey 생성 후 --> 어디 변수에 넣어둔뒤
// Encrytion 실행 --> 암호화된 내용
// Decrytion 실행 --> 원문

namespace YLManager.Encryption
{
    /// <summary>
    /// AES-128 암호화 & 복호화 클래스
    /// </summary>
    public class AES128
    {
        /// <summary>
        /// KEY 값 생성
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CreateKey(int length)
        {
            // AES-128의 키는 15글자 보다 커야함.
            if (length >= 15)
            {
                using (var crypto = new RNGCryptoServiceProvider())
                {
                    var bits = (length * 6);
                    var byte_size = ((bits + 7) / 8);
                    var bytesarray = new byte[byte_size];
                    crypto.GetBytes(bytesarray);
                    return Convert.ToBase64String(bytesarray);
                }
            }
            else
            {
                return "Key값은 15글자 이상이어야 합니다.";
            }
        }


        /// <summary>
        /// AES-128 암호화 128BIT 까지 지원
        /// </summary>
        /// <param name="plain">평문</param>
        /// <param name="key">키</param>
        /// <returns>암호화된 내용</returns>
        public static string Encrypt(string plain, string key)
        {
            try
            {
                // 바이트로 변환
                byte[] plainBytes = Encoding.UTF8.GetBytes(plain);

                // 레인달 객체 생성
                RijndaelManaged rm = new RijndaelManaged();

                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                rm.KeySize = 128;

                // 메모리스트림 생성
                MemoryStream memoryStream = new MemoryStream();

                // key, iv값 정의
                ICryptoTransform encryptor = rm.CreateEncryptor(Encoding.UTF8.GetBytes(key.Substring(0, 128 / 8)), Encoding.UTF8.GetBytes(key.Substring(0, 128 / 8)));

                // CryptoStream을 Key와 iv값으로 메모리스트림을 이용하여 생성
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                // CryptoStream에 바이트배열을 쓰고 플러시
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();

                // 메모리스트림에 담겨있는 암호화된 바이트배열을 담음
                byte[] encryptBytes = memoryStream.ToArray();

                // BASE64 CONVERTING
                string encryptString = Convert.ToBase64String(encryptBytes);

                // 스트림 닫기
                cryptoStream.Close();
                memoryStream.Close();

                return encryptString;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// AES-128 복호화 128BIT 까지 지원
        /// </summary>
        /// <param name="encrypt">암호화된 내용</param>
        /// <param name="key">키</param>
        /// <returns>평문</returns>
        public static string Decrypt(string encrypt, string key)
        {
            try
            {
                // BASE64를 바이트로 변환
                byte[] encryptBytes = Convert.FromBase64String(encrypt);

                // 레인달 객체 생성
                RijndaelManaged rm = new RijndaelManaged();

                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                rm.KeySize = 128;

                // 메모리스트림 생성
                MemoryStream memoryStream = new MemoryStream(encryptBytes);

                // Key, iv값 정의
                ICryptoTransform decryptor = rm.CreateDecryptor(Encoding.UTF8.GetBytes(key.Substring(0, 128 / 8)), Encoding.UTF8.GetBytes(key.Substring(0, 128 / 8)));

                // CryptoStream을 key와 iv값으로 메모리스트림을 이용하여 생성
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                // 복호화된 데이터를 담을 바이트 배열을 선언한다.
                byte[] plainBytes = new byte[encryptBytes.Length];

                int plainCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

                // 복호화된 바이트 배열을 string으로 변환
                string plainString = Encoding.UTF8.GetString(plainBytes, 0, plainCount);

                // 스트림 닫기
                cryptoStream.Close();
                memoryStream.Close();

                return plainString;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
