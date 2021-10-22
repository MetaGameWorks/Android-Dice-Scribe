using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AttackDice : MonoBehaviour
{
    // variables for the menus
    public GameObject attackMenu;
    public GameObject attackResultMenu;

    // variables for the input fields
    public GameObject whiteAttackInput;
    public GameObject blackAttackInput;
    public GameObject redAttackInput;
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

        // assign values from the input fields


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
                numOfWhiteBlanks = whiteAttackDiePool.Sum() - (numOfWhiteCrits + numOfWhiteHits + numOfWhiteSurges);

                // total results
                totalNumOfCrits += numOfWhiteCrits;
                totalNumOfHits += numOfWhiteHits;
                totalNumOfSurges += numOfWhiteSurges;
                totalNumOfBlanks += numOfWhiteBlanks;
            }

            if(numOfBlackAttackDice > 0)
            {
                RollDice(blackAttackDiePool, numOfBlackAttackDice);
            }

            if(numOfRedAttackDice > 0)
            {
                RollDice(redAttackDiePool, numOfRedAttackDice);
            }
        }
    }

    public void RollDice(int[] dicePool, int num)
    {
        for(int i = 0; i > num; i++)
        {
            dicePool[UnityEngine.Random.Range(0, 8)]++;
        }
    }
}
