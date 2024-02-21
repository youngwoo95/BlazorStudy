using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;

/*
    LogControl.CreateLogFolder(); // Load에 생성
    LogControl.CreateSettingFolder(); // Load에 생성

    LogControl.LogMessage("로그메시지"); // 로그메시지 try~catch 에 생성
    LogControl.CreateSettingFile("세팅파일12"); // 세팅파일 저장버튼에 생성
*/

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
        /// 로그폴더 생성 - BaseDirectory\\SystemLog (프로그램 설치파일 위치)
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
        /// 로그메시지 출력 + 날짜 디렉터리 생성 (프로그램 설치파일 위치)
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
        /// 설정폴더 생성 - BaseDirectory\\SystemSetting (프로그램 설치파일 위치)
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
        /// 세팅파일 저장 (프로그램 설치파일 위치)
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

        /// <summary>
        /// 스케줄러 등록 - 자동 실행 설정
        /// </summary>
        public static bool CreateScheduler(string path, string fileName, string fileDesc)
        {
            try
            {
                bool check = YLManager.Field.FieldControl.StringEmptyCheck(path);
                if (check)
                {
                    return false;
                }

                if (!IsAdministrator()) // 관리자 권한으로 실행중인지 여부
                {
#if DEBUG
                    Console.WriteLine("관리자 권한이 아닙니다.");
#endif
                    return false;
                }

                using (TaskService taskService = new TaskService())
                {
                    TaskDefinition taskDefinition = taskService.NewTask();

                    // 일반
                    taskDefinition.Principal.DisplayName = fileName; // 작업스케줄명
                    taskDefinition.RegistrationInfo.Description = fileDesc; // 스케줄 설명
                    LogonTrigger login = new LogonTrigger();
                    taskDefinition.Principal.UserId = string.Concat(Environment.UserDomainName, "\\", Environment.UserName);
                    taskDefinition.Principal.LogonType = TaskLogonType.InteractiveToken;
                    taskDefinition.Principal.RunLevel = TaskRunLevel.Highest;
                    taskDefinition.Triggers.Add(login);

                    // 조건
                    taskDefinition.Settings.MultipleInstances = TaskInstancesPolicy.IgnoreNew;
                    taskDefinition.Settings.DisallowStartIfOnBatteries = false;
                    taskDefinition.Settings.StopIfGoingOnBatteries = false;
                    taskDefinition.Settings.AllowHardTerminate = false;
                    taskDefinition.Settings.StartWhenAvailable = false;
                    taskDefinition.Settings.RunOnlyIfNetworkAvailable = false;
                    taskDefinition.Settings.IdleSettings.StopOnIdleEnd = false;
                    taskDefinition.Settings.IdleSettings.RestartOnIdle = false;

                    // 설정
                    taskDefinition.Settings.AllowDemandStart = false;
                    taskDefinition.Settings.Enabled = true;
                    taskDefinition.Settings.Hidden = false;
                    taskDefinition.Settings.RunOnlyIfIdle = false;
                    taskDefinition.Settings.RunOnlyIfIdle = false;
                    taskDefinition.Settings.ExecutionTimeLimit = TimeSpan.Zero;
                    taskDefinition.Settings.Priority = System.Diagnostics.ProcessPriorityClass.High;

                    // 동작
                    taskDefinition.Actions.Add(path); // 동작시킬 경로

                    taskService.RootFolder.RegisterTaskDefinition(fileName, taskDefinition); // 스케줄러 레지스터 등록
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 스케줄 삭제
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool DeleteScheduler(string fileName)
        {
            try
            {
                if (!IsAdministrator()) // 관리자 권한으로 실행중인지 여부
                {
#if DEBUG
                    Console.WriteLine("관리자 권한이 아닙니다.");
#endif
                    return false;
                }

                using (TaskService taskService = new TaskService())
                {
                    taskService.RootFolder.DeleteTask(fileName);
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 관리자 권한으로 실행중인지 확인
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            bool isAdmin;
            WindowsIdentity user = null;
            try
            {
                user = WindowsIdentity.GetCurrent(); // 현재 로그인된 user의 정보
                if (user == null)
                    throw new InvalidOperationException("Couldn't get the current user identity");

                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator); // 관리자 권한인지 여부
            }
            catch(UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch(Exception ex)
            {
                isAdmin = false;
            }
            finally
            {
                if (user != null)
                    user.Dispose();
            }

            return isAdmin;
        }


    }
}
