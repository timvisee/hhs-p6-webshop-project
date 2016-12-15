using System;

namespace hhs_p6_webshop_project.App.Util {
    public static class LogUtils {

        /// <summary>
        /// Log a regular message to the console.
        /// </summary>
        /// <param name="msg">Text to log.</param>
        /// <param name="color">Console color to log with.</param>
        public static void Log(string msg, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Log a info message to the console.
        /// </summary>
        /// <param name="msg">Info message to log.</param>
        public static void Info(string msg) {
            Log(msg, ConsoleColor.White);
        }

        /// <summary>
        /// Log a success message to the console.
        /// </summary>
        /// <param name="msg">Success message to log.</param>
        public static void Success(string msg) {
            Log(msg, ConsoleColor.Green);
        }

        /// <summary>
        /// Log a warning message to the console.
        /// </summary>
        /// <param name="msg">Warning message to log.</param>
        public static void Warning(string msg) {
            Log(msg, ConsoleColor.Red);
        }
    }
}