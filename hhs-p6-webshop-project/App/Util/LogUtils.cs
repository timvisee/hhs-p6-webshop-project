using System;

namespace hhs_p6_webshop_project.App.Util {
    public static class LogUtils {

        /// <summary>
        /// Log a message to the console.
        /// </summary>
        /// <param name="text">Text to log.</param>
        /// <param name="color">Color to log with.</param>
        public static void Log(string text, ConsoleColor color = ConsoleColor.Red) {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}