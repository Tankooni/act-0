using UnityEngine;
using States.Monster;
using System;

[RequireComponent(typeof(StatSystem))]
public class HydriaAI : BaseAI
{
    StatSystem stats;
    public GameObject player;

    public StateAggressive sAggressive;
    public StateNeutral sNeutral;
    public StateFriendly sFriendly;

    void Start()
    {
        //Not the best way to get the player but it'll have to do for now
        player = GameObject.Find("Player");

        sAggressive.setParent(this);
        sNeutral.setParent(this);
        sFriendly.setParent(this);

        stats = this.GetComponent<StatSystem>();

        baseState.AddState("aggressive", sAggressive);
        baseState.AddState("neutral", sNeutral);
        baseState.AddState("friendly", sFriendly);

        baseState.ChangeState("neutral");
    }

    [System.Serializable]
    public class StateFriendly : State {
        private HydriaAI parent;

        public StateFriendly(){
        }

        public void setParent(HydriaAI parent) {
            this.parent = parent;
        }

        public override void UpdateState() {

        }

        public override void EnterState() {}
        public override void LeaveState() {}
    }
}

