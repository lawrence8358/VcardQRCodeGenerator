using System; 

namespace VcardQRCodeGenerator.Utility
{
    internal class LogObj
    {
        static object infoLock = new object();
        static object errorLock = new object();

        public void Info(string message, string type = "Info")
        {
            lock (infoLock) this.Write(message, type);
        }

        public void Error(string message)
        {
            lock (errorLock) this.Write(message, "Exception");
            Console.WriteLine($"Error :: {DateTime.Now.ToString("HH:mm:ss")} :: {message}");
        }

        private void Write(string message, string type)
        {
            string logDir = "Log";
            string logFile = DateTime.Now.ToString("yyyyMMdd");
            if (string.IsNullOrEmpty(logFile)) logFile = DateTime.Now.ToString("yyyyMMdd");
            logFile += ".txt";

            string path = Path.Combine(logDir, logFile);
            if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} :: {message}");
            }
        }
    }
}
