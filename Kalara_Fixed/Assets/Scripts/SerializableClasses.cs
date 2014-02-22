using UnityEngine;
using System.Collections;

#region Classes

[System.Serializable]
public class Gauges
{
    int Shield;
    int Health;
    int Ego;
    int Desperation;
}

[System.Serializable]
public class BaseAttributes
{
    //Physcial Stats
    public int Strength, Muscle, Flexibility, Speed, Toughness, Endurance;
    
    //Intellectual Stats
    public int Intelligence, Intuition, Stability, Discipline, Extroversion, Tact;
    
    //Residual Stats
    public int Willpower, Honor, Ability, Control, Astral, Sight;
};


[System.Serializable]
public class AdvancedAttributes
{
    //Pysical Attributes
    public int pDmg, pDef, rDmg, rDef, guile, bulwark;

    //Intellectual Attributes
    public int alacrity, acumen, prowess, voice, cunning, daunt;

    //Residual Attributes
    public int iDmg, iDef, eDmg, eDef, PRE, sensory;
};

#endregion

#region Enums

public enum ePrimaryStat
{
    Physical,
    Intellectual,
    Residual
}

public enum eItemTypes
{
    Armor,
    Weapon,
    Accessory
}

public enum eArmorTypes
{
    Heavy,
    Medium,
    Light,
    Back,
    Chest,
    Shoulder,
    Armware,
    Magic,
    Head,
    Legs,
    Feet
}

public enum eWeaponTypes
{
    Slashing,
    Piercing,
    Bludgeoning,
    Spear,
    Sword,
    Mace,
    Gun,
    Bow,
    Crossbow,
    Large,Medium,
    Small,
    Thrown,
    Instrument,
    Magic
}

public enum eAccessoryTypes
{
    Ring,
    Earring,
    Necklace,
    Eyeware,
    Bracelet
}

#endregion