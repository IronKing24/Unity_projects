using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// The HUD
/// </summary>
public class Hud : MonoBehaviour
{
    [SerializeField]
    Text player1Text;
    [SerializeField]
    Text player2Text;
    [SerializeField]
    Text gameOverText;

    Restartgame restaretgame = new Restartgame();

    float seconds;
    int resettimes;
    bool gameover;

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        seconds = 0;

        // register listeners
        EventManager.AddTakeTurnListener(HandleTakeTurnEvent);
        EventManager.AddTurnOverListener(HandleTurnOverEvent);
        EventManager.AddGameOverListener(HandleGameOverEvent);
        EventManager.AddRestartGameListener(Reseting);
        EventManager.AddRestartGameInvoker(this);

        // hide game over text
        gameOverText.enabled = false;

        gameover = false;
    }

    void FixedUpdate()
    {
        if (gameover == true) 
        {
            seconds++;
            if (seconds >= 2 ) 
            {
                restaretgame.Invoke();
                seconds = 0;
                gameover = false;
            }
        }
    }

    /// <summary>
    /// Highlights the text for the current player
    /// </summary>
    /// <param name="player">whose turn it is</param>
    /// <param name="unused">unused</param>
    void HandleTakeTurnEvent(PlayerName player, Configuration unused)
    {
        if (player == PlayerName.Player1)
        {
            player1Text.color = Color.green;
        }
        else
        {
            player2Text.color = Color.green;
        }
    }

    /// <summary>
    /// Unhighlights the text for the player who just
    /// finished their turn
    /// </summary>
    /// <param name="player">who just finished their turn</param>
    /// <param name="boardConfiguration">the new board configuration</param>
    void HandleTurnOverEvent(PlayerName player, Configuration boardConfiguration)
    {
        if (player == PlayerName.Player1)
        {
            player1Text.color = Color.white;
        }
        else
        {
            player2Text.color = Color.white;
        }
    }

    /// <summary>
    /// Displays the game over test
    /// </summary>
    /// <param name="player">who won the game</param>
    void HandleGameOverEvent(PlayerName player ,Difficulty x ,Difficulty y)
    {
        if (player == PlayerName.Player1)
        {
            gameOverText.text = "Player 1 Won!";
        }
        else
        {
            gameOverText.text = "Player 2 Won!";
        }
        gameOverText.enabled = true;
        gameover = true;
    }

    void Reseting()
    {
        if (resettimes <= 500)
        {
            seconds = 0;
            resettimes++;
            gameOverText.enabled = false;
            gameOverText.text = "";

        }
    }

    public void AddrestartgameListeners(UnityAction Listener) 
    {
        restaretgame.AddListener(Listener);
    }
}
