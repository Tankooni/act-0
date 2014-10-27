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
		bool j = animation.Play("FarCombatRun");
		animation["FarCombatAngleLeft"].layer = 1;
		animation["FarCombatAngleRight"].layer = 1;
		animation["FarCombatRun"].layer = 1;
		animation["FarNeutralAngleLeft"].layer = 1;
		animation["FarNeutralAngleRight"].layer = 1;
		animation["FarNeutralWalk"].layer = 1;
		animation["NearCombatAngleLeft"].layer = 1;
		animation["NearCombatAngleRight"].layer = 1;
		animation["NearCombatRun"].layer = 1;
		animation["NearNeutralAngleLeft"].layer = 1;
		animation["NearNeutralAngleRight"].layer = 1;
		animation["NearNeutralWalk"].layer = 1;
        animation["NearAttackStart"].layer = 12;
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
