using EventSourcingDemo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Events
{
    public class RemoveItemEvent(int index) : IEvent
    {
        public DateTime TimeStamp { get; init; } = DateTime.Now;

        public string Log => $"Time stamp: {TimeStamp}, Removed item at index {index}";

        public void Apply(object target)
        {
            if (target is not List<Item> items)
                throw new ArgumentException("Target must be a List<Item>", nameof(target));
            if (index < 0 || index >= items.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Item index is out of range.");
            items.RemoveAt(index);
        }
    }
}
