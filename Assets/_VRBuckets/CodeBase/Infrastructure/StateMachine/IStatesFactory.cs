using System;
using System.Collections.Generic;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public interface IStatesFactory
    {
        Dictionary<Type, IState> CreateStates();
    }
}