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

    // takes in an dice pool array and a num and rolls a number of d6 dice equal to num
    // this is to be used in other functions
    // this is different because it doesn't clear the array when rolling
    public void ReRollDice(int[] dicePool, int num)
    {
        // rolls a random number 0-5 a number of times equal to num
        for (int i = num; i > 0; i--)
        {
            dicePool[UnityEngine.Random.Range(0, 6)]++;
        }
    }

    // takes a num and an arracy and returns an int
    // the num is the lower limit to count the number of successful rolls in the dice array
    // this is to be used in other functions
    public int CountSuccesses(int num, int[] dicePool)
    {
        int numOfSuc = 0;

        for (int i = 5; i >= (num - 1); i--)
        {
            numOfSuc += dicePool[i];
        }

        return numOfSuc;
    }

    // takes a num and an arracy and returns an int
    // the num is the upper limit to count the number of failed rolls in the dice array
    // this is to be used in other functions
    public int CountFailures(int num, int[] dicePool)
    {
        int numOfFail = 0;

        for (int i = 0; i < (num - 1); i++)
        {
            numOfFail += dicePool[i];
        }

        return numOfFail;
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

    // outputs re-rolls if the results change
    // called by other functions during all re-rolls
    public void PrintReRolls(int[] dicePool, GameObject[] resultText, int currentTotal, int goal)
    {
        // outputs the new total if it changed
        if (CountSuccesses(goal, dicePool) > 0)
        {
            resultText[6].GetComponent<Text>().text = "" + currentTotal + " + " + goal;
        }

        for (int i = 0; i < 6; i++)
        {
            if (reRollDiePool[i] > 0)
            {
                resultText[i].GetComponent<Text>().text = "" + dicePool[i] + " + " + reRollDiePool[i];
            }
            else
            {
                resultText[i].GetComponent<Text>().text = "" + dicePool[i];
            }
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
    // called from the attack menu when toggling the D3 switch
    public void D3Toggle(bool toggle)
    {
        if (toggle)
        {
            vAttacks = VariantAttacks.D3;
            d6Toggle.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            vAttacks = VariantAttacks.Single;
        }
    }

    // toggle for D6 attacks
    // called from the attack menu when toggling the D6 switch
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
    // this is to called from the menu by pressing the Attack! button after entering the required info
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
                CalcNumOfVariantAttacks(d3Result, d3AttacksMenu, true);
            }
            else
            {
                CalcNumOfVariantAttacks(d6Result, d6AttacksMenu, false);
            }
        }
    }

    // function that handles variant number of attacks
    // takes in the array of text results, the destination menu, and a bool to determine if it's for D3 or D6
    void CalcNumOfVariantAttacks(GameObject[] dicePool, GameObject menu, bool isD3)
    {
        // change menus
        ChangeMenus(attackMenu, menu);

        // roll dice
        RollDice(variantAttacks, numOfAttacks);

        // outputs the hit roll results
        for(int i = 0; i < 6; i++)
		{
            dicePool[i].GetComponent<Text>().text = "" + variantAttacks[i];
        }

        // determines if it's D3 or D6
        if(isD3)
		{
            numOfAttacks = (variantAttacks[0] + variantAttacks[1]) + (2 * (variantAttacks[2] + variantAttacks[3])) + (3 * (variantAttacks[4] + variantAttacks[5]));
            d3Result[6].GetComponent<Text>().text = "Total Attacks: " + numOfAttacks;
        }
		else
		{
            numOfAttacks = variantAttacks[0] + (variantAttacks[1] * 2) + (variantAttacks[2] * 3) + (variantAttacks[3] * 4) + (variantAttacks[4] * 5) + (variantAttacks[5] * 6);
            d6Result[6].GetComponent<Text>().text = "Total Attacks: " + numOfAttacks;
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

        Array.Clear(reRollDiePool, 0, reRollDiePool.Length);

        // rolls the dice
        // numOfAttacks is taken from StandardAttackStart
        RollDice(hitDiePool, numOfAttacks);

        // outputs the hit roll results
        PrintResults(hitDiePool, hitResult);

        // counts the number of hits in the dice pool
        numOfHits = CountSuccesses(toHit, hitDiePool);
        hitResult[6].GetComponent<Text>().text = "Hits: " + numOfHits;

        // as long as the number of hits is greater than 0, activate the wound button
        if (numOfHits > 0) { woundButton.SetActive(true); }
        // if there are any failed hits then activate re-roll 1 and re-roll all failed buttons
        if (numOfHits < numOfAttacks) { reRoll1HitButton.SetActive(true); reRollAllHitsButton.SetActive(true); }
        // if there are any 1's activate the re-roll 1's button
        if (hitDiePool[0] > 0) { reRoll1sHitButton.SetActive(true); }
    }

    // re-roll one failed hit roll for command point re-roll or other abilities
    // function is called from the hit result menu when pressing the re-roll buttons
    public void ReRollOneHit()
    {
        // roll the re-roll dice pool
        reRollDiePool[UnityEngine.Random.Range(0, 6)]++;

        // remove the lowest die from the dice pool
        for (int i = 0; i < (toHit - 1); i++)
        {
            if (hitDiePool[i] > 0)
            {
                hitDiePool[i]--;
                break;
            }
        }

        // deactivate buttons depending on the number of available failed rolls
        if (CountFailures(toHit, hitDiePool) == 0)
        {
            reRoll1HitButton.SetActive(false);
            reRoll1sHitButton.SetActive(false);
            reRollAllHitsButton.SetActive(false);
        }
        else if (hitDiePool[0] == 0)
        {
            reRoll1sHitButton.SetActive(false);
        }

        // if the wound button was disabled and there are new hits from the re-roll enable the wound button
        if (woundButton.activeSelf == false)
        {
            if (CountSuccesses(toHit, reRollDiePool) > 0) { woundButton.SetActive(true); }
        }

        // output results of the re-roll
        PrintReRolls(hitDiePool, hitResult, CountSuccesses(toHit, hitDiePool), toHit);
    }

    // re-roll all hit rolls of 1
    // function is called from the hit result menu when pressing the re-roll buttons
    public void ReRollHitsOf1()
    {
        // roll the re-roll dice pool
        ReRollDice(reRollDiePool, hitDiePool[0]);

        // remove the 1's from the dice pool
        hitDiePool[0] = 0;
        // disable re-roll 1's button and re-roll all hits
        reRoll1sHitButton.SetActive(false);
        reRollAllHitsButton.SetActive(false);
        // if there are no more failed hits disable re-roll 1 hit button
        if (CountFailures(toHit, hitDiePool) == 0) { reRoll1HitButton.SetActive(false); }

        // if the wound button was disabled and there are new hits from the re-roll enable the wound button
        if (woundButton.activeSelf == false)
        {
            if (CountSuccesses(toHit, reRollDiePool) > 0) { woundButton.SetActive(true); }
        }

        // output results of the re-roll
        PrintReRolls(hitDiePool, hitResult, CountSuccesses(toHit, hitDiePool), toHit);
    }

    // re-roll all failed hit rolls
    // function is called from the hit result menu when pressing the re-roll buttons
    public void ReRollHitAllFails()
    {
        // roll the re-roll dice pool
        ReRollDice(reRollDiePool, CountFailures(toHit, hitDiePool));

        // clear failed rolls from the dice pool
        for (int i = 0; i < (toHit - 1); i++)
        {
            hitDiePool[i] = 0;
        }

        // disable re-roll buttons, all failures are gone
        reRoll1HitButton.SetActive(false);
        reRoll1sHitButton.SetActive(false);
        reRollAllHitsButton.SetActive(false);

        // if the wound button was disabled and there are new hits from the re-roll enable the wound button
        if (woundButton.activeSelf == false)
        {
            if (CountSuccesses(toHit, reRollDiePool) > 0) { woundButton.SetActive(true); }
        }

        // output results of the re-roll
        PrintReRolls(hitDiePool, hitResult, CountSuccesses(toHit, hitDiePool), toHit);
    }

    // roll to wound
    // function called after hits are calculated and the Wound button is pressed
    public void RollToWound()
    {
        Array.Clear(reRollDiePool, 0, reRollDiePool.Length);

        // rolls the dice
        RollDice(woundDiePool, numOfHits);

        // outputs the wound roll results
        for(int i = 0; i < 6; i++)
		{
            woundResult[i].GetComponent<Text>().text = "" + woundDiePool[i];
        }

        numOfWounds = CountSuccesses(toWound, woundDiePool);
        woundResult[6].GetComponent<Text>().text = "Wounds: " + numOfWounds;

        // if there are any failed wounds then activate re-roll 1 and re-roll all failed buttons
        if (numOfHits > numOfWounds) { reRoll1WoundButton.SetActive(true); reRollAllWoundsButton.SetActive(true); }
        // if there are any 1's activate the re-roll 1's button
        if (woundDiePool[0] > 0) { reRoll1sWoundButton.SetActive(true); }
    }

    // re-roll one failed wound roll for command point re-roll or other abilities
    public void ReRollOneWound()
    {
        // roll the re-roll dice pool
        reRollDiePool[UnityEngine.Random.Range(0, 6)]++;

        // remove the lowest die from the dice pool
        for (int i = 0; i < (toWound - 1); i++)
        {
            if (woundDiePool[i] > 0)
            {
                woundDiePool[i]--;
                break;
            }
        }

        // deactivate buttons depending on the number of available failed rolls
        if (CountFailures(toWound, woundDiePool) == 0)
        {
            reRoll1WoundButton.SetActive(false);
            reRoll1sWoundButton.SetActive(false);
            reRollAllWoundsButton.SetActive(false);
        }
        else if (woundDiePool[0] == 0)
        {
            reRoll1sWoundButton.SetActive(false);
        }

        // output results of the re-roll
        PrintReRolls(woundDiePool, hitResult, CountSuccesses(toWound, woundDiePool), toWound);
    }

    // re-roll all wound rolls of 1
    public void ReRollWoundsOf1()
    {
        // roll the re-roll dice pool
        ReRollDice(reRollDiePool, woundDiePool[0]);

        // remove the 1's from the dice pool
        woundDiePool[0] = 0;
        // disable re-roll 1's button and re-roll all hits
        reRoll1sWoundButton.SetActive(false);
        // if there are no more failed hits disable re-roll 1 hit button
        if (CountFailures(toWound, woundDiePool) == 0) { reRoll1WoundButton.SetActive(false); reRollAllWoundsButton.SetActive(false); }

        // output results of the re-roll
        PrintReRolls(woundDiePool, hitResult, CountSuccesses(toWound, woundDiePool), toWound);
    }

    // re-roll all failed wound rolls
    public void ReRollWoundAllFails()
    {
        // roll the re-roll dice pool
        ReRollDice(reRollDiePool, CountFailures(toWound, woundDiePool));

        // clear failed rolls from the dice pool
        for (int i = 0; i < (toWound - 1); i++)
        {
            woundDiePool[i] = 0;
        }

        // disable re-roll buttons
        reRoll1WoundButton.SetActive(false);
        reRoll1sWoundButton.SetActive(false);
        reRollAllWoundsButton.SetActive(false);

        // output results of the re-roll
        PrintReRolls(woundDiePool, hitResult, CountSuccesses(toWound, woundDiePool), toWound);
    }
}