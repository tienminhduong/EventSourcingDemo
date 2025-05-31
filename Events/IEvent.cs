using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Events
{
    public interface IEvent
    {
        DateTime TimeStamp { get; }
        string Log { get; }


        void Apply(object target);
    }
}
