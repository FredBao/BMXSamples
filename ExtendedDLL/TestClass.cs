namespace ExtendedDLL
{
    using System;

    public class TestClass : MarshalByRefObject
    {
        public string Print()
        {
            return "Version 22";
        }
    }
}