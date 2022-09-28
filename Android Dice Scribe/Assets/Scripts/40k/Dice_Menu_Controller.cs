using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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


    // Printing Functions
    /*(void PrintReRollHits()
    {
        int num = CountSuccesses(toHit, reRollDiePool);

        // outputs the wound roll results
        if (num > 0)
        {
            totalHits.GetComponent<Text>().text = "Hits: " + numOfHits + " + " + num;
        }
        
        for (int i = 0; i < 6; i++)
        {
            if (reRollDiePool[i] > 0)
            {
                hitResult[i].GetComponent<Text>().text = "" + hitDiePool[i] + " + " + reRollDiePool[i];
            }
            else
            {
                hitResult[i].GetComponent<Text>().text = "" + hitDiePool[i];
            }
         }
    }

    public void PrintReRollWounds()
    {
        int num = CountSuccesses(toWound, reRollDiePool);

        // outputs the wound roll results
        if (num > 0)
        {
            totalWounds.GetComponent<Text>().text = "Wounds: " + numOfWounds + " + " + num;
        }
        
        for(int i = 0; i < 6; i++)
        {
            if (reRollDiePool[i] > 0)
            {
                woundResult[i].GetComponent<Text>().text = "" + woundDiePool[i] + " + " + reRollDiePool[i];
            }
            else
            {
                woundResult[i].GetComponent<Text>().text = "" + woundDiePool[i];
            }
        }
    }

    public void PrintReRollToAttackD3()
    {
        for(int i = 0; i < 6; i++)
        {

        }

        if (reRollDiePool[0] > 0)
        {
            d3Die1.GetComponent<Text>().text = "" + variantAttacks[0] + " + " + reRollDiePool[0];
        }
        else
        {
            d3Die1.GetComponent<Text>().text = "" + variantAttacks[0];
        }
        if (reRollDiePool[1] > 0)
        {
            d3Die2.GetComponent<Text>().text = "" + variantAttacks[1] + " + " + reRollDiePool[1];
        }
        else
        {
            d3Die2.GetComponent<Text>().text = "" + variantAttacks[1];
        }
        if (reRollDiePool[2] > 0)
        {
            d3Die3.GetComponent<Text>().text = "" + variantAttacks[2] + " + " + reRollDiePool[2];
        }
        else
        {
            d3Die3.GetComponent<Text>().text = "" + variantAttacks[2];
        }
        if (reRollDiePool[3] > 0)
        {
            d3Die4.GetComponent<Text>().text = "" + variantAttacks[3] + " + " + reRollDiePool[3];
        }
        else
        {
            d3Die4.GetComponent<Text>().text = "" + variantAttacks[3];
        }
        if (reRollDiePool[4] > 0)
        {
            d3Die5.GetComponent<Text>().text = "" + variantAttacks[4] + " + " + reRollDiePool[4];
        }
        else
        {
            d3Die5.GetComponent<Text>().text = "" + variantAttacks[4];
        }
        if (reRollDiePool[5] > 0)
        {
            d3Die6.GetComponent<Text>().text = "" + variantAttacks[5] + " + " + reRollDiePool[5];
        }
        else
        {
            d3Die6.GetComponent<Text>().text = "" + variantAttacks[5];
        }
    }

    public void PrintReRollToAttackD6()
    {
        if (reRollDiePool[0] > 0)
        {
            d6Die1.GetComponent<Text>().text = "" + variantAttacks[0] + " + " + reRollDiePool[0];
        }
        else
        {
            d6Die1.GetComponent<Text>().text = "" + variantAttacks[0];
        }
        if (reRollDiePool[1] > 0)
        {
            d6Die2.GetComponent<Text>().text = "" + variantAttacks[1] + " + " + reRollDiePool[1];
        }
        else
        {
            d6Die2.GetComponent<Text>().text = "" + variantAttacks[1];
        }
        if (reRollDiePool[2] > 0)
        {
            d6Die3.GetComponent<Text>().text = "" + variantAttacks[2] + " + " + reRollDiePool[2];
        }
        else
        {
            d6Die3.GetComponent<Text>().text = "" + variantAttacks[2];
        }
        if (reRollDiePool[3] > 0)
        {
            d6Die4.GetComponent<Text>().text = "" + variantAttacks[3] + " + " + reRollDiePool[3];
        }
        else
        {
            d6Die4.GetComponent<Text>().text = "" + variantAttacks[3];
        }
        if (reRollDiePool[4] > 0)
        {
            d6Die5.GetComponent<Text>().text = "" + variantAttacks[4] + " + " + reRollDiePool[4];
        }
        else
        {
            d6Die5.GetComponent<Text>().text = "" + variantAttacks[4];
        }
        if (reRollDiePool[5] > 0)
        {
            d6Die6.GetComponent<Text>().text = "" + variantAttacks[5] + " + " + reRollDiePool[5];
        }
        else
        {
            d6Die6.GetComponent<Text>().text = "" + variantAttacks[5];
        }
    }*/
}