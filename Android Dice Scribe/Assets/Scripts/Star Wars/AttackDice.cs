using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // variables to keep track of
    private int totalNumOfHits;
    private int totalNumOfCrits;
    private int totalNumOfSurges;
    private int totalNumOfMisses;

    public void RollAttackDice()
    {
        // assign values from the input fields

        // don't roll dice if there were none selected
        if(numOfWhiteAttackDice > 0 || numOfBlackAttackDice > 0 || numOfRedAttackDice > 0)
        {
            // roll for each color
        }
    }
}
