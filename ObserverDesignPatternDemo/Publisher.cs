namespace ObserverDesignPatternDemo
{
    using System;
    using System.Collections.Generic;

    public abstract class Publisher : IPublisher
    {
        private readonly List<ISubscriber> subscribers;

        protected Publisher()
        {
            subscribers = new List<ISubscriber>();
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            if (!subscribers.Contains(subscriber))
            {
                subscribers.Add(subscriber);
            }
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            if (subscribers.Contains(subscriber))
            {
                subscribers.Remove(subscriber);
            }
        }

        public void Notify(FishType type)
        {
            foreach (var subscriber in this.subscribers)
            {
                subscriber.Update(type);
            }
        }
    }
}
