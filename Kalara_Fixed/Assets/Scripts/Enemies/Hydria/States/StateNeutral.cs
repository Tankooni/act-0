using System;
using UnityEngine;

namespace States.Monster
{
    [System.Serializable]
    public class StateNeutral : State
    {
        private HydriaAI parent;
        
        private StateMachine currentTask = new StateMachine();
        
        public float watchDistance;
        public float aggressiveDistance;
        public float wanderRadius;
        public float movespeed;
        
        private Vector3 startingPosition;
        
        private Vector3 targetDestination;
        private float distanceToTarget;
        
        public StateNeutral()
        {
            currentTask.AddState("stopped", new StateStopped(this));
            currentTask.AddState("walking", new StateWalking(this));
            currentTask.AddState("watching", new StateWatching(this));
            
            currentTask.ChangeState("stopped");
        }
        
        public void setParent(HydriaAI parent)
        {
            this.parent = parent;
            startingPosition = parent.transform.position;
        }
        
        public override void UpdateState()
        {
            //Update
            currentTask.Update();
            
            //check if player is nearby
            distanceToTarget = Vector3.Distance(parent.player.transform.position, parent.transform.position);
            
            if(distanceToTarget < aggressiveDistance)
            {
                parent.baseState.ChangeState("aggressive");
                Debug.Log("AGGRESSIVE!");
            }
        }
        
        [System.Serializable]
        public class StateStopped : StateNeutral {
            StateNeutral parent;
            
            public StateStopped (StateNeutral parent)
            {
                this.parent = parent;
            }
            
            public override void UpdateState()
            {
                //find a new place to walk to
                Vector2 rand2dVec = (MathEx.GenUnitVec2() * parent.wanderRadius);
                //Debug.Log(rand2dVec);
                parent.targetDestination = parent.startingPosition + new Vector3(rand2dVec.x, 0 ,rand2dVec.y);
                parent.currentTask.ChangeState("walking");
            }
            
            public override void EnterState() {}
            public override void LeaveState() {}
        }
        [System.Serializable]
        public class StateWatching : State {
            StateNeutral parent;
            
            public StateWatching (StateNeutral parent) {
                this.parent = parent;
            }
            
            public override void UpdateState() {
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
            
            public override void EnterState() {}
            public override void LeaveState() {}
        }
        [System.Serializable]
        public class StateWalking : State {
            StateNeutral parent;
            
            public StateWalking (StateNeutral parent) {
                this.parent = parent;
            }
            
            public override void UpdateState() {
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
            public override void EnterState() {}
            public override void LeaveState() {}
        }
        
        public override void EnterState() {}
        public override void LeaveState() {}
    }
}

