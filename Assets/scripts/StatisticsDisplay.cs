using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the statistics
/// </summary>
public class StatisticsDisplay : MonoBehaviour
{
    [SerializeField]
    Text easyEasyPlayer1Wins;
    [SerializeField]
    Text easyEasyPlayer2Wins;

    [SerializeField]
    Text mediumMediumPlayer1Wins;
    [SerializeField]
    Text mediumMediumPlayer2Wins;

    [SerializeField]
    Text hardHardPlayer1Wins;
    [SerializeField]
    Text hardHardPlayer2Wins;

    [SerializeField]
    Text easyMediumPlayer1Wins;
    [SerializeField]
    Text easyMediumPlayer2Wins;

    [SerializeField]
    Text easyHardPlayer1Wins;
    [SerializeField]
    Text easyHardPlayer2Wins;

    [SerializeField]
    Text mediumHardPlayer1Wins;
    [SerializeField]
    Text mediumHardPlayer2Wins;

    /// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        int[] scores = Statistics.Scores();
        easyEasyPlayer1Wins.text = "" +  scores[0];
        easyEasyPlayer2Wins.text = "" + scores[1];
        easyMediumPlayer1Wins.text = "" + scores[2];
        easyMediumPlayer2Wins.text = "" + scores[3];
        easyHardPlayer1Wins.text = "" + scores[4];
        easyHardPlayer2Wins.text = "" + scores[5];
        mediumMediumPlayer1Wins.text = "" + scores[6];
        mediumMediumPlayer2Wins.text = "" + scores[7];
        mediumHardPlayer1Wins.text = "" + scores[8];
        mediumHardPlayer2Wins.text = "" + scores[9];
        hardHardPlayer1Wins.text = "" + scores[10];
        hardHardPlayer2Wins.text = "" + scores[11];
    }

  
}
