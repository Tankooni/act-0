using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerController : MonoBehaviour
{
    public float gravity = 9.8f;
    public bool IsGrounded = false;

    void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

	void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update()
    {
	    
	}

    void FixedUpdate()
    {
        if(IsGrounded)
        {

        }
    }

    void Move(Vector3 moveTo)
    {

    }
}
