using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using System.Linq;

public class AttackDice : MonoBehaviour
{
    // variables for the menus
    public GameObject attackMenu;
    public GameObject attackResultMenu;

    // variables for the input fields
    public GameObject whiteAttackInputText;
    public GameObject blackAttackInputText;
    public GameObject redAttackInputText;
    // variables for the attack result menu
    public GameObject numOfCritsOutputText;
    public GameObject numOfHitsOutputText;
    public GameObject numOfSurgesOutputText;
    public GameObject numOfBlanksOutputText;
    public GameObject critColorText;
    public GameObject hitColorText;
    public GameObject surgeColorText;
    public GameObject blankColorText;

    // variables to hold the input values
    private int numOfWhiteAttackDice;
    private int numOfBlackAttackDice;
    private int numOfRedAttackDice;

    // arrays to hold the die pools
    private int[] whiteAttackDiePool = new int[8];
    private int[] blackAttackDiePool = new int[8];
    private int[] redAttackDiePool = new int[8];
    private int[] reRollWhiteAttackDiePool = new int[8];
    private int[] reRollBlackAttackDiePool = new int[8];
    private int[] reRollRedAttackDiePool = new int[8];

    // variables to keep track of
    private int totalNumOfHits;
    private int totalNumOfCrits;
    private int totalNumOfSurges;
    private int totalNumOfBlanks;
    
    private int numOfWhiteHits;
    private int numOfWhiteCrits;
    private int numOfWhiteSurges;
    private int numOfWhiteBlanks;
    private int numOfBlackHits;
    private int numOfBlackCrits;
    private int numOfBlackSurges;
    private int numOfBlackBlanks;
    private int numOfRedHits;
    private int numOfRedCrits;
    private int numOfRedSurges;
    private int numOfRedBlanks;

    private int numOfReRollWhiteHits;
    private int numOfReRollWhiteCrits;
    private int numOfReRollWhiteSurges;
    private int numOfReRollWhiteBlanks;
    private int numOfReRollBlackHits;
    private int numOfReRollBlackCrits;
    private int numOfReRollBlackSurges;
    private int numOfReRollBlackBlanks;
    private int numOfReRollRedHits;
    private int numOfReRollRedCrits;
    private int numOfReRollRedSurges;
    private int numOfReRollRedBlanks;

    private bool whiteInCritPool;
    private bool whiteInHitPool;
    private bool whiteInSurgePool;
    private bool whiteInBlankPool;
    private bool blackInCritPool;
    private bool blackInHitPool;
    private bool blackInSurgePool;
    private bool blackInBlankPool;
    private bool redInCritPool;
    private bool redInHitPool;
    private bool redInSurgePool;
    private bool redInBlankPool;

    // variables for setting the color text for dice results
    private string critCText;
    private string hitCText;
    private string surgeCText;
    private string blankCText;
    private char w = 'W';
    private char b = 'B';
    private char r = 'R';

    public void Attack()
    {
        // initialize values
        totalNumOfCrits = totalNumOfHits = totalNumOfSurges = totalNumOfBlanks = 0;
        numOfWhiteAttackDice = numOfBlackAttackDice = numOfRedAttackDice = 0;
        Array.Clear(whiteAttackDiePool, 0, 8);
        Array.Clear(blackAttackDiePool, 0, 8);
        Array.Clear(redAttackDiePool, 0, 8);
        Array.Clear(reRollWhiteAttackDiePool, 0, 8);
        Array.Clear(reRollBlackAttackDiePool, 0, 8);
        Array.Clear(reRollRedAttackDiePool, 0, 8);
        whiteInCritPool = whiteInHitPool = whiteInSurgePool = whiteInBlankPool = false;
        blackInCritPool = blackInHitPool = blackInSurgePool = blackInBlankPool = false;
        redInCritPool = redInHitPool = redInSurgePool = redInBlankPool = false;

        // assign values from the input fields
        int.TryParse(whiteAttackInputText.GetComponent<Text>().text, out numOfWhiteAttackDice);
        int.TryParse(blackAttackInputText.GetComponent<Text>().text, out numOfBlackAttackDice);
        int.TryParse(redAttackInputText.GetComponent<Text>().text, out numOfRedAttackDice);

        // don't roll dice if there were none selected
        if (numOfWhiteAttackDice > 0 || numOfBlackAttackDice > 0 || numOfRedAttackDice > 0)
        {
            if(numOfWhiteAttackDice > 0)
            {
                // roll white dice
                RollDice(whiteAttackDiePool, numOfWhiteAttackDice);

                // tally white dice
                numOfWhiteCrits = whiteAttackDiePool[7];
                numOfWhiteHits = whiteAttackDiePool[6];
                numOfWhiteSurges = whiteAttackDiePool[5];
                numOfWhiteBlanks = whiteAttackDiePool[4] + whiteAttackDiePool[3] + whiteAttackDiePool[2] + whiteAttackDiePool[1] + whiteAttackDiePool[0];

                // total results
                if(numOfWhiteCrits > 0)
                {
                    totalNumOfCrits += numOfWhiteCrits;
                    whiteInCritPool = true;
                }
                if(numOfWhiteHits > 0)
                {
                    totalNumOfHits += numOfWhiteHits;
                    whiteInHitPool = true;
                }
                if(numOfWhiteSurges > 0)
                {
                    totalNumOfSurges += numOfWhiteSurges;
                    whiteInSurgePool = true;
                }
                if(numOfWhiteBlanks > 0)
                {
                    totalNumOfBlanks += numOfWhiteBlanks;
                    whiteInBlankPool = false;
                }
            }

            if(numOfBlackAttackDice > 0)
            {
                RollDice(blackAttackDiePool, numOfBlackAttackDice);

                // tally black dice
                numOfBlackCrits = blackAttackDiePool[7];
                numOfBlackHits = blackAttackDiePool[6] + blackAttackDiePool[5] + blackAttackDiePool[4];
                numOfBlackSurges = blackAttackDiePool[3];
                numOfBlackBlanks = blackAttackDiePool[2] + blackAttackDiePool[1] + blackAttackDiePool[0];

                // total results
                if(numOfBlackCrits > 0)
                {
                    totalNumOfCrits += numOfBlackCrits;
                    blackInCritPool = true;
                }
                if(numOfBlackHits > 0)
                {
                    totalNumOfHits += numOfBlackHits;
                    blackInHitPool = true;
                }
                if (numOfBlackSurges > 0)
                {
                    totalNumOfSurges += numOfBlackSurges;
                    blackInSurgePool = true;
                }
                if (numOfBlackBlanks > 0)
                {
                    totalNumOfBlanks += numOfBlackBlanks;
                    blackInBlankPool = true;
                }
            }

            if(numOfRedAttackDice > 0)
            {
                RollDice(redAttackDiePool, numOfRedAttackDice);

                // tally red dice
                numOfRedCrits = redAttackDiePool[7];
                numOfRedHits = redAttackDiePool[6] + redAttackDiePool[5] + redAttackDiePool[4] + redAttackDiePool[3] + redAttackDiePool[2];
                numOfRedSurges = redAttackDiePool[1];
                numOfRedBlanks = redAttackDiePool[0];

                // total results
                if(numOfRedCrits > 0)
                {
                    totalNumOfCrits += numOfRedCrits;
                    redInCritPool = true;
                }
                if (numOfRedHits > 0)
                {
                    totalNumOfHits += numOfRedHits;
                    redInHitPool = true;
                }
                if (numOfRedSurges > 0)
                {
                    totalNumOfSurges += numOfRedSurges;
                    redInSurgePool = true;
                }
                if (numOfRedBlanks > 0)
                {
                    totalNumOfBlanks += numOfRedBlanks;
                    redInBlankPool = true;
                }
            }

            // print results
            numOfCritsOutputText.GetComponent<Text>().text = "" + totalNumOfCrits;
            numOfHitsOutputText.GetComponent<Text>().text = "" + totalNumOfHits;
            numOfSurgesOutputText.GetComponent<Text>().text = "" + totalNumOfSurges;
            numOfBlanksOutputText.GetComponent<Text>().text = "" + totalNumOfBlanks;
            SetColorText();

            // change menus
            attackResultMenu.SetActive(true);
            attackMenu.SetActive(false);
        }
    }

    // rolls to the dicePool array a number of times equal to num
    public void RollDice(int[] dicePool, int num)
    {
        for(int i = 0; i > num; i++)
        {
            dicePool[UnityEngine.Random.Range(0, 8)]++;
        }
    }

    public void ReRollHit()
    {
        // re-roll a hit dice, presumably to get a crit

    }

    public void ReRollSurge()
    {
        // re-roll a surge dice

    }

    public void ReRollBlank()
    {
        // re-roll a blank dice

    }

    public void PrintReRoll()
    {
        // print re-rolls

    }

    // Sets color text values over the dice results to let the user know what colors are in each category
    public void SetColorText()
    {
        // clear text as re-rolling may remove a color entirely from a category
        critCText = "";
        hitCText = "";
        surgeCText = "";
        blankCText = "";

        // check crit category for different colors
        if(whiteInCritPool == true){ critCText += w; }
        if(blackInCritPool == true){ critCText += b; }
        if (redInCritPool == true){ critCText += r; }

        // check hit category for different colors
        if (whiteInHitPool == true) { hitCText += w; }
        if (blackInHitPool == true) { hitCText += b; }
        if (redInHitPool == true) { hitCText += r; }

        // check surge category for different colors
        if(whiteInSurgePool == true) { surgeCText += w; }
        if(blackInSurgePool == true) { surgeCText += b; }
        if(redInSurgePool == true) { surgeCText += r; }

        // check blank category for different colors
        if(whiteInBlankPool == true) { blankCText += w; }
        if (blackInBlankPool == true) { blankCText += b; }
        if (redInBlankPool == true) { blankCText += r; }

        // allocate values to the text
        critColorText.GetComponent<Text>().text = critCText;
        hitColorText.GetComponent<Text>().text = hitCText;
        surgeColorText.GetComponent<Text>().text = surgeCText;
        blankColorText.GetComponent<Text>().text = blankCText;
    }
}
