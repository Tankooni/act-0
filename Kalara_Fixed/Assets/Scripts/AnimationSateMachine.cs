using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationSateMachine : MonoBehaviour
{
	public Animator AnimThings;
	public Animation[] animations;
	float times = 0;
	// Use this for initialization
	void Start()
	{
		//animation.Play("CombatIdle");
		AnimThings = GetComponent<Animator>();
		bool j = GetComponent<Animation>().Play("FarCombatRun");
		GetComponent<Animation>()["FarCombatAngleLeft"].layer = 1;
		GetComponent<Animation>()["FarCombatAngleRight"].layer = 1;
		GetComponent<Animation>()["FarCombatRun"].layer = 1;
		GetComponent<Animation>()["FarNeutralAngleLeft"].layer = 1;
		GetComponent<Animation>()["FarNeutralAngleRight"].layer = 1;
		GetComponent<Animation>()["FarNeutralWalk"].layer = 1;
		GetComponent<Animation>()["NearCombatAngleLeft"].layer = 1;
		GetComponent<Animation>()["NearCombatAngleRight"].layer = 1;
		GetComponent<Animation>()["NearCombatRun"].layer = 1;
		GetComponent<Animation>()["NearNeutralAngleLeft"].layer = 1;
		GetComponent<Animation>()["NearNeutralAngleRight"].layer = 1;
		GetComponent<Animation>()["NearNeutralWalk"].layer = 1;
        GetComponent<Animation>()["NearAttackStart"].layer = 12;
		print(j);
	}
	
	// Update is called once per frame
	void Update()
	{
		if ((times += Time.deltaTime) > 2)
		{
			
		}

	}
}
