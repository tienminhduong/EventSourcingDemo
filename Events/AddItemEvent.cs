using EventSourcingDemo.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Events
{
    public record AddItemEvent(string name, int amount, float price) : IEvent
    {
        public void Apply(object target)
        {
            if (target is not List<Item> items)
                throw new ArgumentException("Target must be a List<Item>", nameof(target));

            items.Add(new(name, amount, price));
        }

        public string Log => $"Time stamp: {TimeStamp}, Added item: {name}, Amount: {amount}, Price: {price}";

        public DateTime TimeStamp { get; init; } = DateTime.Now;

    }
}
