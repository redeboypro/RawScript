using System;
using System.IO;
using System.Windows.Forms;
using RawScript;

namespace RawExecutor
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var engine = new Engine();
            var terminal = engine.Terminal;
            
            var folderBrowser = new FolderBrowserDialog();
            var result = folderBrowser.ShowDialog();
            if (result is DialogResult.OK)
            {
                var files = Directory.GetFiles(folderBrowser.SelectedPath);
                foreach (var file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        engine.LoadAssemlyFromFile(file);
                    }
                }
                
                foreach (var file in files)
                {
                    if (file.EndsWith(".rs"))
                    {
                        engine.LoadFromFile(file);
                    }
                }
            }
            
            engine.Invoke("main.rs");
            for (var lineIndex = 0; lineIndex < terminal.Length; lineIndex++)
            {
                Console.WriteLine(terminal[lineIndex]);
            }

            while (!Console.KeyAvailable)
            {
                /*
                 * Not Implemented
                 */
            }
        }
    }
}