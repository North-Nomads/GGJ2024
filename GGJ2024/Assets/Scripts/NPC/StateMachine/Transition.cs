using System;
using System.Collections.Generic;
using System.Linq;
using NPC.StateMachine.States;

namespace NPC.StateMachine
{
    public class Transition
    {
        private readonly List<Func<bool>> _conditions;
        private readonly IState _to;

        public bool IsAnyConditionCompleted => _conditions.Any(isCompleted => isCompleted.Invoke());
        public IState To => _to;

        public Transition(IState to, params Func<bool>[] conditions)
        {
            _to = to;
            _conditions = conditions.ToList();
        }

        public void AddCondition(Func<bool> condition) => 
            _conditions.Add(condition);

        public void RemoveCondition(Func<bool> condition)
        {
            if (_conditions.Contains(condition))
                _conditions.Remove(condition);
        }
    }
}