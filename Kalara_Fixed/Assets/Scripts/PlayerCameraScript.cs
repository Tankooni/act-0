using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour
{
    private float cameraDistance = 6;
	private float upDistance = 1;
    private float smoothJazz = 10;
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
        Vector3 targetPos;

        //center the camera if the button for it is pressed
        if(Input.GetButton("CameraFix")) {
            targetPos = -followedPlayerScript.transform.forward;
            targetPos.Normalize();
        } else {
            //find the unit vector in the direction the camera should be
            targetPos = this.transform.position - followedPlayerScript.transform.position;
            targetPos.Normalize();

            //rotate the vector based on user's camera input (ei. right stick)
            float horzCameraInput = Input.GetAxis("RightHorizontal");
            if(Mathf.Abs(horzCameraInput) > .3) {
                targetPos = MathEx.rotateXZ(targetPos, -horzCameraInput * 30);
            }
        }

        targetPos *= cameraDistance;

        //reference the new camera position from the player and camera height
        targetPos += followedPlayerScript.transform.position;
        targetPos.y = followedPlayerScript.transform.position.y + upDistance;

        //rotate the camera based on the movement of the right stick
        //if(Input.GetButton("CameraFix" &)) {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothJazz);
       
		transform.LookAt(followedPlayerScript.transform.position, followedPlayerScript.transform.up);
	}
}