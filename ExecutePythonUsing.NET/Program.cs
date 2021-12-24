using System;
using System.Diagnostics;
using System.IO;

namespace ExecutePythonUsing.NET
{
    class Program   //uruchamia skrypt pythona będący kalkulatorem błędu względnego: (abs(wynik - wzorcowyWynik) / wzorcowyWynik) * 100
    {               //skrypt powstał na potrzeby tsp PEA i przyjmuje plik z pomiarami (każdy pomiar w nowej linii) oraz tworzy plik z wartościami błędu względnego
        static void Main(string[] args)         //uruchamiając program z wiersza poleceń lub innego programu (np skryptu Pythona :) ) podać argumenty
        {
            Console.WriteLine(args.Length);
            string pythonInterpreter = string.Format(@"{0}", args[0]);  //ścieżka do interpretera pythona (zazwyczaj wystarczy "python" jeśli jest zainstalowany)
            string pythonFile = string.Format(@"{0}", args[1]);         //fineName.py - skrypt do uruchomienia 
            Console.WriteLine(args[4]);
            string pythonArgs = string.Format(@"{0} {1} {2}", args[2], args[3], args[4]);   //argumenty dla programu w pythonie podane kolejno oddzielone spacją (w cmd)
            Console.WriteLine(pythonInterpreter);                                           //dokladnie to plik wejsciowy, plik wyjsciowy i najlepszy znany wynik tsp
            Console.WriteLine(pythonFile);
            Console.WriteLine(pythonArgs);

            string result = RunFromCmd(pythonInterpreter, pythonFile, pythonArgs);          //metoda zwracająca to co python wypisuje poprzez print("sth")
            Console.WriteLine(result);                                                      //było na testy innego programu, a w przypadku tego kalkulatora jest zbędne
        }

        private static string RunFromCmd(string pythonInterpreter, string pythonFile, string pythonArgs)
        {
            string result = "";
            try
            {
                var procesInfo = new ProcessStartInfo();                                    //podajemy informacje (parametry) dla procesu (programu) który chcemy uruchomić
                procesInfo.FileName = pythonInterpreter;                                    //normalnie piszemy w konsoli python script.py arg1 arg2...
                procesInfo.Arguments = pythonFile + " " + pythonArgs;                       //tutaj nazwa skryptu.py i jego argumenty
                procesInfo.RedirectStandardOutput = true;                                   //będzie pobierane to co byłoby wypisane w konsoli
                procesInfo.UseShellExecute = false;

                using(Process process = new())
                {
                    Console.WriteLine("powstał nowy proces");                               //w celach debugowania :)
                    process.StartInfo = procesInfo;
                    process.Start();
                    process.WaitForExit();                                                  //poczekanie aż python obliczy
                    if(process.ExitCode == 0)
                    {
                        result = process.StandardOutput.ReadToEnd();                        //odczyt konsoli
                        Console.WriteLine("python skończył pracę");
                    }
                }
                return result;
            }
            catch(Exception e)
            {
                throw new Exception("Python program failed: " + result, e);                 //wypisanie błędu (Error) zwróconego przez interpreter pythona
            }
        }
    }
}
