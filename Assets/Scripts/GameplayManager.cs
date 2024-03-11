using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manage game sate, scores and respawns during gamelplay. Only one GameplayManager should be in the scene
/// reference the script to access gameplay objects like UI
/// </summary>

public class GameplayManager : MonoBehaviour
{

    #region Variables
    //Store gameplay states
    
    public enum State { Intro, Gameplay, Paused, Ending}
    [Header("Game Settings")]
    public State gameState = State.Intro;


    [Header ("Player Settings")]
    //store player object references
    public GameObject player1, player2;
    //store score data
    public int player1Score, player2Score;

    //store respawns postions
    public Transform[] respawnPositions;

    [Header("UI Settings")]
    //store UI refernces
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    public TMP_Text timerText;
    public TMP_Text messageText;

    

    public int maxScore;
    public float gameDuration;

    //store a timer
   
    //public Timer gameTimer; //need Timer.cs


    //store player data
    //
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Score update function - increase players score, check if game is over and update UI
    public void UpdateScore(int playerNumber)
    {
        //increase the score on the player given. if playerNumber is 1, increase player1 score, otherwise increase player 2
        //check if either players score is more than the max score. If so, call End game function
        //update tthe score UI
    }
    //respawns - find player to respawn, deactivate controls, decrease live*, disable player, moveto spawn point
    //reset data, reactivate palyer, check for end of game if relevant

    //Display the timer

    //run intro sequence

    //begin gameplay sequence - start the time, activate the players, update UI

    //End game - freeze players, tally scores, display resulta or move to next scene
}
