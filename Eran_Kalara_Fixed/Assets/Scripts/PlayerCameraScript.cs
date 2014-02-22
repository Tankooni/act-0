using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour {

    public Transform FollowTarget;
    public float damping = 0.1f;

    private PlayerScript mFollowedPlayerScript;

    private float xRot = -1;
    private float otherTempThing;
	// Use this for initialization
	void Start () 
    {
        mFollowedPlayerScript = FollowTarget.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
    void Update()
    {
        xRot += Input.GetAxis("Mouse X") * damping;
    }
	void LateUpdate () 
    {
        Vector3 tmp = FollowTarget.position;
        tmp = tmp + (new Vector3(Mathf.Sin(xRot),0,Mathf.Cos(xRot)) * 5);
        tmp = tmp + (Vector3.up * 3);
        transform.position = tmp;
        transform.LookAt(FollowTarget);
	}
}
