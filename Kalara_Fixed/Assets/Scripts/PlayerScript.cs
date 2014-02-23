using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
	public StatSystem stats;

    private float curJumpSpeed;
    private Vector3 mLookPos;

    private CharacterController cc;

    // Use this for initialization
	void Start () 
    {
        cc = this.GetComponent<CharacterController>();
        stats.UpdateStats();
	}

    void CheckJump()
    {

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            curJumpSpeed = stats.ACTUAL_STATS.Strength;
        }
    }

    void ApplyGravity()
    {
        curJumpSpeed += Physics.gravity.y * Time.deltaTime;
    }

    void ApplyMovement(Vector3 direction)
    {
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }



        cc.Move((direction.z * Camera.main.transform.forward) * stats.ACTUAL_STATS.Speed * Time.deltaTime);
        cc.Move((direction.x * Camera.main.transform.right) * stats.ACTUAL_STATS.Speed * Time.deltaTime);
    }

    void ApplyJump()
    {
        cc.Move(Vector3.up * curJumpSpeed * Time.fixedDeltaTime);
    }

    void ApplyLookDirection( Vector3 direction )
    {
        if (direction.sqrMagnitude > 0)
        {
            float tmp = transform.position.y;
            mLookPos = transform.position + (direction.z * Camera.main.transform.forward) + (direction.x * Camera.main.transform.right);
            mLookPos.Set(mLookPos.x, tmp, mLookPos.z);

            Quaternion rotation = Quaternion.LookRotation(mLookPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6.0f);
        }
    }

    //All input functions should be in Update, dont' be dumb and put them in fixed update
    void Update()
    {
        if (!Screen.lockCursor)
        {
            Screen.lockCursor = true;
        }
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CheckJump();
        ApplyMovement(direction);
        ApplyLookDirection(direction);
    }

    void FixedUpdate () 
    {
        ApplyJump();
        ApplyGravity();

    }

    void LateUpdate()
    {
        cc.SimpleMove(Vector3.zero);
    }
}
