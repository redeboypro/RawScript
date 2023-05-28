using System;
using System.Linq;
using System.Reflection;

namespace RawScript
{
    public class ExtractedStruct : Struct
    {
        private const string ConstructorInitialization = "__init__";
        private const string SetterPrefix = "set";
        private const string GetterPrefix = "get";
        
        private object instance;

        public ExtractedStruct(Type type, Engine engine) : base(engine)
        {
            Add(ConstructorInitialization, (variables, parameters) =>
            {
                instance = Activator.CreateInstance(type, parameters);
            });

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                Add(SetterPrefix + ToUpper(field.Name), (variables, parameters) =>
                {
                    field.SetValue(instance, parameters[0]);
                });
                
                Add(GetterPrefix + ToUpper(field.Name), (variables, parameters) => field.GetValue(instance));
            }

            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (var method in methods)
            {
                if (method.ReturnType.IsAssignableFrom(typeof(void)))
                {
                    Add(method.Name, (variables, parameters) =>
                    {
                        method.Invoke(instance, parameters);
                    });
                    continue;
                }
                
                Add(method.Name, (variables, parameters) => method.Invoke(instance, parameters));
            }
        }
        
        private static string ToUpper(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}