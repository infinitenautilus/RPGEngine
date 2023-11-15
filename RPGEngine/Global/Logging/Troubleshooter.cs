using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RPGEngine.Global.Logging
{
    public class Troubleshooter
    {
        private static readonly Lazy<Troubleshooter> _troubleshooter = new Lazy<Troubleshooter>(() => new Troubleshooter());
        public static readonly Troubleshooter Instance = _troubleshooter.Value;

        public const string PATHTOLOGDIRECTORY = "./log/";
        private const string LOGFILENAME = "log.txt";
        private string logFilePath = Path.Combine(PATHTOLOGDIRECTORY, LOGFILENAME);

        private Troubleshooter()
        {
            InitializeLogDirectory();
            ClearLog();
        }

        private void InitializeLogDirectory()
        {
            if (!Directory.Exists(PATHTOLOGDIRECTORY))
            {
                Directory.CreateDirectory(PATHTOLOGDIRECTORY);
            }
            
        }

        private void ClearLog()
        {
            if (File.Exists(logFilePath))
            {
                try
                {
                    File.Delete(logFilePath);
                    File.Create(logFilePath).Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ClearLog called and failed:: {ex.Message}.");
                }
            }
        }

        public void Log(string message)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff");
            string formattedMessage = $"@{timeStamp} : {message}";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(formattedMessage);
            Console.ForegroundColor = ConsoleColor.White;

            File.AppendAllText(logFilePath, formattedMessage + Environment.NewLine);
        }


    }
}
