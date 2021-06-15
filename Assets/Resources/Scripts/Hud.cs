using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    Text Text;
    float time;
    int secs;
    int mins;
    int ship;
    Text Score;
    Text GOver;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
      Text = GameObject.Find("Canvas").GetComponent<Text>();
      Score = GameObject.Find("Score").GetComponent<Text>();
      GOver = GameObject.Find("Game Over").GetComponent<Text>();
      GOver.enabled = false;
    }


    void Update()
    {
       
        //find ship
        ship = GameObject.FindGameObjectsWithTag("Ship").Length;
        if (ship == 0)
        {
            gameObject.GetComponent<Hud>().enabled = false;
            GOver.enabled = true;
        }

        // time counter
        time = Time.time;
        secs = (int) time % 60;
        mins = (int) time/60;




        //second decible zero
        if (mins<10 && secs<10)
        {
            Text.text = "Time Played  0" + mins.ToString() + ":0" + secs.ToString();
        }

        else if (mins >= 10 && secs < 10)
        {
            Text.text = "Time Played  " + mins.ToString() + ":0" + secs.ToString();
        }

        else if (mins < 10 && secs >= 10)
        {
            Text.text = "Time Played  0" + mins.ToString() + ":" + secs.ToString();
        }

        else if (mins >= 10 && secs >= 10)
        {
            Text.text = "Time Played  " + mins.ToString() + ":" + secs.ToString();
        }
    }

    public void AddPoint()
    {
        score++;
        Score.text = "Score:" + score.ToString();
    }
}


