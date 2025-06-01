using EventSourcingDemo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.EDA
{
    public delegate void ListenerAction(IEvent @event);
    public static class EventBroker
    {
        private static Dictionary<string, List<ListenerAction>> listeners = new();
        public static void Subscribe(string eventType, ListenerAction action)
        {
            if (!listeners.ContainsKey(eventType))
            {
                listeners[eventType] = new List<ListenerAction>();
            }
            listeners[eventType].Add(action);
        }

        public static void Unsubscribe(string eventType, ListenerAction action)
        {
            if (listeners.ContainsKey(eventType))
            {
                listeners[eventType].Remove(action);
                if (listeners[eventType].Count == 0)
                {
                    listeners.Remove(eventType);
                }
            }
        }

        public static void Publish(IEvent @event)
        {
            string eventType = @event.GetType().Name;
            if (listeners.ContainsKey(eventType))
            {
                foreach (var action in listeners[eventType])
                {
                    action(@event);
                }
            }
        }
    }
}
