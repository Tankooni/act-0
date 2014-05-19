using UnityEngine;

public abstract class BaseAI : MonoBehaviour
{
    public StateMachine baseState = new StateMachine();

    public void Update () {
        baseState.Update();
    }
}