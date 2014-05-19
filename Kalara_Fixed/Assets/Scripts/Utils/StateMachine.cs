using System;
using System.Collections.Generic;

public class State {
    public virtual void EnterState(){}
    public virtual void UpdateState(){}
    public virtual void LeaveState(){}
}

public class StateMachine
{
    private Dictionary<string, State> states = new Dictionary<string, State>();
    private string currentState;

    public StateMachine() {}

    public void AddState (string name, State stateobj) {
        states.Add(name, stateobj);
    }
    public void AddState(KeyValuePair<string, State>[] stateList) {
        foreach(var state in stateList) {
            states.Add(state.Key, state.Value);
        }
    }

    public void RemoveState (string name) {
        states.Remove(name);
    }

    public void ChangeState(string state) {
        if(currentState != null) {
            states[currentState].LeaveState();
            currentState = state;
            states[currentState].EnterState();
        } else {
            currentState = state;
        }
    }

    public void Update () {
        states[currentState].UpdateState();
    }
}

