using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YLManager.Logger
{
    /// <summary>
    /// 로그관련 기능담은 클래스
    /// </summary>
    public class LogControl
    {
        /// <summary>
        /// [1]. Base Direcotry 경로 [변경불가]
        /// </summary>
        public static readonly string BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 로그파일 경로
        /// </summary>
        public static string LogPath { get; set; }

        /// <summary>
        /// 세팅파일 경로
        /// </summary>
        public static string SettingPath { get; set; }

        /// <summary>
        /// 로그폴더 생성 - BaseDirectory\\SystemLog
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dirname"></param>
        /// <returns></returns>
        public static bool CreateLogFolder()
        {
            // SystemLog - 년도 - 월 - 일
            Console.WriteLine(BaseDirectoryPath);

            try
            {
                LogPath = string.Format(@"{0}\\SystemLog", BaseDirectoryPath);

                DirectoryInfo di = new DirectoryInfo(LogPath);

                if (!di.Exists)
                {
                    di.Create();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 로그메시지 출력 + 날짜 디렉터리 생성
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string LogMessage(string message)
        {
            try
            {
                DateTime thisday = DateTime.Now;

                string path = string.Format(@"{0}/{1}", LogPath, thisday.Year);

                DirectoryInfo di = new DirectoryInfo(path);

                // 년도 디렉터리 생성
                if (!di.Exists) di.Create();

                // 월
                path = string.Format(@"{0}/{1}", path, thisday.Month);
                di = new DirectoryInfo(path);
                if (!di.Exists) di.Create();

                // 일
                string filepath = Path.Combine(path, String.Format("{0}_{1}_{2}.txt", thisday.Year, thisday.Month, thisday.Day));

                // 일.txt + 로그내용
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    System.Diagnostics.StackTrace objStackTrace = new System.Diagnostics.StackTrace(new System.Diagnostics.StackFrame(1));
                    var s = objStackTrace.ToString(); // 호출한 함수 위치
                    writer.WriteLine($"[{thisday.ToString()}]\t{message}");
                }

                return $"[{thisday.ToString()}]\t{message}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 설정폴더 생성 - BaseDirectory\\SystemSetting
        /// </summary>
        public static bool CreateSettingFolder()
        {
            // SystemSetting 
            try
            {
                SettingPath = string.Format(@"{0}\\SystemSetting", BaseDirectoryPath);

                DirectoryInfo di = new DirectoryInfo(SettingPath);

                if (!di.Exists)
                {
                    di.Create();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 세팅파일 저장
        /// </summary>
        /// <param name="setobj"></param>
        /// <returns></returns>
        public static string CreateSettingFile(string setobj)
        {
            try
            {
                string filepath = Path.Combine(SettingPath, String.Format("{0}.txt", "SettingFile"));

                // 일.txt + 로그내용
                using (StreamWriter writer = new StreamWriter(filepath, false))
                {
                    System.Diagnostics.StackTrace objStackTrace = new System.Diagnostics.StackTrace(new System.Diagnostics.StackFrame(1));
                    var s = objStackTrace.ToString(); // 호출한 함수 위치
                    writer.WriteLine($"{setobj}");
                }

                return setobj;
            }
            catch(Exception ex)
            {
                LogMessage("세팅파일 저장에러");
                return null;
            }
        }


    }
}
