namespace UnitTestDemo
{
    using System;

    public interface IDistribute
    {
        void ToNotice(string message);
    }

    public class Distribute : IDistribute
    {
        public void ToNotice(string message)
        {
            Console.WriteLine(message);
        }
    }
}