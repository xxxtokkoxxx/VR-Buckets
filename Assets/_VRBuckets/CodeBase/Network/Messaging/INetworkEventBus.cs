using System;

namespace _VRBuckets.CodeBase.Network.Messaging
{
    public interface INetworkEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : INetworkEvent;
        void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : INetworkEvent;
        void Publish<TEvent>(TEvent eventData) where TEvent : INetworkEvent;
        void RemoveAllSubscriptions();
    }
}