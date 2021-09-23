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

    // variables for the results fields
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

    // re-roll buttons to be disabled or enabled based on availability
    public GameObject reRoll1HitButton;
    public GameObject reRoll1sHitButton;
    public GameObject reRollAllHitButton;
    public GameObject woundButton;
    public GameObject reRoll1WoundButton;
    public GameObject reRoll1sWoundButton;
    public GameObject reRollAllWoundButton;

    private int numOfAttacks = 0;
    private int toHit = 0;
    private int numOfHits = 0;
    private int numOfWounds = 0;
    private int weaponStr = 0;
    private int targetToughness = 0;
    private int toWound = 0;

    int[] hitDiePool = new int[6];
    int[] woundDiePool = new int[6];
    int[] reRollDiePool = new int[6];


    public void RollToHit()
    {
        // reset values to 0
        numOfAttacks = toHit = numOfHits = weaponStr = targetToughness = toWound = 0;

        // reset all arrays to 0 as not to keep previous rolls
        Array.Clear(hitDiePool, 0, 6);
        Array.Clear(woundDiePool, 0, 6);
        Array.Clear(reRollDiePool, 0, 6);

        // set all of the auxillary buttons to disabled.
        // they will be re-enabled as needed
        reRoll1HitButton.SetActive(false);
        reRoll1sHitButton.SetActive(false);
        reRollAllHitButton.SetActive(false);
        woundButton.SetActive(false);
        reRoll1WoundButton.SetActive(false);
        reRoll1sWoundButton.SetActive(false);
        reRollAllWoundButton.SetActive(false);

        // Assigning vaules from the input fields
        int.TryParse(attacksInput.GetComponent<Text>().text, out numOfAttacks);
        int.TryParse(skillInput.GetComponent<Text>().text, out toHit);
        int.TryParse(weaponStrInput.GetComponent<Text>().text, out weaponStr);
        int.TryParse(targetToughnessInput.GetComponent<Text>().text, out targetToughness);

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
        if (numOfHits > numOfAttacks) { reRoll1HitButton.SetActive(true); reRollAllHitButton.SetActive(true); }
        // if there are any 1's activate the re-roll 1's button
        if (hitDiePool[0] > 0) { reRoll1sHitButton.SetActive(true); }
    }

    // re-roll one failed hit roll for command point re-roll or other abilities
    public void ReRollOneHit()
    {
        // roll the re-roll dice pool
        RollDice(1, reRollDiePool);

        // output results of the re-roll
        

        // if there are no more failed hits deactivate the re-roll 1 and re-roll all failed buttons
        if (CountFailures(toHit, hitDiePool) > 0) { reRoll1HitButton.SetActive(false); reRollAllHitButton.SetActive(false); }
    }

    // re-roll all hit rolls of 1
    public void ReRollHitsOf1()
    {
        // roll the re-roll dice pool
        RollDice(hitDiePool[0], reRollDiePool);

        // output results of the re-roll


        //
    }

    // re-roll all failed hit rolls
    public void ReRollHitAllFails()
    {
        // roll the re-roll dice pool
        RollDice(toHit, reRollDiePool);

        // output results of the re-roll


        // if there are no more failed hits deactivate the re-roll 1 and re-roll all failed buttons
        if (CountFailures(toHit, hitDiePool) > 0) { reRoll1HitButton.SetActive(false); reRollAllHitButton.SetActive(false); }
    }

    // roll to wound
    public void RollToWound()
    {
        // clears the re-roll dice pool
        Array.Clear(reRollDiePool, 0, 6);

        // rolls the dice
        RollDice(numOfHits, woundDiePool);

        // outputs the wound roll results
        woundDie1.GetComponent<Text>().text = "" + woundDiePool[0];
        woundDie2.GetComponent<Text>().text = "" + woundDiePool[1];
        woundDie3.GetComponent<Text>().text = "" + woundDiePool[2];
        woundDie4.GetComponent<Text>().text = "" + woundDiePool[3];
        woundDie5.GetComponent<Text>().text = "" + woundDiePool[4];
        woundDie6.GetComponent<Text>().text = "" + woundDiePool[5];

        numOfWounds = CountSuccesses(toWound, woundDiePool);
        totalWounds.GetComponent<Text>().text = "Wounds: " + numOfWounds;

        // if there are any failed wounds then activate re-roll 1 and re-roll all failed buttons
        if (numOfHits > numOfWounds) { reRoll1WoundButton.SetActive(true); reRollAllWoundButton.SetActive(true); }
        // if there are any 1's activate the re-roll 1's button
        if (woundDiePool[0] > 0) { reRoll1sWoundButton.SetActive(true); }
    }

    // re-roll one failed wound roll for command point re-roll or other abilities
    public void ReRollOneWound()
    {

    }

    // re-roll all wound rolls of 1
    public void ReRollWoundsOf1()
    {

    }
    
    // re-roll all failed wound rolls
    public void ReRollWoundAllFails()
    {

    }

    // takes a num and an array
    // num is the number of dice and dicePool is the array of dice ex
    public void RollDice(int num, int[] dicePool)
    {
        for (int i = 0; i < num; i++)
        {
            dicePool[UnityEngine.Random.Range(0, 5)]++;
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

    // searches a dice pool for the lowest failure and removes it
    void RemoveLowestFail(int num, int[] dicePool)
    {
        // finds the lowest failed roll to re-roll
    }

    //***************************************************************************************************************************************************************************************************

    // function to be called when the player hits the Attack! button
    /*public void Attack()
    {
        // Assigning vaules from the input fields
        int.TryParse(attacksInput.GetComponent<Text>().text, out int attacks);
        int.TryParse(skillInput.GetComponent<Text>().text, out int hit);
        int.TryParse(weaponStrInput.GetComponent<Text>().text, out int str);
        int.TryParse(targetToughnessInput.GetComponent<Text>().text, out int toughness);

        Debug.Log("attacks: " + attacks);
        Debug.Log("hit: " + hit);
        Debug.Log("str: " + str);
        Debug.Log("toughness: " + toughness);

        // Calculating the number needed to wound
        if (str == toughness)
        {
            // If the equal to, 4+ is needed to wound
            wound = 4;
        }
        else if(str > toughness)
        {
            if(str >= (toughness*2))
            {
                // if str is equal to or greater than double the toughness, 2+ is needed to wound
                wound = 2;
            }
            else
            {
                // if str is greater than but not qual to or greater than double the toughness, 3+ is needed to wound
                wound = 3;
            }
        }
        else
        {
            if (toughness >= (str * 2))
            {
                // if toughness is equal to or greater than double the str, 6+ is needed to wound
                wound = 6;
            }
            else
            {
                // if toughness is greater than but not qual to or greater than double the str, 5+ is needed to wound
                wound = 5;
            }
        }
        Debug.Log("wound: " + wound);
        // need to create catches to ensure that numbers were entered and that those numbers were within certain bounds

        // rolling function call
        if(attacks > 0 && (hit > 0 && hit < 7) && str > 0 && toughness > 0)
        {
            Roll(attacks, hit, wound);
        }
    }

    public void Roll(int attacks, int hitReq, int woundReq)
    {
        // initializing to 0
        hitDiePool = new int[6];
        woundDiePool = new int[6];
        int numOfHits = 0;
        int numOfWounds = 0;

        // rolling to hit
        for (int i = 1; i <= attacks; i++)
        {
            hitDiePool[UnityEngine.Random.Range(0, 5)]++;
        }

        // outputs the hit roll results
        hitDie1.GetComponent<Text>().text = "" + hitDiePool[0];
        hitDie2.GetComponent<Text>().text = "" + hitDiePool[1];
        hitDie3.GetComponent<Text>().text = "" + hitDiePool[2];
        hitDie4.GetComponent<Text>().text = "" + hitDiePool[3];
        hitDie5.GetComponent<Text>().text = "" + hitDiePool[4];
        hitDie6.GetComponent<Text>().text = "" + hitDiePool[5];
        Debug.Log("Hits - 6: " + hitDiePool[5] + " / 5: " + hitDiePool[4] + " / 4: " + hitDiePool[3] + " / 3: " + hitDiePool[2] + " / 2: " + hitDiePool[1] + " / 1: " + hitDiePool[0]);

        // counts the successful hits
        for (int i = 5; i >= (hitReq - 1); i--)
        {
            numOfHits += hitDiePool[i];
        }

        totalHits.GetComponent<Text>().text = "Hits: " + numOfHits;
        Debug.Log(numOfHits + " Hits");

        // rolling to wound
        for (int i = 1; i <= numOfHits; i++)
        {
            woundDiePool[UnityEngine.Random.Range(0, 5)]++;
        }

        // outputs the wound roll results
        woundDie1.GetComponent<Text>().text = "" + woundDiePool[0];
        woundDie2.GetComponent<Text>().text = "" + woundDiePool[1];
        woundDie3.GetComponent<Text>().text = "" + woundDiePool[2];
        woundDie4.GetComponent<Text>().text = "" + woundDiePool[3];
        woundDie5.GetComponent<Text>().text = "" + woundDiePool[4];
        woundDie6.GetComponent<Text>().text = "" + woundDiePool[5];
        Debug.Log("Wounds - 6: " + woundDiePool[5] + " / 5: " + woundDiePool[4] + " / 4: " + woundDiePool[3] + " / 3: " + woundDiePool[2] + " / 2: " + woundDiePool[1] + " / 1: " + woundDiePool[0]);

        // counts the successful wounds
        for (int i = 5; i >= (woundReq - 1); i--)
        {
            numOfWounds += woundDiePool[i];
        }

        // outputs the number of successful wounds
        totalWounds.GetComponent<Text>().text = "Wounds: " + numOfWounds;
        Debug.Log(numOfWounds + " Wounds");
    }*/
}
