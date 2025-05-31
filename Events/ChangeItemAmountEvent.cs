using EventSourcingDemo.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Events
{
    public class ChangeItemAmountEvent(int itemIndex, int NewAmount) : IEvent
    {
        public void Apply(object target)
        {
            if (target is not List<Item> items)
                throw new ArgumentException("Target must be a List<Item>", nameof(target));

            if (itemIndex < 0 || itemIndex >= items.Count)
                throw new ArgumentOutOfRangeException(nameof(itemIndex), "Item index is out of range.");

            items[itemIndex].Amount = NewAmount;
        }
        public string Log => $"Time stamp: {TimeStamp}, Changed amount of item at index {itemIndex} to {NewAmount}";
        public DateTime TimeStamp { get; init; } = DateTime.Now;
    }
}
