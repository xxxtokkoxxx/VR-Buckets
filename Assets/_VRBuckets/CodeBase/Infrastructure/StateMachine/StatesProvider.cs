using System;
using System.Collections.Generic;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class StatesProvider : IStatesFactory
    {
        private readonly IEnumerable<IState> _states;
        public StatesProvider(IEnumerable<IState> states)
        {
            _states = states;
        }

        public Dictionary<Type, IState> CreateStates()
        {
            Dictionary<Type, IState> states = new Dictionary<Type, IState>();

            foreach (IState state in _states)
            {
                states.Add(state.GetType(), state);
            }

            return states;
        }
    }
}