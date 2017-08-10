namespace ObserverDesignPatternDemo_Event
{
    using System;

    public class FishMan : ISubscriber
    {
        private readonly string name;

        private int fishCount;

        public FishMan(string name)
        {
            this.name = name;
        }

        public void Update(FishType type)
        {
            this.fishCount++;

            Console.WriteLine($@"{this.name}调到了一条[{type}],这是他调到的第{this.fishCount}条鱼了!");
        }
    }
}
