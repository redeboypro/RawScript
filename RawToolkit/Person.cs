using RawScript;

namespace RawToolkit
{
    [Instance("person")]
    public class Person : Struct
    {
        public Person(Engine engine) : base(engine)
        {
            Add("get", (variables, parameters) => (float) parameters[0] + (float) parameters[1]);
        }
    }
}