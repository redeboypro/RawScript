using System;
using System.IO;
using System.Text;
using RawScript;

namespace RawToolkit
{
    [Instance("file")]
    public class RawFile : Struct
    {
        private char[][] data;
        private int linesCount;
        
        public RawFile(Engine engine) : base(engine)
        {
            Add("read", (variables, parameters) =>
            {
                var lines = File.ReadAllLines(parameters.ParametersToString());
                linesCount = lines.Length;
                data = new char[linesCount][];
                for (var index = 0; index < lines.Length; index++)
                {
                    data[index] = lines[index].ToCharArray();
                }
            });
            Add("getLinesCount", (variables, parameters) => linesCount);
            Add("getLength", (variables, parameters) => data[Convert.ToInt32(parameters[0])].Length);
            Add("get", (variables, parameters) => Convert.ToInt32(
                data[Convert.ToInt32(parameters[0])]
                    [Convert.ToInt32(parameters[1])]));
        }
    }
}