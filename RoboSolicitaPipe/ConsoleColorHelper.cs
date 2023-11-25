using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSolicitaPipe
{
    public static class ConsoleColorHelper
    {
        public enum Writes
        {
            Common,
            Information,
            Error
        }

        private static object _lock = new object();
        public static void ConsoleWrite(string value, Writes error = Writes.Common)
        {
            lock (_lock)
            {
                switch ((int)error)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"[{DateTime.Now}]\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(value);
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(value);
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"[{DateTime.Now}]\t");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(value);
                        break;
                    default:
                        break;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }

        public static void ConsoleWriteException(Exception exception)
        {
            var msg = string.Empty;
            var br = "\n\n";
            if (exception.Data?.Keys.Count > 0)
            {
                foreach (var key in exception.Data.Keys)
                {
                    if (exception.Data[key] is null)
                        continue;
                    else
                        msg += $"{br}{key}:\t{exception.Data[key]?.ToString()}";
                }
            }

            msg += $"{br}Message: {exception.Message}" +
                   $"{br}StackTrace: {exception.StackTrace}" +
                   $"{br}Source: {exception.Source}";

            ConsoleWrite(msg, Writes.Error);
        }
    }
}
