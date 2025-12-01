using System;
using System.Collections.Generic;

namespace _VRBuckets.CodeBase.Network.Messaging
{
    public class NetworkEventBus : INetworkEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _eventHandlers = new();

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : INetworkEvent
        {
            Type eventType = typeof(TEvent);

            if (!_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType] = new List<Delegate>();
            }

            _eventHandlers[eventType].Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : INetworkEvent
        {
            Type eventType = typeof(TEvent);

            if (_eventHandlers.TryGetValue(eventType, out List<Delegate> handlers))
            {
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    _eventHandlers.Remove(eventType);
                }
            }
        }

        public void Publish<TEvent>(TEvent eventData) where TEvent : INetworkEvent
        {
            if (eventData == null)
                return;

            Type eventType = typeof(TEvent);

            if (_eventHandlers.TryGetValue(eventType, out List<Delegate> handlers))
            {
                Delegate[] handlersCopy = handlers.ToArray();

                foreach (Delegate handler in handlersCopy)
                {
                    if (handler is Action<TEvent> typedHandler)
                    {
                        typedHandler.Invoke(eventData);
                    }
                }
            }
        }

        public void RemoveAllSubscriptions()
        {
            _eventHandlers.Clear();
        }
    }
}