using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(StatSystem))]
public class PlayerScript : MonoBehaviour
{
	private StatSystem stats;

    private float curJumpSpeed;
    private Vector3 mLookPos;

    private CharacterController cc;

    [SerializeField]
    private Animator animator;

    private float directionDampTime = 0.25f;

    // Use this for initialization
	void Start () 
    {
        stats = this.GetComponent<StatSystem>();
        cc = this.GetComponent<CharacterController>();
        stats.UpdateStats();

        animator = GetComponentsInChildren<Animator>()[0];
        if(animator.layerCount >= 2)
            animator.SetLayerWeight(1, 1);
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

        if(animator)
        {
            animator.SetFloat("Speed", direction.sqrMagnitude);
            animator.SetFloat("Direction", direction.x, directionDampTime, Time.deltaTime);
        }
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
