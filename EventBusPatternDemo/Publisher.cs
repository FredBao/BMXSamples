namespace EventBusPatternDemo
{
    using System;
    using System.Collections.Generic;

    public class Publisher
    {
        public delegate void FishingHandler(FishType type);
    }
}
