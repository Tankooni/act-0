using System;
using System.Collections.Generic;

public interface IState {
    void UpdateState();
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
        currentState = state;
    }

    public void Update () {
        states[currentState].UpdateState();
    }
}

