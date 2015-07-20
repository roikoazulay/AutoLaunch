using System.Collections.Generic;
using AutomationServer;

namespace AutomationClient.Models.Memento
{
    public class Originator
    {
        private Stack<MementoScript> _mementoStack = new Stack<MementoScript>();

        public void SaveToMemento(ScriptObj script)
        {
            _mementoStack.Push(new MementoScript(script));
        }

        public ScriptObj Restore()
        {
            if (_mementoStack.Count == 0)
                return new ScriptObj();

            return _mementoStack.Pop().SavedState;
        }
    }

    public class MementoScript
    {
        public readonly ScriptObj SavedState;

        public MementoScript(ScriptObj stateToSave)
        {
            SavedState = new ScriptObj();
            foreach (var entity in stateToSave.Entities)
                SavedState.Entities.Add(entity);
        }
    }
}