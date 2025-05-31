using EventSourcingDemo.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Objects
{
    public class ItemStore
    {
        public List<IEvent> Events { get; private set; } = [];
        public List<Item> Items { get; private set; } = [];

        public void AddEvent(IEvent e)
        {
            Events.Add(e);
        }

        public void Undo()
        {
            if (Events.Count == 0)
                throw new InvalidOperationException("No events to undo.");

            Events.RemoveAt(Events.Count - 1);
        }

        public void ReturnToState(int index)
        {
            if (index < 0 || index >= Events.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            Events.RemoveRange(index + 1, Events.Count - index - 1);
        }

        public List<Item> StreamEvents()
        {
            Items.Clear();
            foreach (var e in Events)
                e.Apply(Items);

            return Items;
        }

        public List<Item> StreamEvents(int endIndex)
        {
            if (endIndex < 0 || endIndex >= Events.Count)
                throw new ArgumentOutOfRangeException(nameof(endIndex), "End index is out of range.");
            Items.Clear();
            for (int i = 0; i <= endIndex; i++)
                Events[i].Apply(Items);

            return Items;
        }
    }
}
