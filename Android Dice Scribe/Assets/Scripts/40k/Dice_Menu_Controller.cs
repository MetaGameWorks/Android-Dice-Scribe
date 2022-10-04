using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Dice_Menu_Controller : MonoBehaviour
{
    // variables for the attack input fields
    public GameObject numOfAttacksInput;
    public GameObject toHitInput;
    public GameObject toWoundInput;
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
    private int toWound = 0;
    private int numOfHits = 0;
    private int numOfWounds = 0;
    private int numOfMisc = 0;

    private int[] hitDiePool = new int[6];
    private int[] woundDiePool = new int[6];
    private int[] reRollDiePool = new int[6];
    private int[] variantAttacks = new int[6];
    private int[] miscDiePool = new int[6];

    // takes in an dice pool array and a num and rolls a number of d6 dice equal to num
    // this is to be used in other functions
    public void RollDice(int[] dicePool, int num)
	{
        // clears the array first
        Array.Clear(dicePool, 0, dicePool.Length);

        // rolls a random number 0-5 a number of times equal to num
        for(int i = num; i > 0; i--)
		{
            dicePool[UnityEngine.Random.Range(0, 6)]++;
		}
	}

    // takes in a dice pool array and an array of text GameObjects and outputs the dice results to the array of text GameObjects
    // this is to be used in other functions
    public void PrintResults(int[] dicePool, GameObject[] resultText)
	{
        for(int i = 0; i < 6; i++)
		{
            resultText[i].GetComponent<Text>().text = "" + dicePool[i];
        }
	}

    // clear text GameObject
    // this is to be used in other functions
    public void ClearText(GameObject[] resultText)
	{
        for(int i = 0; i < 7; i++)
		{
            resultText[i].GetComponent<Text>().text = "0";
        }
	}

    // change active menus
    // this is to be used in other functions
    public void ChangeMenus(GameObject currentMenu, GameObject nextMenu)
	{
        nextMenu.SetActive(true);
        currentMenu.SetActive(false);
    }

    // toggle for D3 attacks
    // called from the attack menu
    public void D3Toggle(bool toggle)
    {
        if (toggle)
        {
            vAttacks = VariantAttacks.D3;
            d6Toggle.GetComponent<Toggle>().isOn = false;
            Debug.Log("The toggle is on: " + vAttacks);
        }
        else
        {
            vAttacks = VariantAttacks.Single;
            Debug.Log("The toggle is off: " + vAttacks);
        }
    }

    // toggle for D6 attacks
    // called from the attack menu
    public void D6Toggle(bool toggle)
    {
        if (toggle)
        {
            vAttacks = VariantAttacks.D6;
            d3Toggle.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            vAttacks = VariantAttacks.Single;
        }
    }

    // takes in the user input to start the stardard attack sequence
    // this is to called from the menu by pressing the Atack! button after entering the required info
    public void StandardAttackStart()
    {
        // Assigning vaules from the input fields
        int.TryParse(numOfAttacksInput.GetComponent<Text>().text, out numOfAttacks);
        int.TryParse(toHitInput.GetComponent<Text>().text, out toHit);
        int.TryParse(toWoundInput.GetComponent<Text>().text, out toWound);

        // verify that the inputs are within correct ranges
        if (numOfAttacks > 0 && (toHit > 0 && toHit <= 6))
        {
            // checks to see if there is a variant number of attacks such as d3 or d6
            if (vAttacks == VariantAttacks.Single)
            {
                RollToHit();
            }
            else if (vAttacks == VariantAttacks.D3)
            {
                CalcNumOfAttacksD3();
            }
            else
            {
                CalcNumOfAttacksD6();
            }
        }
    }

    // function follows the StandardAttackStart, rolls to hit
    // this function is called from attack menu once the user presses the Attack! button and after the user's input is taken
    public void RollToHit()
    {
        // change from the attack menu to the hit result menu
        ChangeMenus(attackMenu, hitResultMenu);

        // resetting values to prepare for a new roll
        toHit = toWound = numOfAttacks = numOfHits = 0;

        // set all of the auxillary buttons to disabled.
        // they will be re-enabled as needed
        reRoll1HitButton.SetActive(false);
        reRoll1sHitButton.SetActive(false);
        reRollAllHitsButton.SetActive(false);
        woundButton.SetActive(false);
        reRoll1WoundButton.SetActive(false);
        reRoll1sWoundButton.SetActive(false);
        reRollAllWoundsButton.SetActive(false);

        // rolls the dice
        // numOfAttacks is taken from StandardAttackStart
        RollDice(hitDiePool, numOfAttacks);

        // outputs the hit roll results
        PrintResults(hitDiePool, hitResult);

        //numOfHits = CountSuccesses(toHit, hitDiePool);
        hitResult[6].GetComponent<Text>().text = "Hits: " + numOfHits;

        // as long as the number of hits is greater than 0, activate the wound button
        if (numOfHits > 0) { woundButton.SetActive(true); }
        // if there are any failed hits then activate re-roll 1 and re-roll all failed buttons
        if (numOfHits < numOfAttacks) { reRoll1HitButton.SetActive(true); reRollAllHitsButton.SetActive(true); }
        // if there are any 1's activate the re-roll 1's button
        if (hitDiePool[0] > 0) { reRoll1sHitButton.SetActive(true); }
    }

    // calculate the number of attacks if it's D3
    // this is called if the number of attacks are D3 from the Attack Menu
    public void CalcNumOfAttacksD3()
    {
        // change menus
        ChangeMenus(attackMenu, d3AttacksMenu);

        // rolls a number of D6 dice equal to numOfAttacks
        // numOfAttacks is obtained from the Attack Menu
        RollDice(variantAttacks, numOfAttacks);

        // outputs the hit roll results
        PrintResults(variantAttacks, d3Result);

        numOfAttacks = (variantAttacks[0] + variantAttacks[1]) + (2 * (variantAttacks[2] + variantAttacks[3])) + (3 * (variantAttacks[4] + variantAttacks[5]));
        d3Result[6].GetComponent<Text>().text = "Total Attacks: " + numOfAttacks;
    }

    // calculate the number of attacks if it's D6
    // this is called if the number of attacks are D6 from the Attack Menu
    public void CalcNumOfAttacksD6()
    {
        // change menus
        ChangeMenus(attackMenu, d6AttacksMenu);

        // rolls a number of D6 dice equal to numOfAttacks
        // numOfAttacks is obtained from the Attack Menu
        RollDice(variantAttacks, numOfAttacks);

        // outputs the hit roll results
        PrintResults(variantAttacks, d6Result);

        numOfAttacks = variantAttacks[0] + (variantAttacks[1] * 2) + (variantAttacks[2] * 3) + (variantAttacks[3] * 4) + (variantAttacks[4] * 5) + (variantAttacks[5] * 6);
        d6Result[6].GetComponent<Text>().text = "Total Attacks: " + numOfAttacks;
    }

    // Printing Functions
    void PrintReRollHits()
    {
        /*int num = CountSuccesses(toHit, reRollDiePool);

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
         }*/
    }

    public void PrintReRollWounds()
    {
        /*int num = CountSuccesses(toWound, reRollDiePool);

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
        }*/
    }

    public void PrintReRollToAttackD3()
    {
        /*for(int i = 0; i < 6; i++)
        {
            if (reRollDiePool[i] > 0)
            {
                d3Result[i].GetComponent<Text>().text = "" + variantAttacks[i] + " + " + reRollDiePool[i];
            }
            else
            {
                d3Result[i].GetComponent<Text>().text = "" + variantAttacks[i];
            }
        }*/
    }

    public void PrintReRollToAttackD6()
    {
        /*for(int i = 0; i < 6; i++)
        {
            if (reRollDiePool[0] > 0)
            {
                d6Result[i].GetComponent<Text>().text = "" + variantAttacks[i] + " + " + reRollDiePool[i];
            }
            else
            {
                d6Die1.GetComponent<Text>().text = "" + variantAttacks[i];
            }
        }*/
    }
}