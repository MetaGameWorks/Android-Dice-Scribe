using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army_Editor_Script : MonoBehaviour
{
    
}

public class Army
{
    string armyName;
    int numOfUnits;
    int pointLimit;
    int powerLevelLimit;
    Unit[] list;

    //public void AddUnit();
    //public void RemoveUnit();
}

public class Unit
{
    string unitName;
    enum UnitType
    {
        HQ,
        Trooper,
        Elite,
        Heavy,
        FastAttack,
        Flying
    }
    int numOfWeaponOptions;

    //public void AtackRoll();
    //public void DefenseRoll();
}

public class WeaponProfile
{
    string profileName;
    int numOfWeaponsInThisProfile;
    int weaponStr;
    int numOfShots;
    enum NumOfShotsType
    {
        D1,
        D3,
        D6
    }
    int damage;
    enum DamageType
    {
        D1,
        D3,
        D6
    }
    // need to add a section for special abilities

    //public void AddWeaponProfile();
    //public void RemoveWeaponProfile();
    //public void SetnumOfWeaponsInThisProfile();
}
