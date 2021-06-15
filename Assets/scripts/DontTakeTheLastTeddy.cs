using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


/// <summary>
/// Game manager
/// </summary>
public class DontTakeTheLastTeddy : MonoBehaviour
{
    Board board;
    Player player1;
    Player player2;

    // events invoked by class
    TakeTurn takeTurnEvent = new TakeTurn();
    GameOver gameOverEvent = new GameOver();

    int DiffcultsetupNumber = 1;
    int roundnumber = 0;

    PlayerName StartingPlayer;
    Difficulty FirstPlayerDifficulty;
    Difficulty SecondPlayerDifficulty;
    bool FirstRound = true;

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        // retrieve board and player references
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
        StartingPlayer = DecideOnPlayer();

        // register as invoker and listener
        EventManager.AddTakeTurnInvoker(this);
        EventManager.AddGameOverInvoker(this);
        EventManager.AddTurnOverListener(HandleTurnOverEvent);
        EventManager.AddGameOverListener(Statistics.Scoring);
        EventManager.AddRestartGameListener(Starting);
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        Starting();
    }

    void Starting() 
    {
        Debug.Log(" STarting");
        if (roundnumber == 100 && DiffcultsetupNumber <= 5)
        {
            roundnumber = 0;
            DiffcultsetupNumber++;
        }

        if (DiffcultsetupNumber > 5)
        {
            SceneManager.LoadScene("statistics");
        }

        else
        {
            Diffcultysetup();
            StartGame(DecideOnPlayer(), FirstPlayerDifficulty, SecondPlayerDifficulty);
            roundnumber++;
        }
    }

    /// <summary>
    /// Adds the given listener for the TakeTurn event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddTakeTurnListener(UnityAction<PlayerName, Configuration> listener)
    {
        takeTurnEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the GameOver event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddGameOverListener(UnityAction<PlayerName,Difficulty,Difficulty> listener)
    {
        gameOverEvent.AddListener(listener);
    }

    /// <summary>
    /// Starts a game with the given player taking the
    /// first turn
    /// </summary>
    /// <param name="firstPlayer">player taking first turn</param>
    /// <param name="player1Difficulty">difficulty for player 1</param>
    /// <param name="player2Difficulty">difficulty for player 2</param>
    void StartGame(PlayerName firstPlayer, Difficulty player1Difficulty, Difficulty player2Difficulty)
    {
        // set player difficulties
        player1.Difficulty = player1Difficulty;
        player2.Difficulty = player2Difficulty;

        // create new board
        board.CreateNewBoard();
        takeTurnEvent.Invoke(firstPlayer,board.Configuration);
    }

    /// <summary>
    /// Handles the TurnOver event by having the 
    /// other player take their turn
    /// </summary>
    /// <param name="player">who finished their turn</param>
    /// <param name="newConfiguration">the new board configuration</param>
    void HandleTurnOverEvent(PlayerName player, Configuration newConfiguration)
    {
        board.Configuration = newConfiguration;

        // check for game over
        if (newConfiguration.Empty)
        {
            // fire event with winner
            if (player == PlayerName.Player1)
            {
                gameOverEvent.Invoke(PlayerName.Player2,player1.Difficulty,player2.Difficulty);
            }
            else
            {
                gameOverEvent.Invoke(PlayerName.Player1, player1.Difficulty, player2.Difficulty);
            }
        }
        else
        {
            // game not over, so give other player a turn
            if (player == PlayerName.Player1)
            {
                takeTurnEvent.Invoke(PlayerName.Player2, newConfiguration);
            }
            else
            {
                takeTurnEvent.Invoke(PlayerName.Player1, newConfiguration);
            }
        }
    }

    /// <summary>
    /// Decides which player should play first at the start 
    /// </summary>
    /// <returns></returns>
    PlayerName DecideOnPlayer()
    {
        if (FirstRound == true)
        {
            int FRP = Random.Range(1, 3);

            switch (FRP)
            {
                case 1:
                    StartingPlayer = PlayerName.Player1;
                    break;
                case 2:
                    StartingPlayer = PlayerName.Player2;
                    break;

            }
            FirstRound = false;
        }

        else if (FirstRound == false && StartingPlayer == PlayerName.Player1)
        {
            StartingPlayer = PlayerName.Player2;
        }

        else if (FirstRound == false && StartingPlayer == PlayerName.Player2)
        {
            StartingPlayer = PlayerName.Player1;
        }

        return StartingPlayer;
    }

    void Diffcultysetup() 
    {
        switch (DiffcultsetupNumber) 
        {
            case 1:
                FirstPlayerDifficulty = Difficulty.Easy;
                SecondPlayerDifficulty = Difficulty.Easy;
                break;

            case 2:
                FirstPlayerDifficulty = Difficulty.Medium;
                SecondPlayerDifficulty = Difficulty.Medium;
                break;

            case 3:
                FirstPlayerDifficulty = Difficulty.Hard;
                SecondPlayerDifficulty = Difficulty.Hard;
                break;

            case 4:
                FirstPlayerDifficulty = Difficulty.Easy;
                SecondPlayerDifficulty = Difficulty.Hard;
                break;

            case 5:
                FirstPlayerDifficulty = Difficulty.Medium;
                SecondPlayerDifficulty = Difficulty.Hard;
                break;
        }
    }
}
