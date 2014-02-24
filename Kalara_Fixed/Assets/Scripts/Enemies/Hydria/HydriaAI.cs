using UnityEngine;

using System;

[RequireComponent(typeof(StatSystem))]
public class HydriaAI : BaseAI
{
    StatSystem stats;
    GameObject player;

    public StateAggressive sAggressive;
    public StateNeutral sNeutral;
    public StateFriendly sFriendly;

    void Start() {
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
    public class StateAggressive : IState {
        private HydriaAI parent;
        
        public StateAggressive(){
        }

        public void setParent(HydriaAI parent) {
            this.parent = parent;
        }
        
        void IState.UpdateState(){
        
        }
    }

    [System.Serializable]
    public class StateNeutral : IState {
        private HydriaAI parent;

        private StateMachine currentTask = new StateMachine();

        public float watchDistance;
        public float aggressiveDistance;
        public float wanderRadius;
        public float movespeed;

        private Vector3 startingPosition;

        private Vector3 targetDestination;
        private float distanceToTarget;

        public StateNeutral(){
            currentTask.AddState("stopped", new StateStopped(this));
            currentTask.AddState("walking", new StateWalking(this));
            currentTask.AddState("watching", new StateWatching(this));

            currentTask.ChangeState("stopped");
        }

        public void setParent(HydriaAI parent) {
            this.parent = parent;
            startingPosition = parent.transform.position;
        }

        void IState.UpdateState() {
            //Update
            currentTask.Update();

            //check if player is nearby
            distanceToTarget = Vector3.Distance(parent.player.transform.position, parent.transform.position);
                
            if(distanceToTarget < aggressiveDistance) {
                parent.baseState.ChangeState("aggressive");
                Debug.Log("AGGRESSIVE!");
            }
        }

        [System.Serializable]
        public class StateStopped : IState {
            StateNeutral parent;

            public StateStopped (StateNeutral parent) {
                this.parent = parent;
            }

            void IState.UpdateState() {
                //find a new place to walk to
                Vector2 rand2dVec = (MathEx.GenUnitVec2() * parent.wanderRadius);
                Debug.Log(rand2dVec);
                parent.targetDestination = parent.startingPosition + new Vector3(rand2dVec.x, 0 ,rand2dVec.y);
                parent.currentTask.ChangeState("walking");
            }
        }
        [System.Serializable]
        public class StateWatching : IState {
            StateNeutral parent;
            
            public StateWatching (StateNeutral parent) {
                this.parent = parent;
            }
            
            void IState.UpdateState() {
                //check if we should be walking now
                if(parent.distanceToTarget > parent.watchDistance) {
                    //player is walking away, resume the position
                    parent.currentTask.ChangeState("walking");
                } 
                
                //continue watching while player is close
                //turn toward player
                parent.parent.transform.LookAt(parent.parent.player.transform.position);
                //growl or whatever
            }
        }
        [System.Serializable]
        public class StateWalking : IState {
            StateNeutral parent;
            
            public StateWalking (StateNeutral parent) {
                this.parent = parent;
            }
            
            void IState.UpdateState() {
                //check if we should be watching now
                if(parent.distanceToTarget <= parent.watchDistance) {
                    //player is getting close, start watching him/her
                    parent.currentTask.ChangeState("watching");
                } 
                
                //continue moving toward desination
                //make it look the right way
                parent.parent.transform.LookAt(parent.targetDestination);

                //moveby... Fix later
                Vector3 pathUnitVector = Vector3.Normalize(parent.targetDestination - parent.parent.transform.position);

                Vector3 movementVector = Time.deltaTime * parent.movespeed * pathUnitVector;
                //Debug.Log(pathUnitVector);

                //instead of passing it, move to it and set state to stopped
                if(movementVector.magnitude >= (parent.parent.transform.position - parent.targetDestination).magnitude) {
                    parent.parent.transform.position = parent.targetDestination;
                    parent.currentTask.ChangeState("stopped");
                }
                else {
                    parent.parent.transform.position += movementVector;
                }
            }
        }
    }

    [System.Serializable]
    public class StateFriendly : IState {
        private HydriaAI parent;

        public StateFriendly(){
        }

        public void setParent(HydriaAI parent) {
            this.parent = parent;
        }

        void IState.UpdateState() {

        }
    }
}

