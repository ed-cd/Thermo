using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using MyCollections;
using TestEvents;

namespace MyCollections
{    
    // A class that works just like ArrayList, but sends event
    // notifications whenever the list changes.
    public class ListWithChangedEvent<T> : List<T>
    {
        public delegate void ChangedEventHandler(object sender, MyEventArgs<T> e);

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event ChangedEventHandler Changed;

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(MyEventArgs<T> e)
        {
            Changed?.Invoke(this, e);
        }
      
        // Override some of the methods that can change the list;
        // invoke event after each
        public new void Add(T value)
        {
            base.Add(value);
            OnChanged(new MyEventArgs<T> { Value = value });
        }

        public new void Clear()
        {
            base.Clear();
            OnChanged(new MyEventArgs<T>());
        }

        public new T this[int index]
        {
            set
            {
                base[index] = value;
                OnChanged(new MyEventArgs<T>(){Value = value});
            }
        }
    }
}

namespace TestEvents
{
    public class EventListener<T>
    {
        private ListWithChangedEvent<string> List;

        public EventListener(ListWithChangedEvent<string> list)
        {
            List = list;
            List.Changed += ListChanged;
        }

        // This will be called whenever the list changes.
        private static void ListChanged(object sender, MyEventArgs<string> e)
        {
            Console.WriteLine($"This is called when the event fires:{e.Value} at time {e.Time}");
        }

        public void Detach()
        {
            // Detach the event and delete the list
            List.Changed -= ListChanged;
            List = null;
        }
    }

    public class Test
    {
        // Test the ListWithChangedEvent class.
        public static void Main()
        {
            // Create a new list.
            var list = new ListWithChangedEvent<string>();

            // Create a class that listens to the list's change event.
            var listener = new EventListener<string>(list);

            // Add and remove items from the list.
            list.Add("item 1");
            list.Clear();
            listener.Detach();
        }
    }

    public class MyEventArgs<T> : EventArgs
    {
        public T Value { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}