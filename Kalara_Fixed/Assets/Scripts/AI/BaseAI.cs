using UnityEngine;

public abstract class BaseAI : MonoBehaviour
{
    protected StateMachine baseState = new StateMachine();

    public void Update () {
        baseState.Update();
    }
}