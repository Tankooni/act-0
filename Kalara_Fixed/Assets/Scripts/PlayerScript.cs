using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{

    #region Variables

    #region Public

    public int Level;
    public int Shield;
    public BaseAttributes BASE_STATS, GROWTH_STATS;
    public ePrimaryStat PrimaryStat;

    #endregion

    #region Private

    public BaseAttributes ACTUAL_STATS;
    private int Health, Energy, Ego;

    #region Physical Stats

    private int PhysicalModifier;
    private int Power, Agility, Fortitude;

    #endregion

    #region Intellectual Stats

    private int IntellectualModifier;
    private int Mental, Emotional, Social;

    #endregion

    #region Residual Stats

    private int ResidualModifier;
    private int Soul, Invocation, Aura;

    #endregion

    private float curJumpSpeed;
    private Vector3 mLookPos;

    private CharacterController cc;

    #endregion

    #endregion

    #region Initialization
    // Use this for initialization
	void Start () 
    {
        cc = this.GetComponent<CharacterController>();
        ApplyLevelToStats();
	}

    void ApplyLevelToStats()
    {
        PhysicalModifier = PrimaryStat == ePrimaryStat.Physical ? Level : (int)(0.5f * Level);
        IntellectualModifier = PrimaryStat == ePrimaryStat.Intellectual ? Level : (int)(0.5f * Level);
        ResidualModifier = PrimaryStat == ePrimaryStat.Residual ? Level : (int)(0.5f * Level);

        ACTUAL_STATS.Strength = BASE_STATS.Strength + (Level * GROWTH_STATS.Strength);
        ACTUAL_STATS.Muscle = BASE_STATS.Muscle + (Level * GROWTH_STATS.Muscle);
        ACTUAL_STATS.Flexibility = BASE_STATS.Flexibility + (Level * GROWTH_STATS.Flexibility);
        ACTUAL_STATS.Speed = BASE_STATS.Speed + (Level * GROWTH_STATS.Speed);
        ACTUAL_STATS.Toughness = BASE_STATS.Toughness + (Level * GROWTH_STATS.Toughness);
        ACTUAL_STATS.Endurance = BASE_STATS.Endurance + (Level * GROWTH_STATS.Endurance);

        ACTUAL_STATS.Intelligence = BASE_STATS.Intelligence + (Level * GROWTH_STATS.Intelligence);
        ACTUAL_STATS.Intuition = BASE_STATS.Intuition + (Level * GROWTH_STATS.Intuition);
        ACTUAL_STATS.Stability = BASE_STATS.Stability + (Level * GROWTH_STATS.Stability);
        ACTUAL_STATS.Discipline = BASE_STATS.Discipline + (Level * GROWTH_STATS.Discipline);
        ACTUAL_STATS.Extroversion = BASE_STATS.Extroversion + (Level * GROWTH_STATS.Extroversion);
        ACTUAL_STATS.Tact = BASE_STATS.Tact + (Level * GROWTH_STATS.Tact);

        ACTUAL_STATS.Willpower = BASE_STATS.Willpower + (Level * GROWTH_STATS.Willpower);
        ACTUAL_STATS.Honor = BASE_STATS.Honor + (Level * GROWTH_STATS.Honor);
        ACTUAL_STATS.Ability = BASE_STATS.Ability + (Level * GROWTH_STATS.Ability);
        ACTUAL_STATS.Control = BASE_STATS.Control + (Level * GROWTH_STATS.Control);
        ACTUAL_STATS.Astral = BASE_STATS.Astral + (Level * GROWTH_STATS.Astral);
        ACTUAL_STATS.Sight = BASE_STATS.Sight + (Level * GROWTH_STATS.Sight);

        Power = ACTUAL_STATS.Strength + ACTUAL_STATS.Muscle;
        Agility = ACTUAL_STATS.Flexibility + ACTUAL_STATS.Speed;
        Fortitude = ACTUAL_STATS.Toughness + ACTUAL_STATS.Endurance;

        Mental = ACTUAL_STATS.Intelligence + ACTUAL_STATS.Intuition;
        Emotional = ACTUAL_STATS.Stability + ACTUAL_STATS.Discipline;
        Social = ACTUAL_STATS.Extroversion + ACTUAL_STATS.Tact;

        Soul = ACTUAL_STATS.Willpower + ACTUAL_STATS.Honor;
        Invocation = ACTUAL_STATS.Ability + ACTUAL_STATS.Control;
        Aura = ACTUAL_STATS.Astral + ACTUAL_STATS.Sight;

        Health = 100 + (Power + Agility + Fortitude + Level + PhysicalModifier) * (ACTUAL_STATS.Muscle + Fortitude);
        Energy = 100 + (Mental + Emotional + Social + Level + IntellectualModifier) * (ACTUAL_STATS.Discipline + ACTUAL_STATS.Endurance + ACTUAL_STATS.Willpower);
        Ego = 100 + (Soul + Invocation + Aura + Level + ResidualModifier) * (ACTUAL_STATS.Willpower + ACTUAL_STATS.Honor + ACTUAL_STATS.Control);
    }
    #endregion

    #region Jump

    void CheckJump()
    {

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            curJumpSpeed = ACTUAL_STATS.Strength;
        }
    }

    void ApplyGravity()
    {
        curJumpSpeed += Physics.gravity.y * Time.deltaTime;
    }

    #endregion

    #region Movement

    void ApplyMovement(Vector3 direction)
    {
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }



        cc.Move((direction.z * Camera.main.transform.forward) * ACTUAL_STATS.Speed * Time.deltaTime);
        cc.Move((direction.x * Camera.main.transform.right) * ACTUAL_STATS.Speed * Time.deltaTime);
    }

    void ApplyJump()
    {
        cc.Move(Vector3.up * curJumpSpeed * Time.fixedDeltaTime);
    }

    #endregion

    #region Look Direction

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

    #endregion

    #region Update

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

    #endregion
}
