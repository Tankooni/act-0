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
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
            //Debug.Log("Normalized dir");
        }
        //Debug.Log("dir1: " + direction);

        //cc.transform.rotation = Quaternion.FromToRotation(transform.forward, Vector3.Scale(playerCamera.transform.right, direction));
        cc.Move((direction.z * playerCamera.transform.forward) * stats.ACTUAL_STATS.Speed * Time.deltaTime);

        //cc.Move((direction.x * playerCamera.transform.right) * stats.ACTUAL_STATS.Speed * Time.deltaTime);
    }

    void ApplyVerticalMovement()
    {
        cc.Move(transform.up * curVerticalMove * Time.fixedDeltaTime);
    }

    void ApplyLookDirection( Vector3 direction )
    {
        if (direction.sqrMagnitude > 0)
        {
//            float tmp = transform.position.y;
//            mLookPos = transform.position + (direction.z * transform.forward) + (direction.x * transform.right);
//            mLookPos.Set(mLookPos.x, tmp, mLookPos.z);

            //Quaternion rotation = Quaternion.LookRotation(mLookPos - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6.0f);

            
        }
    }

    public void StickToWorldspace(Transform root, Transform camera, ref float directionOut)
    {
        Vector3 rootDirection = root.forward;

        Vector3 cameraDirection = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraRight.y = cameraDirection.y = 0;

        Quaternion playerRot = Quaternion.FromToRotation(Vector3.Scale(cameraRight, direction), transform.forward);
        Vector3 newPlayerLook = playerRot * Vector3.one;
        //Debug.Log(newPlayerLook);

        newPlayerLook = Vector3.Cross(Vector3.Scale(cameraRight, direction), Vector3.Scale(cameraDirection, direction));

        Debug.DrawRay(new Vector3(root.position.x, root.position.y, root.position.z) + root.up * 2f, cameraRight, Color.red);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y, root.position.z) + root.up * 2f, Vector3.Scale(cameraRight, direction), Color.blue);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y, root.position.z) + root.up * 2f, Vector3.Scale(cameraDirection, direction), Color.green);

        //Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y, root.position.z) + root.up * 2f, Vector3.Scale(Vector3.Scale(cameraRight, direction), Vector3.Scale(cameraDirection, direction)), Color.yellow);

//        Quaternion referentialShift = Quaternion.FromToRotation(transform.up, cameraDirection);
//        //Quaternion referentialShift = Quaternion.LookRotation(transform.forward, transform.up);
//        Debug.Log(referentialShift);
//        Vector3 moveDirection = referentialShift * direction;
//        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);
//
//        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), cameraDirection, Color.white);
//        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
//        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), axisSign, Color.red);
//        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
//        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), direction, Color.blue);
//
//
//
//        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
//
//        angleRootToMove /= 180f;
//        directionOut = angleRootToMove * turnSpeed;


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
//        if (!Screen.lockCursor)
//        {
//            Screen.lockCursor = true;
//        }
        if(Input.GetButtonDown("Fire1"))
            animator.Play("CombatAttackStart");
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CheckJump();
        var angle = 0.0f;

        ApplyHorizontalMovement(direction);
        ApplyLookDirection(direction);
        ApplyGravity();
        ApplyVerticalMovement();

        StickToWorldspace(transform, playerCamera.transform, ref angle);

        if(animator)
        {
            animator.SetFloat("Speed", direction.sqrMagnitude);
            animator.SetFloat("Direction", direction.x, directionDampTime, Time.deltaTime);
        }
    }

    void FixedUpdate() 
    {

    }

    void LateUpdate()
    {
        //cc.SimpleMove(Vector3.zero);
    }
    #endregion UnityMethods
}
