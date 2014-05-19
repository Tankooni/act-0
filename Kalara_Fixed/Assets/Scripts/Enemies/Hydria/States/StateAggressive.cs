using System;
using UnityEngine;

namespace States.Monster
{
    [System.Serializable]
    public class StateAggressive : State
    {
        private HydriaAI parent;
        
        private StateMachine currentTask = new StateMachine();
        
        //circle state variables. Must be here to edit in unity and not cause errors
        public float circleRadius;
        public float runSpeed;
        public float avgRunTime;
        public float runDeviation;
        
        public StateAggressive()
        {
            StateCircle sCircle = new StateCircle();
            sCircle.setParent(this);
            
            currentTask.AddState("circle", sCircle);
            
            currentTask.ChangeState("circle");
        }
        
        public void setParent(HydriaAI parent)
        {
            this.parent = parent;
        }
        
        public override void UpdateState()
        {
            currentTask.Update();
        }
        
        [System.Serializable]
        public class StateCircle : State
        {
            private StateAggressive parent;
            
            public void setParent(StateAggressive parent)
            {
                this.parent = parent;
            }
            
            public override void UpdateState()
            {
                //if player gets to close, attack them immediately
                
                Vector3 toPlayer = parent.parent.transform.position - parent.parent.player.transform.position;
                
                //don't turn left, always circle to the right
                float turnRate = (parent.runSpeed / parent.circleRadius) * Time.deltaTime;
                
                Debug.Log(turnRate);
                
                float turnScaleFactor = toPlayer.magnitude / parent.circleRadius;
                turnRate *= turnScaleFactor;
                
                Debug.Log(turnRate);
                
                //find the unit normal to the vector pointing to the player and turn based on
                //the above turn rate. Then give it our movespeed and...well move there
                Vector3 movementVec = MathEx.rotateXZ(Vector3.Normalize(toPlayer), turnRate) * parent.runSpeed;
                
                Debug.Log(movementVec);
                
                parent.parent.transform.position += movementVec;
            }
        }
        
        public class StateLunge
        {
            
        }
        
        public class StateBackoff
        {
            
        }
        
        public override void EnterState() {}
        public override void LeaveState() {}
    }
}

