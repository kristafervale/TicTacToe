using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public bool GameOver = false;
    public Tile[] GamePieces = new Tile[9];
    public bool IsPlayerTurn = true;
    public List<int>[] WinLines = new List<int>[8];
    public int[] ComputerOdds = new int[9];
    public int Winner = 0;

    void Start()
    {
        Restart();
    }

    void processTurn()
    {
        CheckWin();
        if (GameOver)
            EndGame();

        CheckStalemate();
        if (GameOver)
            EndGame();

        ComputerMove();
    }

    void EndGame()
    {
        if (Winner == 1)
        {
            Debug.Log("The Player Won this Round!");
        }
        else if (Winner == 2)
        {
            Debug.Log("The Computer Won this Round!");
        }
        else
        {
            Debug.Log("This round ended in a Tie!");
        }

        Restart();
    }

    void Restart()
    {
        WinLines[0] = new List<int> { 0, 1, 2 };
        WinLines[1] = new List<int> { 3, 4, 5 };
        WinLines[2] = new List<int> { 6, 7, 8 };
        WinLines[3] = new List<int> { 0, 3, 6 };
        WinLines[4] = new List<int> { 1, 4, 7 };
        WinLines[5] = new List<int> { 2, 5, 8 };
        WinLines[6] = new List<int> { 0, 4, 8 };
        WinLines[7] = new List<int> { 2, 4, 6 };

        for (int i = 0; i < GamePieces.Length; i++)
        {
            ComputerOdds[i] = 0;
        }

        for (int i = 0; i < GamePieces.Length; i++)
        {
            GamePieces[i].Reset();
        }
    }

    void ComputerMove()
    {
        SetOdds();
    }

    /// <summary>
    /// Get a list of all the lines that are only one move from closing.
    /// If any of those lines have two computer squares WIN THE GAME.
    /// If any of those lines have two player squares BLOCK THE PLAYER.
    /// If none of the closable lines are computer controlled or player controlled ignore that line.
    /// </summary>
    void FindClosableLines()
    {

    }

    /// <summary>
    /// Find all the lines that have at least ONE square claimed.
    /// If the line is started by the Computer, consider that line as a next move.
    /// </summary>
    void FindStartedLines()
    {

    }

    /// <summary>
    /// Once all the lines have been checked find the optimum square to claim.
    /// This is definied by the open square that either:
    /// A. Wins the game +10
    /// B. Blocks the Player from winning the game +5
    /// C. Gives the computer 2 squares in a line +2
    /// D. Is an open square with the most open squares connected to it +1
    /// </summary>
    void FindOptimumSquare()
    {

    }

    void SetOdds()
    {
        for (int i = 0; i < WinLines.Length; i++)
        {
            int odds = 0;
            for (int x = 0; x < 3; x++)
            {
                if (GamePieces[WinLines[i][x]].IsClaimed)
                {
                    if (GamePieces[WinLines[i][x]].IsPlayers)
                    {
                        odds--;
                    }
                    else
                    {
                        odds++;
                    }
                }
            }

            ComputerOdds[i] = odds;
        }
    }

    int CheckWin()
    {
        for (int x = 0; x < WinLines.Length; x++)
        {
            int a = WinLines[x][0];
            int b = WinLines[x][1];
            int c = WinLines[x][2];

            if (GamePieces[a].IsClaimed && GamePieces[b].IsClaimed && GamePieces[c].IsClaimed)
            {
                if (GamePieces[a].IsPlayers && GamePieces[b].IsPlayers && GamePieces[c].IsPlayers)
                {
                    GameOver = true;
                    return 1;
                }
                else if (!GamePieces[a].IsPlayers && !GamePieces[b].IsPlayers && !GamePieces[c].IsPlayers)
                {
                    GameOver = true;
                    return 2;
                }
            }
        }
        GameOver = false;
        return 0;
    }

    bool CheckStalemate()
    {
        for (int i = 0; i < GamePieces.Length; i++)
        {
            if (!GamePieces[i].IsClaimed)
            {
                GameOver = false;
                return false;
            }
        }
        GameOver = true;
        return true;
    }

}
