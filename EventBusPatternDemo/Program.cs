using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusPatternDemo
{
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            //定义钓鱼人
            var fishMan = new FishMan("LiLei");
            var fishMan2 = new FishMan("HanMeimei");

            //定义鱼竿
            var fishTool = new FishTool();
            var fishTool2 = new FishTool();

            //钓鱼人订阅鱼竿 - 当有鱼上钩，发出响铃
            fishTool.FishingEvent += fishMan.Update;
            fishTool2.FishingEvent += fishMan2.Update;

            //定义钓鱼场景
            while (true)
            {
                Thread.Sleep(5000);
                fishTool.Fishing();
                fishTool2.Fishing();
            }
        }
    }
}
