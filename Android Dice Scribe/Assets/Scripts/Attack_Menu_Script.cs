using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack_Menu_Script : MonoBehaviour
{
    public GameObject attacksInput;
    public GameObject skillInput;
    public GameObject weaponStrInput;
    public GameObject targetToughnessInput;

    //private GameObject numOfAttacks;
    //private GameObject numToHit;
    //private GameObject numToWound;
    public int attacks = 0;
    public int hit = 0;
    public int str = 0;
    public int toughness = 0;
    public int wound = 0;

    public bool explodeHit = false;
    public int[] hitDiePool;
    public int[] extraDiePool;
    public int[] woundDiePool;

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
            hitDiePool[Random.Range(0, 5)]++;
        }

        // outputs the hit roll results
        Debug.Log("Hits - 6: " + hitDiePool[5] + " / 5: " + hitDiePool[4] + " / 4: " + hitDiePool[3] + " / 3: " + hitDiePool[2] + " / 2: " + hitDiePool[1] + " / 1: " + hitDiePool[0]);

        // hit rolls of 6 grants extra attacks
        /*if(explodeHit)
        {
            if(hitDiePool[5] != 0)
            {
                int extraHits = hitDiePool[5];
                extraDiePool = new int[6];

                for(int i = 1; i <= attacks; i++)
                {
                    extraDiePool[Random.Range(0, 5)]++;
                }

                for (int i = 5; i >= (hitReq - 1); i--)
                {
                    numOfHits += hitDiePool[i];
                }

                Debug.Log("Exploding Hits - 6: " + extraDiePool[5] + " / 5: " + extraDiePool[4] + " / 4: " + extraDiePool[3] + " / 3: " + extraDiePool[2] + " / 2: " + extraDiePool[1] + " / 1: " + extraDiePool[0]);
            }
        }*/

        // counts the successful hits
        for (int i = 5; i >= (hitReq - 1); i--)
        {
            numOfHits += hitDiePool[i];
        }

        Debug.Log(numOfHits + " Hits");

        // rolling to wound
        for (int i = 1; i <= numOfHits; i++)
        {
            woundDiePool[Random.Range(0, 5)]++;
        }

        // outputs the wound roll results
        Debug.Log("Wounds - 6: " + woundDiePool[5] + " / 5: " + woundDiePool[4] + " / 4: " + woundDiePool[3] + " / 3: " + woundDiePool[2] + " / 2: " + woundDiePool[1] + " / 1: " + woundDiePool[0]);

        // counts the successful wounds
        for (int i = 5; i >= (woundReq - 1); i--)
        {
            numOfWounds += woundDiePool[i];
        }

        // outputs the number of successful wounds
        Debug.Log(numOfWounds + " Wounds");
    }
}
