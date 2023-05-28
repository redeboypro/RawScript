using System;
using System.Collections.Generic;
using System.Text;
using RawScript;

namespace RawToolkit
{
    [Instance("grid")]
    public class RawGrid : Struct
    {
        private object[,] grid;
        
        public RawGrid(Engine engine) : base(engine)
        {
            Add("capacity", (variables, parameters) =>
            {
                grid = new object[Convert.ToInt32(parameters[0]),Convert.ToInt32(parameters[1])];
            });

            Add("set", (variables, parameters) =>
            {
                grid[Convert.ToInt32(parameters[0]), Convert.ToInt32(parameters[1])] = parameters[2];
            });
            
            Add("get", (variables, parameters) => grid[Convert.ToInt32(parameters[0]), Convert.ToInt32(parameters[1])]);
            
            Add("getLength", (variables, parameters) => grid.GetLength(Convert.ToInt32(parameters[0])));
            
            Add("print", (variables, parameters) =>
            {
                for (var y = 0; y < grid.GetLength(0); y++)
                {
                    var tokens = new List<string>();
                    for (var x = 0; x < grid.GetLength(1); x++)
                    {
                        tokens.Add(grid[y, x].ToString());
                    }
                    engine.Terminal.Print(tokens.JoinTokens());
                }
            });
        }
    }
}