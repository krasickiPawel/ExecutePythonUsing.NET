using System;
using System.Diagnostics;
using System.IO;

namespace ExecutePythonUsing.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args.Length);
            //string pythonInterpreter = @"C:\Users\Paweł\AppData\Local\Programs\Python\Python39\python.exe";
            //string pythonInterpreter = "python";
            //string pythonFile = @"C:\Users\Paweł\PycharmProjects\wiki\calcTest.py";
            //string pythonFile = @"C:\Users\Paweł\PycharmProjects\wiki\calcPEA.py";
            //string pythonArgs = "81 39";
            //string pythonArgs = @"C:\Users\Paweł\PycharmProjects\wiki\_TEMP.txt C:\Users\Paweł\PycharmProjects\wiki\_TEMP_result.txt 39";
            string pythonInterpreter = string.Format(@"{0}", args[0]);
            string pythonFile = string.Format(@"{0}", args[1]);
            //string pythonArgs = args[3].Substring(1, args[3].Length - 2);
            Console.WriteLine(args[4]);
            string pythonArgs = string.Format(@"{0} {1} {2}", args[2], args[3], args[4]);
            Console.WriteLine(pythonInterpreter);
            Console.WriteLine(pythonFile);
            Console.WriteLine(pythonArgs);

            string result = RunFromCmd(pythonInterpreter, pythonFile, pythonArgs);
            Console.WriteLine(result);
        }

        private static string RunFromCmd(string pythonInterpreter, string pythonFile, string pythonArgs)
        {
            string result = "";
            try
            {
                var procesInfo = new ProcessStartInfo();
                procesInfo.FileName = pythonInterpreter;
                procesInfo.Arguments = pythonFile + " " + pythonArgs;
                procesInfo.RedirectStandardOutput = true;
                procesInfo.UseShellExecute = false;

                using(Process process = new())
                {
                    Console.WriteLine("powstał nowy proces");
                    process.StartInfo = procesInfo;
                    process.Start();
                    process.WaitForExit();
                    if(process.ExitCode == 0)
                    {
                        result = process.StandardOutput.ReadToEnd();
                        Console.WriteLine("python skończył pracę");
                    }
                }
                return result;
            }
            catch(Exception e)
            {
                throw new Exception("Python program failed: " + result, e);
            }
        }
    }
}
