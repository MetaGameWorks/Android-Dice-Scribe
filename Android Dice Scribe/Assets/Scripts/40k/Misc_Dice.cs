using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Misc_Dice : MonoBehaviour
{
    // variables for the game object text input fields for the attack menu
    public GameObject numOfAttacksInput;
    public GameObject toHitInput;
    public GameObject toWoundInput;

    // text game objects for result fields
    // 0-5 will be for die results
    // 6 will be for total number of successes
    public GameObject[] hitDie;
    public GameObject[] woundDie;
    public GameObject[] MiscDie;
    public GameObject[] d3Die;
    public GameObject[] d6Die;

    // Menu game objects for disabling and enabling the menus
    public GameObject attackMenu;
    public GameObject hitResultMenu;
    public GameObject miscMenu;
    public GameObject miscResultMenu;

    // Extra menus in case the number of attacks are 1-to-1, D3, and D6
    public GameObject d3AttacksMenu;
    public GameObject d6AttacksMenu;
    public GameObject d3Toggle;
    public GameObject d6Toggle;

    // variable for variant number of attacks
    private enum VariantAttacks { Single, D3, D6 };
    private VariantAttacks vAttacks = VariantAttacks.Single;

    // variables to calculate
    private int numOfAttacks = 0;
    private int toHit = 0;
    private int numOfHits = 0;
    private int numOfWounds = 0;
    private int weaponStr = 0;
    private int targetToughness = 0;
    private int toWound = 0;
    private int numOfMisc = 0;
     
    // variables for die pools
    private int[] hitDiePool = new int[6];
    private int[] woundDiePool = new int[6];
    private int[] reRollDiePool = new int[6];
    private int[] variantAttacks = new int[6];
    private int[] miscDiePool = new int[6];

    // roll function
    public void RollDice(int[] diePool, GameObject[] textOutput)
    {
        Array.Clear(diePool, 0, 6);

    }
}
