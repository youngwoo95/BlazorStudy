using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YLManager.Encryption
{
    public class AES256
    {
        /// <summary>
        /// KEY 값 생성
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CreateKey(int length)
        {
            // AES256의 키는 29글자 보다 커야함.
            if (length >= 29)
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
                return "Key값은 29글자 이상이어야 합니다.";
            }
        }

        /// <summary>
        /// AES-256 암호화 256BIT 까지 지원
        /// </summary>
        /// <param name="plain">평문</param>
        /// <param name="key">암호키</param>
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
                rm.KeySize = 256;

                // 메모리스트림 생성
                MemoryStream memoryStream = new MemoryStream();

                // key, iv값 정의
                ICryptoTransform encryptor = rm.CreateEncryptor(Encoding.UTF8.GetBytes(key.Substring(0, 256 / 8)), Encoding.UTF8.GetBytes(key.Substring(0, 128 / 8)));

                // 크립토스트림을 키와 iv값으로 메모리스트림을 이용하여 생성
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                // 크립토스트림에 바이트배열을 쓰고 플러시
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();

                // 메모리스트림에 담겨있는 암호화된 바이트배열을 담음
                byte[] encryptBytes = memoryStream.ToArray();

                // Base64로 변환
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
        /// AES - 256 복호화 256BIT 까지 지원
        /// </summary>
        /// <param name="plain"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string plain, string key)
        {
            try
            {
                // base64를 바이트로 변환
                byte[] encryptBytes = Convert.FromBase64String(plain);

                // 레인달 알고리즘
                RijndaelManaged rm = new RijndaelManaged();

                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                rm.KeySize = 256;

                // 메뫼스트림 생성
                MemoryStream memoryStream = new MemoryStream(encryptBytes);

                // key, iv값 정의
                ICryptoTransform decryptor = rm.CreateDecryptor(Encoding.UTF8.GetBytes(key.Substring(0, 256 / 8)), Encoding.UTF8.GetBytes(key.Substring(0, 128 / 8)));

                // 크립토스트림을 Key와 iv값으로 메모리스트림을 이용하여 생성
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
