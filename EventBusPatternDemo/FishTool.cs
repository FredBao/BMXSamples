using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusPatternDemo
{
    public class FishTool : Publisher
    {
        public event FishingHandler FishingEvent;

        public void Fishing()
        {
            if (new Random().Next() % 2 == 0)
            {
                var type = (FishType)new Random().Next(0, 5);

                Console.WriteLine("铃铛：叮叮叮，鱼儿咬钩了");

                this.FishingEvent?.Invoke(type);
            }
        }
    }
}
