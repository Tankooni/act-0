using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(StatSystem))]
public class PlayerScript : MonoBehaviour
{
	private StatSystem stats;

    [SerializeField]
    private float curVerticalMove;
    [SerializeField]
    private Vector3 mLookPos;

    private CharacterController cc;

    [SerializeField]
    private float directionDampTime = 0.25f;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private float turnSpeed = 0.3f;

    /// <summary>
    /// If the player is jumping, and grounded, adds the jump strength to the player
    /// </summary>
    void CheckJump()
    {
        if (Input.GetButtonDown("Jump") /*&& cc.isGrounded*/)
        {
            transform.Rotate(new Vector3(0,0,180));
            //curVerticalMove += stats.ACTUAL_STATS.Strength;
        }

    }

    /// <summary>
    /// Applies gravity to the jump speed to be calculated on the player at a later time, but only if the player is not g
    /// </summary>
    void ApplyGravity()
    {
        if(!cc.isGrounded)
        {
            if(curVerticalMove > -60)
                curVerticalMove += Physics.gravity.y * Time.deltaTime;
            Debug.Log("NotGrounded");
        }
        else
        {
            curVerticalMove = 0;
            Debug.Log("Grounded");
        }

    }

    /// <summary>
    /// Applies movement to the player in the horizontal plane
    /// </summary>
    /// <param name="direction">Direction.</param>
    void ApplyHorizontalMovement(Vector3 direction)
    {
        cc.Move(direction * stats.ACTUAL_STATS.Speed * Time.deltaTime);
    }

    void ApplyVerticalMovement()
    {
        cc.Move(transform.up * curVerticalMove * Time.fixedDeltaTime);
    }

    void ApplyLookDirection( Vector3 direction )
    {
        transform.forward = direction;
    }

    #region UnityMethods

    // Use this for initialization
    void Start () 
    {
        stats = this.GetComponent<StatSystem>();
        cc = this.GetComponent<CharacterController>();
        stats.UpdateStats();
        
        animator = GetComponentsInChildren<Animator>()[0];
        if(animator.layerCount >= 2)
            animator.SetLayerWeight(1, 1);

        playerCamera = Camera.main;
    }

    //All input functions should be in Update, dont' be dumb and put them in fixed update
    void Update()
    {
        //find new direction for the player
        Vector3 rawInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = playerCamera.transform.rotation * rawInput;
        direction.y = 0;
        Debug.Log(rawInput);

        //update the rotation and position
        if(rawInput.x != 0 || rawInput.z != 0) {
            ApplyLookDirection(direction);
            ApplyHorizontalMovement(direction);
        }

        if(Input.GetButtonDown("Fire1"))
            animator.Play("CombatAttackStart");
        CheckJump();

        ApplyGravity();
        ApplyVerticalMovement();

        if(animator)
        {
            animator.SetFloat("Speed", direction.sqrMagnitude);
            animator.SetFloat("Direction", direction.x, directionDampTime, Time.deltaTime);
        }
    }
    #endregion UnityMethods
}
