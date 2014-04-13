using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour
{
	public float awayDistance = 5;
	public float upDistance = 3;
	public float smoothJazz = 10;
	public Vector3 targetPos;
	public Transform FollowTarget;
	private PlayerScript followedPlayerScript;

	// Use this for initialization
	void Start()
	{
		followedPlayerScript = FollowTarget.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	void LateUpdate()
	{
		targetPos = followedPlayerScript.transform.position + Vector3.up * upDistance - followedPlayerScript.transform.forward * awayDistance;
//		targetPos.position = followedPlayerScript.transform.position + Vector3.up * upDistance - followedPlayerScript.transform.forward * awayDistance;
		Debug.DrawRay(FollowTarget.position, Vector3.up * upDistance, Color.red);
		Debug.DrawRay(FollowTarget.position, -FollowTarget.forward * awayDistance, Color.blue);
		Debug.DrawLine(FollowTarget.position, targetPos, Color.magenta);

		transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothJazz);

		transform.LookAt(followedPlayerScript.transform.position);
	}

//	void OnDrawGizmos()
//	{
//		Gizmos.DrawCube(targetPos, Vector3.one/2);
//	}
}