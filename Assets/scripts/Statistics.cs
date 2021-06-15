using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statistics 
{
    static int [] ScoreTable = new int [12];

    public static int[] Scores() 
    {
        return  ScoreTable;
    }

    public static void Scoring(PlayerName name, Difficulty diffculty1, Difficulty diffculty2)
    {
        switch (diffculty1)
        {
            case Difficulty.Easy:
                switch (diffculty2)
                {
                    case Difficulty.Easy:
                        if (name == PlayerName.Player1)
                        {
                            ScoreTable[0] = ScoreTable[0] + 1;
                        }

                        else
                        {
                            ScoreTable[1] = ScoreTable[1] + 1;
                        }
                        break;

                    case Difficulty.Medium:
                        if (name == PlayerName.Player1)
                        {
                            ScoreTable[2] = ScoreTable[2] + 1;
                        }

                        else
                        {
                            ScoreTable[3] = ScoreTable[3] + 1;
                        }
                        break;

                    case Difficulty.Hard:
                        if (name == PlayerName.Player1)
                        {
                            ScoreTable[4] = ScoreTable[4] + 1;
                        }

                        else
                        {
                            ScoreTable[5] = ScoreTable[5] + 1;
                        }
                        break;
                }
                break;

            case Difficulty.Medium:
                switch (diffculty2)
                {
                    case Difficulty.Medium:
                        if (name == PlayerName.Player1)
                        {
                            ScoreTable[6] = ScoreTable[6] + 1;
                        }

                        else
                        {
                            ScoreTable[7] = ScoreTable[7] + 1;
                        }
                        break;

                    case Difficulty.Hard:
                        if (name == PlayerName.Player1)
                        {
                            ScoreTable[8] = ScoreTable[8] + 1;
                        }

                        else
                        {
                            ScoreTable[9] = ScoreTable[9] + 1;
                        }
                        break;
                }
                break;

            case Difficulty.Hard:

                switch (diffculty2)
                {
                    case Difficulty.Hard:
                        if (name == PlayerName.Player1)
                        {
                            ScoreTable[10] = ScoreTable[10] + 1;
                        }

                        else
                        {
                            ScoreTable[11] = ScoreTable[11] + 1;
                        }
                        break;
                }
                break;
        }
    }
}
