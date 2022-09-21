using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Attack_Menu_Script : MonoBehaviour
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

    public GameObject hitDie1;
    public GameObject hitDie2;
    public GameObject hitDie3;
    public GameObject hitDie4;
    public GameObject hitDie5;
    public GameObject hitDie6;
    public GameObject totalHits;

    public GameObject woundDie1;
    public GameObject woundDie2;
    public GameObject woundDie3;
    public GameObject woundDie4;
    public GameObject woundDie5;
    public GameObject woundDie6;
    public GameObject totalWounds;

    public GameObject d3Die1;
    public GameObject d3Die2;
    public GameObject d3Die3;
    public GameObject d3Die4;
    public GameObject d3Die5;
    public GameObject d3Die6;
    public GameObject d3TotalAttacks;

    public GameObject d6Die1;
    public GameObject d6Die2;
    public GameObject d6Die3;
    public GameObject d6Die4;
    public GameObject d6Die5;
    public GameObject d6Die6;
    public GameObject d6TotalAttacks;

    public GameObject miscDie1;
    public GameObject miscDie2;
    public GameObject miscDie3;
    public GameObject miscDie4;
    public GameObject miscDie5;
    public GameObject miscDie6;

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
    private enum VariantAttacks {Single, D3, D6 };
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

    // roll misc dice
    public void MiscDiceRoll()
    {
        // initialize values
        numOfMisc = 0;
        Array.Clear(miscDiePool, 0, 6);

        // set the value of the input value to the variable
        int.TryParse(miscDiceInput.GetComponent<Text>().text, out numOfMisc);

        if (numOfMisc > 0)
        {
            // roll misc dice
            RollDice(numOfMisc, miscDiePool);

            // outputs the misc roll results
            miscDie1.GetComponent<Text>().text = "" + miscDiePool[0];
            miscDie2.GetComponent<Text>().text = "" + miscDiePool[1];
            miscDie3.GetComponent<Text>().text = "" + miscDiePool[2];
            miscDie4.GetComponent<Text>().text = "" + miscDiePool[3];
            miscDie5.GetComponent<Text>().text = "" + miscDiePool[4];
            miscDie6.GetComponent<Text>().text = "" + miscDiePool[5];

            // change menus
            miscResultMenu.SetActive(true);
            miscMenu.SetActive(false);
        }
    }

    public void D3Toggle(bool toggle)
    {
        if(toggle)
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

    public void Go()
    {
        // reset values to 0
        toHit = numOfAttacks = numOfHits = weaponStr = targetToughness = toWound = 0;

        // reset all arrays to 0 as not to keep previous rolls
        Array.Clear(hitDiePool, 0, 6);
        Array.Clear(woundDiePool, 0, 6);
        Array.Clear(reRollDiePool, 0, 6);
        Array.Clear(variantAttacks, 0, 6);

        // Assigning vaules from the input fields
        int.TryParse(attacksInput.GetComponent<Text>().text, out numOfAttacks);
        int.TryParse(skillInput.GetComponent<Text>().text, out toHit);
        int.TryParse(weaponStrInput.GetComponent<Text>().text, out weaponStr);
        int.TryParse(targetToughnessInput.GetComponent<Text>().text, out targetToughness);

        // verify that the inputs are within correct ranges
        if (numOfAttacks > 0 && (toHit > 0 && toHit <= 6 ))
        {
            if (vAttacks == VariantAttacks.Single)
            {
                // set number of attacks to the entry
                numOfAttacks = 0;
                int.TryParse(attacksInput.GetComponent<Text>().text, out numOfAttacks);

                // change from the attack menu to the hit result menu
                hitResultMenu.SetActive(true);
                attackMenu.SetActive(false);

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

    public void CalcNumOfAttacksD3()
    {
        // change menus
        d3AttacksMenu.SetActive(true); 
        attackMenu.SetActive(false);

        RollDice(numOfAttacks, variantAttacks);

        // outputs the hit roll results
        d3Die1.GetComponent<Text>().text = "" + variantAttacks[0];
        d3Die2.GetComponent<Text>().text = "" + variantAttacks[1];
        d3Die3.GetComponent<Text>().text = "" + variantAttacks[2];
        d3Die4.GetComponent<Text>().text = "" + variantAttacks[3];
        d3Die5.GetComponent<Text>().text = "" + variantAttacks[4];
        d3Die6.GetComponent<Text>().text = "" + variantAttacks[5];

        numOfAttacks = (variantAttacks[0] + variantAttacks[1]) + (2 * (variantAttacks[2] + variantAttacks[3])) + (3 * (variantAttacks[4] + variantAttacks[5]));
        d3TotalAttacks.GetComponent<Text>().text = "Total Attacks: " + numOfAttacks;
    }

    public void CalcNumOfAttacksD6()
    {
        // change menus
        d6AttacksMenu.SetActive(true);
        attackMenu.SetActive(false);

        RollDice(numOfAttacks, variantAttacks);

        // outputs the hit roll results
        d6Die1.GetComponent<Text>().text = "" + variantAttacks[0];
        d6Die2.GetComponent<Text>().text = "" + variantAttacks[1];
        d6Die3.GetComponent<Text>().text = "" + variantAttacks[2];
        d6Die4.GetComponent<Text>().text = "" + variantAttacks[3];
        d6Die5.GetComponent<Text>().text = "" + variantAttacks[4];
        d6Die6.GetComponent<Text>().text = "" + variantAttacks[5];

        numOfAttacks = variantAttacks[0] + (variantAttacks[1]*2) + (variantAttacks[2]*3) + (variantAttacks[3]*4) + (variantAttacks[4]*5) + (variantAttacks[5]*6);
        d6TotalAttacks.GetComponent<Text>().text = "Total Attacks: " + numOfAttacks;
    }

    // re-roll rolls to attack
    public void ReRollToAttackD3()
    {
        // add one die to the re-roll dice pool
        int oneDie = UnityEngine.Random.Range(0, 6);
        reRollDiePool[oneDie]++;

        // re-roll lowest dice in the main dice pool
        for (int i = 0; i < 6; i++)
        {
            if (variantAttacks[i] > 0)
            {
                variantAttacks[i]--;
                break;
            }
        }

        // print re-rolls
        PrintReRollToAttackD3();

        numOfAttacks = ((variantAttacks[0] + variantAttacks[1]) + (2 * (variantAttacks[2] + variantAttacks[3])) + (3 * (variantAttacks[4] + variantAttacks[5]))) + ((reRollDiePool[0] + reRollDiePool[1]) + (2 * (reRollDiePool[2] + reRollDiePool[3])) + (3 * (reRollDiePool[4] + reRollDiePool[5])));
        d3TotalAttacks.GetComponent<Text>().text = "Total Attacks: " + numOfAttacks;

        // if there are no more dice in the main dice pool deactivate the re-roll button
        if (variantAttacks[0] == 0 && variantAttacks[1] == 0 && variantAttacks[2] == 0 && variantAttacks[3] == 0 && variantAttacks[4] == 0 && variantAttacks[5] == 0)
        {
            reRollToAttackD3Button.SetActive(false);
        }
    }

    // re-roll rolls to attack
    public void ReRollToAttackD6()
    {
        // add one die to the re-roll dice pool
        int oneDie = UnityEngine.Random.Range(0, 6);
        reRollDiePool[oneDie]++;

        // re-roll lowest dice in the main dice pool
        for (int i = 0; i < 6; i++)
        {
            if (variantAttacks[i] > 0)
            {
                variantAttacks[i]--;
                break;
            }
        }

        // print re-rolls
        PrintReRollToAttackD6();

        numOfAttacks = (variantAttacks[0] + (variantAttacks[1] * 2) + (variantAttacks[2] * 3) + (variantAttacks[3] * 4) + (variantAttacks[4] * 5) + (variantAttacks[5] * 6)) + (reRollDiePool[0] + (reRollDiePool[1] * 2) + (reRollDiePool[2] * 3) + (reRollDiePool[3] * 4) + (reRollDiePool[4] * 5) + (reRollDiePool[5] * 6));
        d6TotalAttacks.GetComponent<Text>().text = "Total Attacks: " + numOfAttacks;

        // if there are no more dice in the main dice pool deactivate the re-roll button
        if (variantAttacks[0] == 0 && variantAttacks[1] == 0 && variantAttacks[2] == 0 && variantAttacks[3] == 0 && variantAttacks[4] == 0 && variantAttacks[5] == 0)
        {
            reRollToAttackD6Button.SetActive(false);
        }
    }

    // Roll to hit!
    public void RollToHit()
    {
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
        RollDice(numOfAttacks, hitDiePool);

        // outputs the hit roll results
        hitDie1.GetComponent<Text>().text = "" + hitDiePool[0];
        hitDie2.GetComponent<Text>().text = "" + hitDiePool[1];
        hitDie3.GetComponent<Text>().text = "" + hitDiePool[2];
        hitDie4.GetComponent<Text>().text = "" + hitDiePool[3];
        hitDie5.GetComponent<Text>().text = "" + hitDiePool[4];
        hitDie6.GetComponent<Text>().text = "" + hitDiePool[5];

        numOfHits = CountSuccesses(toHit, hitDiePool);
        totalHits.GetComponent<Text>().text = "Hits: " + numOfHits;

        // as long as the number of hits is greater than 0, activate the wound button
        if (numOfHits > 0) { woundButton.SetActive(true); }
        // if there are any failed hits then activate re-roll 1 and re-roll all failed buttons
        if (numOfHits < numOfAttacks) { reRoll1HitButton.SetActive(true); reRollAllHitsButton.SetActive(true); }
        // if there are any 1's activate the re-roll 1's button
        if (hitDiePool[0] > 0) { reRoll1sHitButton.SetActive(true); }
    }

    // re-roll one failed hit roll for command point re-roll or other abilities
    public void ReRollOneHit()
    {
        // Debug
        Debug.Log("Re-Roll One Hit");

        // roll the re-roll dice pool
        int oneDie = UnityEngine.Random.Range(0, 6);
        reRollDiePool[oneDie]++;

        // remove the lowest die from the dice pool
        for(int i = 0; i < (toHit -1 ); i++)
        {
            if(hitDiePool[i] > 0)
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

        Debug.Log("There are " + hitDiePool[0] + " 1s before the print");

        // output results of the re-roll
        PrintReRollHits();

        Debug.Log("There are " + hitDiePool[0] + " 1s after the print");
    }

    // re-roll all hit rolls of 1
    public void ReRollHitsOf1()
    {
        // roll the re-roll dice pool
        RollDice(hitDiePool[0], reRollDiePool);

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
        PrintReRollHits();
    }

    // re-roll all failed hit rolls
    public void ReRollHitAllFails()
    {
        // roll the re-roll dice pool
        RollDice(CountFailures(toHit, hitDiePool), reRollDiePool);

        // clear failed rolls from the dice pool
        for (int i = 0; i < (toHit - 1); i++)
        {
            hitDiePool[i] = 0;
        }

        // disable re-roll buttons
        reRoll1HitButton.SetActive(false);
        reRoll1sHitButton.SetActive(false);
        reRollAllHitsButton.SetActive(false);

        // if the wound button was disabled and there are new hits from the re-roll enable the wound button
        if (woundButton.activeSelf == false)
        {
            if (CountSuccesses(toHit, reRollDiePool) > 0) { woundButton.SetActive(true); }
        }

        // output results of the re-roll
        PrintReRollHits();
    }

    // roll to wound
    public void RollToWound()
    {
        Debug.Log("to hit: " + toHit);
        // checks to see if there are extra hits to calculate, if there are add them to the number of hits
        int extraHits = CountSuccesses(toHit, reRollDiePool);
        if(extraHits > 0) { numOfHits += extraHits; }
        // rolls the dice
        RollDice(numOfHits, woundDiePool);
        // clears the re-roll dice pool
        Array.Clear(reRollDiePool, 0, 6);

        // outputs the wound roll results
        woundDie1.GetComponent<Text>().text = "" + woundDiePool[0];
        woundDie2.GetComponent<Text>().text = "" + woundDiePool[1];
        woundDie3.GetComponent<Text>().text = "" + woundDiePool[2];
        woundDie4.GetComponent<Text>().text = "" + woundDiePool[3];
        woundDie5.GetComponent<Text>().text = "" + woundDiePool[4];
        woundDie6.GetComponent<Text>().text = "" + woundDiePool[5];

        // determine the number needed to wound
        toWound = CalcWound();
        numOfWounds = CountSuccesses(toWound, woundDiePool);
        totalWounds.GetComponent<Text>().text = "Wounds: " + numOfWounds;

        // if there are any failed wounds then activate re-roll 1 and re-roll all failed buttons
        if (numOfHits > numOfWounds) { reRoll1WoundButton.SetActive(true); reRollAllWoundsButton.SetActive(true); }
        // if there are any 1's activate the re-roll 1's button
        if (woundDiePool[0] > 0) { reRoll1sWoundButton.SetActive(true); }
    }

    // re-roll one failed wound roll for command point re-roll or other abilities
    public void ReRollOneWound()
    {
        // roll the re-roll dice pool
        int oneDie = UnityEngine.Random.Range(0, 6);
        reRollDiePool[oneDie]++;

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
        if(CountFailures(toWound, woundDiePool) == 0)
        {
            reRoll1WoundButton.SetActive(false);
            reRoll1sWoundButton.SetActive(false);
            reRollAllWoundsButton.SetActive(false);
        }
        else if(woundDiePool[0] == 0)
        {
            reRoll1sWoundButton.SetActive(false);
        }

        // output results of the re-roll
        PrintReRollWounds();
    }

    // re-roll all wound rolls of 1
    public void ReRollWoundsOf1()
    {
        // roll the re-roll dice pool
        RollDice(woundDiePool[0], reRollDiePool);

        // remove the 1's from the dice pool
        woundDiePool[0] = 0;
        // disable re-roll 1's button and re-roll all hits
        reRoll1sWoundButton.SetActive(false);
        // if there are no more failed hits disable re-roll 1 hit button
        if (CountFailures(toWound, woundDiePool) == 0) { reRoll1WoundButton.SetActive(false); reRollAllWoundsButton.SetActive(false); }

        // output results of the re-roll
        PrintReRollWounds();
    }
    
    // re-roll all failed wound rolls
    public void ReRollWoundAllFails()
    {
        // roll the re-roll dice pool
        RollDice(CountFailures(toWound, woundDiePool), reRollDiePool);

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
        PrintReRollWounds();
    }

    // takes a num and an array
    // num is the number of dice and dicePool is the array of dice ex
    public void RollDice(int num, int[] dicePool)
    {
        for (int i = 0; i < num; i++)
        {
            dicePool[UnityEngine.Random.Range(0, 6)]++;
        }
    }

    // takes a num and an arracy and returns an int
    // the num is the lower limit to count the number of successful rolls in the dice array
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
    public int CountFailures(int num, int[] dicePool)
    {
        int numOfFail = 0;

        for (int i = 0; i < (num - 1); i++)
        {
            numOfFail += dicePool[i];
        }

        return numOfFail;
    }

    // calculates the wound value
    public int CalcWound()
    {
        int woundNum = 0;

        // Calculating the number needed to wound
        if (weaponStr == targetToughness)
        {
            // If the equal to, 4+ is needed to wound
            woundNum = 4;
        }
        else if (weaponStr > targetToughness)
        {
            if (weaponStr >= (targetToughness * 2))
            {
                // if str is equal to or greater than double the toughness, 2+ is needed to wound
                woundNum = 2;
            }
            else
            {
                // if str is greater than but not qual to or greater than double the toughness, 3+ is needed to wound
                woundNum = 3;
            }
        }
        else
        {
            if (targetToughness >= (weaponStr * 2))
            {
                // if toughness is equal to or greater than double the str, 6+ is needed to wound
                woundNum = 6;
            }
            else
            {
                // if toughness is greater than but not qual to or greater than double the str, 5+ is needed to wound
                woundNum = 5;
            }
        }

        return woundNum;
    }

    // outputs the re-rolls to hit on the screen
    void PrintReRollHits()
    {
        int num = CountSuccesses(toHit, reRollDiePool);

        // outputs the wound roll results
        if (num > 0)
        {
            totalHits.GetComponent<Text>().text = "Hits: " + numOfHits + " + " + num;
        }
        
        if (reRollDiePool[0] > 0)
        {
           hitDie1.GetComponent<Text>().text = "" + hitDiePool[0] + " + " + reRollDiePool[0];
        }
        else
        {
            hitDie1.GetComponent<Text>().text = "" + hitDiePool[0];
        }

        if (reRollDiePool[1] > 0)
        {
            hitDie2.GetComponent<Text>().text = "" + hitDiePool[1] + " + " + reRollDiePool[1];
        }
        else
        {
            hitDie2.GetComponent<Text>().text = "" + hitDiePool[1];
        }

        if (reRollDiePool[2] > 0)
        {
            hitDie3.GetComponent<Text>().text = "" + hitDiePool[2] + " + " + reRollDiePool[2];
        }
        else
        {
            hitDie3.GetComponent<Text>().text = "" + hitDiePool[2];
        }

        if (reRollDiePool[3] > 0)
        {
            hitDie4.GetComponent<Text>().text = "" + hitDiePool[3] + " + " + reRollDiePool[3];
        }
        else
        {
            hitDie4.GetComponent<Text>().text = "" + hitDiePool[3];
        }

        if (reRollDiePool[4] > 0)
        {
            hitDie5.GetComponent<Text>().text = "" + hitDiePool[4] + " + " + reRollDiePool[4];
        }
        else
        {
            hitDie5.GetComponent<Text>().text = "" + hitDiePool[4];
        }

        if (reRollDiePool[5] > 0)
        {
            hitDie6.GetComponent<Text>().text = "" + hitDiePool[5] + " + " + reRollDiePool[5];
        }
        else
        {
            hitDie6.GetComponent<Text>().text = "" + hitDiePool[5];
        }
    }

    // outputs the re-rolls to wound on the screen
    void PrintReRollWounds()
    {
        int num = CountSuccesses(toWound, reRollDiePool);

        // outputs the wound roll results
        if (num > 0)
        {
            totalWounds.GetComponent<Text>().text = "Wounds: " + numOfWounds + " + " + num;
        }
        
        if(reRollDiePool[0] > 0)
        {
            woundDie1.GetComponent<Text>().text = "" + woundDiePool[0] + " + " + reRollDiePool[0];
        }
        else
        {
            woundDie1.GetComponent<Text>().text = "" + woundDiePool[0];
        }
        if (reRollDiePool[1] > 0)
        {
            woundDie2.GetComponent<Text>().text = "" + woundDiePool[1] + " + " + reRollDiePool[1];
        }
        else
        {
            woundDie2.GetComponent<Text>().text = "" + woundDiePool[1];
        }
        if (reRollDiePool[2] > 0)
        {
            woundDie3.GetComponent<Text>().text = "" + woundDiePool[2] + " + " + reRollDiePool[2];
        }
        else
        {
            woundDie3.GetComponent<Text>().text = "" + woundDiePool[2];
        }
        if (reRollDiePool[3] > 0)
        {
            woundDie4.GetComponent<Text>().text = "" + woundDiePool[3] + " + " + reRollDiePool[3];
        }
        else
        {
            woundDie4.GetComponent<Text>().text = "" + woundDiePool[3];
        }
        if (reRollDiePool[4] > 0)
        {
            woundDie5.GetComponent<Text>().text = "" + woundDiePool[4] + " + " + reRollDiePool[4];
        }
        else
        {
            woundDie5.GetComponent<Text>().text = "" + woundDiePool[4];
        }
        if (reRollDiePool[5] > 0)
        {
            woundDie6.GetComponent<Text>().text = "" + woundDiePool[5] + " + " + reRollDiePool[5];
        }
        else
        {
            woundDie6.GetComponent<Text>().text = "" + woundDiePool[5];
        }
    }

    public void PrintReRollToAttackD3()
    {
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
    }
}
