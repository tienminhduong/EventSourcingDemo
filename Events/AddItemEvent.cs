using EventSourcingDemo.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Events
{
    public record AddItemEvent(Item item) : IEvent
    {
        public void Apply(object target)
        {
            if (target is not ObservableCollection<Item> items)
                throw new ArgumentException("Target must be a ObservableCollection<Item>", nameof(target));

            items.Add(item);
        }

        public string Log => $"Time stamp: {TimeStamp}, Added item: {item.Name}, Amount: {item.Amount}, Price: {item.Price}";

        public DateTime TimeStamp { get; init; } = DateTime.Now;

    }
}
