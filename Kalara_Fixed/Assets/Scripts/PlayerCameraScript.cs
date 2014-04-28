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
        var targetPos2D = PositionAlongLine(followedPlayerScript.transform.position, transform.position, awayDistance);
        targetPos =  new Vector3(targetPos2D.x, followedPlayerScript.transform.position.y + awayDistance, targetPos2D.y);
        //Debug.Log(targetPos2D);
        //targetPos = followedPlayerScript.transform.position + followedPlayerScript.transform.up * upDistance - followedPlayerScript.transform.forward * awayDistance;
//		targetPos.position = followedPlayerScript.transform.position + Vector3.up * upDistance - followedPlayerScript.transform.forward * awayDistance;
//		Debug.DrawRay(FollowTarget.position, Vector3.up * upDistance, Color.red);
//		Debug.DrawRay(FollowTarget.position, -FollowTarget.forward * awayDistance, Color.blue);
		//Debug.DrawLine(FollowTarget.position, targetPos, Color.magenta);

        transform.position = /*Vector3.Lerp(transform.position, */targetPos/*, Time.deltaTime * smoothJazz)*/;

		transform.LookAt(followedPlayerScript.transform.position, followedPlayerScript.transform.up);
	}

    Vector2 PositionAlongLine(Vector3 newLookAtPos, Vector3 oldPos, float distance)
    {
        return PositionAlongLine(new Vector2(newLookAtPos.x, newLookAtPos.z), new Vector2(oldPos.x, oldPos.z), distance);
    }
    Vector2 PositionAlongLine(Vector2 newPlayerPos, Vector2 oldCamPos, float camDist)
    {
//        Vector2 line = new Vector2(newLookAtPos.x - oldPos.x, newLookAtPos.z - oldPos.z);
//        Vector2 normalizedLine = line.normalized;
//        Vector2 newVec = new Vector2(oldPos.x,oldPos.z) + normalizedLine * (line.magnitude + distance);
//        Debug.Log(newVec.magnitude);

        var newVec = oldCamPos - newPlayerPos;
        newVec.Normalize();
        newVec *= camDist;
        newVec += newPlayerPos;
        return newVec;
//        var newVec = newLookAtPos - distance/Vector3.Distance(oldPos, newLookAtPos) * (oldPos - newLookAtPos);
//        return newVec;

//        Vector2 newCameraPos = new Vector2();
//        
//        float slope = (oldCamPos.y - newPlayerPos.y) / (oldCamPos.x - newPlayerPos.x);
//        newCameraPos.x = Mathf.Sqrt(camDist / (1 + slope*slope));
//        newCameraPos.y = newCameraPos.x / slope;
//        
//        newCameraPos.x = newCameraPos.x + newPlayerPos.x;
//        newCameraPos.y = newCameraPos.y + newPlayerPos.y;
//
//        Debug.Log(newCameraPos);
//        return newCameraPos;
    }

//	void OnDrawGizmos()
//	{
//		Gizmos.DrawCube(targetPos, Vector3.one/2);
//	}
}