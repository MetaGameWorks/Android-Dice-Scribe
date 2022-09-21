using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Menu_Controller : MonoBehaviour
{
    // variables for the attack input fields
    public GameObject attacksInput;
    public GameObject skillInput;
    public GameObject weaponStrInput;
    public GameObject targetToughnessInput;
    public GameObject miscDiceInput;

    // variables for the results fields
    public GameObject[] hitResult;
    public GameObject[] woundResult;
    public GameObject[] d3Result;
    public GameObject[] d6Result;
    public GameObject[] miscResult;

    // re-roll buttons to be disabled or enabled based on availability
    public GameObject reRoll1HitButton;
    public GameObject reRoll1sHitButton;
    public GameObject reRollAllHitsButton;
    public GameObject woundButton;
    public GameObject reRoll1WoundButton;
    public GameObject reRoll1sWoundButton;
    public GameObject reRollAllWoundsButton;
    public GameObject reRollToAttackD3Button;
    public GameObject reRollToAttackD6Button;
    public GameObject rollMiscButton;

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

    private int numOfAttacks = 0;
    private int toHit = 0;
    private int numOfHits = 0;
    private int numOfWounds = 0;
    private int weaponStr = 0;
    private int targetToughness = 0;
    private int toWound = 0;
    private int numOfMisc = 0;

    private int[] hitDiePool = new int[6];
    private int[] woundDiePool = new int[6];
    private int[] reRollDiePool = new int[6];
    private int[] variantAttacks = new int[6];
    private int[] miscDiePool = new int[6];
}
