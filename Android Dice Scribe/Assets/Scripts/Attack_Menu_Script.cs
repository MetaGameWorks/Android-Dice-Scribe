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

    int attacks = 0;
    int hit = 0;
    int str = 0;
    int toughness = 0;
    int wound = 0;

    int[] hitDiePool = new int[6];
    int[] woundDiePool = new int[6];
    int[] reRollDiePool = new int[6];

    public void RollToHit()
    {
        // reset values to 0
        // attacks,hit,str,toughness, wound = 0;

        // reset all arrays to 0 as not to keep previous rolls
        Array.Clear(hitDiePool, 0, 6);
        Array.Clear(woundDiePool, 0, 6);
        Array.Clear(reRollDiePool, 0, 6);

        // Assigning vaules from the input fields
        int.TryParse(attacksInput.GetComponent<Text>().text, out int attacks);
        int.TryParse(skillInput.GetComponent<Text>().text, out int hit);
        int.TryParse(weaponStrInput.GetComponent<Text>().text, out int str);
        int.TryParse(targetToughnessInput.GetComponent<Text>().text, out int toughness);

        

        // rolling function call
        if (attacks > 0 && (hit > 0 && hit < 7))
        {
            // rolls the dice
            RollDice(attacks, hitDiePool);

            // outputs the hit roll results
            hitDie1.GetComponent<Text>().text = "" + hitDiePool[0];
            hitDie2.GetComponent<Text>().text = "" + hitDiePool[1];
            hitDie3.GetComponent<Text>().text = "" + hitDiePool[2];
            hitDie4.GetComponent<Text>().text = "" + hitDiePool[3];
            hitDie5.GetComponent<Text>().text = "" + hitDiePool[4];
            hitDie6.GetComponent<Text>().text = "" + hitDiePool[5];

            totalHits.GetComponent<Text>().text = "Hits: " + CountSuccesses(hit, hitDiePool);
        }
    }

    // re-roll one failed hit roll for command point re-roll or other abilities
    public void ReRollOneHit()
    {

    }

    // re-roll all hit rolls of 1
    public void ReRollHitOf1()
    {
        
    }

    // re-roll all failed hit rolls
    public void ReRollHitAllFails()
    {

    }

    // roll to wound
    public void RollToWound()
    {
        // clears the re-roll dice pool
        Array.Clear(reRollDiePool, 0, 6);

        // rolls the dice
        RollDice(attacks, woundDiePool);

        // outputs the wound roll results
        woundDie1.GetComponent<Text>().text = "" + woundDiePool[0];
        woundDie2.GetComponent<Text>().text = "" + woundDiePool[1];
        woundDie3.GetComponent<Text>().text = "" + woundDiePool[2];
        woundDie4.GetComponent<Text>().text = "" + woundDiePool[3];
        woundDie5.GetComponent<Text>().text = "" + woundDiePool[4];
        woundDie6.GetComponent<Text>().text = "" + woundDiePool[5];

        totalWounds.GetComponent<Text>().text = "Wounds: " + CountSuccesses(wound, woundDiePool);
    }

    // re-roll one failed wound roll for command point re-roll or other abilities
    public void ReRollOneWound()
    {

    }

    // re-roll all wound rolls of 1
    public void ReRollWoundOf1()
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

    // calculates the wound value
    public int CalcWound()
    {
        int woundNum = 0;

        // Calculating the number needed to wound
        if (str == toughness)
        {
            // If the equal to, 4+ is needed to wound
            woundNum = 4;
        }
        else if (str > toughness)
        {
            if (str >= (toughness * 2))
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
            if (toughness >= (str * 2))
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

    //***************************************************************************************************************************************************************************************************

    // function to be called when the player hits the Attack! button
    public void Attack()
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
    }
}
