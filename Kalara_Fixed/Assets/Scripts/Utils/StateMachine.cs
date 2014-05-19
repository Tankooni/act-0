using System;
using System.Collections.Generic;

public interface IState {
    void EnterState();
    void UpdateState();
    void LeaveState();
}

public class StateMachine
{
    private Dictionary<string, IState> states = new Dictionary<string, IState>();
    private string currentState;

    public StateMachine() {}

    public void AddState (string name, IState stateobj) {
        states.Add(name, stateobj);
    }
    public void AddState(KeyValuePair<string, IState>[] stateList) {
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

