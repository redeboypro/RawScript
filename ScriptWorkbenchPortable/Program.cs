using System;

namespace ScriptWorkbenchPortable
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var display = new Display();
            display.ShowDialog();
        }
    }
}