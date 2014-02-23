using UnityEngine;
using System.Collections;

public class StatSystem : MonoBehaviour
{
	public int Level;
	public int Shield;
	public BaseAttributes BASE_STATS, GROWTH_STATS;
	public ePrimaryStat PrimaryStat;

	public BaseAttributes ACTUAL_STATS;
	private int Health, Energy, Ego;

    public int PhysicalTotal;
	private int PhysicalModifier;
	private int Power, Agility, Fortitude;

    public int IntellectualTotal;
	private int IntellectualModifier;
	private int Mental, Emotional, Social;

    public int ResidualTotal;
	private int ResidualModifier;
	private int Soul, Invocation, Aura;

    public AdvancedAttributes HiddenAttributes {
        get { return _hiddenAttributes;}
    }
    private AdvancedAttributes _hiddenAttributes;

	// Use this for initialization
	void Start () 
	{
		UpdateStats();
	}
	
	public void UpdateStats()
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
        PhysicalTotal = Power + Agility + Fortitude;
		
		Mental = ACTUAL_STATS.Intelligence + ACTUAL_STATS.Intuition;
		Emotional = ACTUAL_STATS.Stability + ACTUAL_STATS.Discipline;
		Social = ACTUAL_STATS.Extroversion + ACTUAL_STATS.Tact;
        IntellectualTotal = Mental + Emotional + Social;
		
		Soul = ACTUAL_STATS.Willpower + ACTUAL_STATS.Honor;
		Invocation = ACTUAL_STATS.Ability + ACTUAL_STATS.Control;
		Aura = ACTUAL_STATS.Astral + ACTUAL_STATS.Sight;
        ResidualTotal = Soul + Invocation + Aura;
		
		Health = 100 + (Power + Agility + Fortitude + Level + PhysicalModifier) * (ACTUAL_STATS.Muscle + Fortitude);
		Energy = 100 + (Mental + Emotional + Social + Level + IntellectualModifier) * (ACTUAL_STATS.Discipline + ACTUAL_STATS.Endurance + ACTUAL_STATS.Willpower);
		Ego = 100 + (Soul + Invocation + Aura + Level + ResidualModifier) * (ACTUAL_STATS.Willpower + ACTUAL_STATS.Honor + ACTUAL_STATS.Control);

        //hidden stats
        _hiddenAttributes.acumen = ACTUAL_STATS.Stability + ACTUAL_STATS.Discipline + ACTUAL_STATS.Tact + ACTUAL_STATS.Willpower;
        _hiddenAttributes.alacrity = (ACTUAL_STATS.Astral + ACTUAL_STATS.Tact) 
                                     * ACTUAL_STATS.Intuition 
                                     * (ACTUAL_STATS.Stability + ACTUAL_STATS.Discipline) / 10;
        _hiddenAttributes.bulwark = ACTUAL_STATS.Strength + ACTUAL_STATS.Muscle + ACTUAL_STATS.Toughness + ACTUAL_STATS.Endurance + ACTUAL_STATS.Willpower;
        _hiddenAttributes.cunning = (ACTUAL_STATS.Tact * ACTUAL_STATS.Intelligence)
                                     + (ACTUAL_STATS.Astral + ACTUAL_STATS.Sight - ACTUAL_STATS.Honor);
        _hiddenAttributes.daunt = ACTUAL_STATS.Willpower + ACTUAL_STATS.Discipline + ACTUAL_STATS.Intelligence + ACTUAL_STATS.Muscle;
        _hiddenAttributes.eDef = ACTUAL_STATS.Endurance + ACTUAL_STATS.Willpower + ACTUAL_STATS.Control + ACTUAL_STATS.Sight;
        _hiddenAttributes.eDmg = ACTUAL_STATS.Intelligence * ACTUAL_STATS.Willpower;
        _hiddenAttributes.guile = ACTUAL_STATS.Muscle + ACTUAL_STATS.Flexibility + ACTUAL_STATS.Speed + ACTUAL_STATS.Endurance + ACTUAL_STATS.Willpower;
        _hiddenAttributes.iDef = ACTUAL_STATS.Toughness + ACTUAL_STATS.Endurance + ACTUAL_STATS.Willpower + ACTUAL_STATS.Control;
        _hiddenAttributes.iDmg = (ACTUAL_STATS.Control * ACTUAL_STATS.Tact) + ACTUAL_STATS.Ability;
        _hiddenAttributes.pDef = ACTUAL_STATS.Strength + ACTUAL_STATS.Muscle + ACTUAL_STATS.Toughness + ACTUAL_STATS.Endurance;
        _hiddenAttributes.pDmg = ACTUAL_STATS.Strength * ACTUAL_STATS.Speed;
        _hiddenAttributes.PRE = (PhysicalTotal + IntellectualTotal + ResidualTotal) * ACTUAL_STATS.Ability;
        _hiddenAttributes.prowess = (ACTUAL_STATS.Discipline + ACTUAL_STATS.Sight)
                                    * ACTUAL_STATS.Intelligence
                                    * (ACTUAL_STATS.Discipline + ACTUAL_STATS.Stability) / 10;
        _hiddenAttributes.rDef = ACTUAL_STATS.Willpower + ACTUAL_STATS.Honor + ACTUAL_STATS.Astral + ACTUAL_STATS.Sight;
        _hiddenAttributes.rDmg = ACTUAL_STATS.Willpower * ACTUAL_STATS.Honor;
        _hiddenAttributes.sensory = ACTUAL_STATS.Intelligence + ACTUAL_STATS.Intuition + ACTUAL_STATS.Astral + ACTUAL_STATS.Sight;
        _hiddenAttributes.voice = ACTUAL_STATS.Intelligence + ACTUAL_STATS.Intuition + ACTUAL_STATS.Extroversion + ACTUAL_STATS.Tact;
	}
}