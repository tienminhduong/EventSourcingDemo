using EventSourcingDemo.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.EDA
{
    public class Listener
    {

        public Listener()
        {
            EventBroker.Subscribe(nameof(AddItemEvent), OnItemAdded);
            //EventBroker.Subscribe(nameof(ChangeItemAmountEvent), OnItemAmountChanged);
        }

        public int TotalAmount { get; private set; } = 0;


        private void OnItemAdded(IEvent @event)
        {
            if (@event is AddItemEvent addItemEvent)
                TotalAmount += addItemEvent.amount;

            Trace.WriteLine($"Total amount after adding item: {TotalAmount}");
        }


        private void OnItemAmountChanged(IEvent @event)
        {
            if (@event is ChangeItemAmountEvent changeItemAmountEvent)
                TotalAmount += changeItemAmountEvent.NewInputAmount;

            Trace.WriteLine($"Total amount after changing item amount: {TotalAmount}");
        }
    }
}
