using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverDesignPatternDemo
{
    public interface IPublisher
    {
        void AddSubscriber(ISubscriber subscriber);

        void RemoveSubscriber(ISubscriber subscriber);

        void Notify(FishType type);
    }
}
