using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Line
{
    public bool isDiagonal;
    public Tile[] tiles = new Tile[3];

    public Line(Tile a, Tile b, Tile c, bool isDiag)
    {
        tiles[0] = a;
        tiles[1] = b;
        tiles[2] = c;
        isDiagonal = isDiag;
    }

    public bool IsComplete()
    {
        List<int> UnClaimed = GetUnClaimedTiles();
        if (UnClaimed.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool ContainsEnemy()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].IsPlayer)
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsAlly()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (!tiles[i].IsPlayer)
            {
                return true;
            }
        }
        return false;
    }

    public bool PlayerWon()
    {
        if (tiles[0].IsPlayer && tiles[1].IsPlayer && tiles[2].IsPlayer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsStaleMate()
    {
        if (IsComplete())
        {
            if (tiles[0].IsPlayer && tiles[1].IsPlayer && tiles[2].IsPlayer)
            {
                return false;
            }
            else if (!tiles[0].IsPlayer && !tiles[1].IsPlayer && !tiles[2].IsPlayer)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        else
        {
            return false;
        }
    }

    public List<int> GetUnClaimedTiles()
    {
        List<int> UnClaimed = new List<int>();
        for (int i = 0; i < tiles.Length; i++)
        {
            if (!tiles[i].IsClaimed)
            {
                UnClaimed.Add(i);
            }
        }
        return UnClaimed;
    }

    public bool NeedsToBeBlocked()
    {
        int Count = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].IsClaimed && tiles[i].IsPlayer)
            {
                Count++;
            }
        }

        if (Count >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ReadyToWin()
    {
        int Count = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].IsClaimed && !tiles[i].IsPlayer)
            {
                Count++;
            }
        }

        if (Count >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class GameManager : MonoBehaviour
{
    public Tile[] Tiles = new Tile[9];
    public Line[] Lines = new Line[8];

    public bool IsPlayerTurn = true;
    public int Winner = 0;
    public bool GameOver = false;

    void Start()
    {
        Lines[0] = new Line(Tiles[0], Tiles[1], Tiles[2], false);
        Lines[1] = new Line(Tiles[3], Tiles[4], Tiles[5], false);
        Lines[2] = new Line(Tiles[6], Tiles[7], Tiles[8], false);
        Lines[3] = new Line(Tiles[0], Tiles[3], Tiles[6], false);
        Lines[4] = new Line(Tiles[1], Tiles[4], Tiles[7], false);
        Lines[5] = new Line(Tiles[2], Tiles[5], Tiles[8], false);
        Lines[6] = new Line(Tiles[0], Tiles[4], Tiles[8], true);
        Lines[7] = new Line(Tiles[2], Tiles[4], Tiles[6], true);

        Restart();
    }

    void processTurn()
    {
        ComputerMove();
        Winner = CheckWin();
        if (Winner != 0)
        {
            EndGame();
        }
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
        else if (Winner == 3)
        {
            Debug.Log("This round ended in a Tie!");
        }

        Restart();
    }

    void Restart()
    {
        Winner = 0;
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i].Reset();
            Tiles[i].Clicked.AddListener(processTurn);
            Tiles[i].UpdateColor();
        }
    }

    void ComputerMove()
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            if (Lines[i].ReadyToWin())
            {
                List<int> nextMove = Lines[i].GetUnClaimedTiles();
                if (nextMove.Count > 0)
                {
                    Lines[i].tiles[nextMove[0]].SetXorO(false);
                    return;
                }
            }
        }

        for (int i = 0; i < Lines.Length; i++)
        {
            if (Lines[i].NeedsToBeBlocked())
            {
                List<int> nextMove = Lines[i].GetUnClaimedTiles();
                if (nextMove.Count > 0)
                {
                    Lines[i].tiles[nextMove[0]].SetXorO(false);
                    return;
                }
            }
        }

        for (int i = 0; i < Lines.Length; i++)
        {
            if (!Lines[i].IsComplete())
            {
                if (!Lines[i].ContainsEnemy())
                {
                    List<int> nextMove = Lines[i].GetUnClaimedTiles();
                    if (nextMove.Count > 0)
                    {
                        Lines[i].tiles[nextMove[0]].SetXorO(false);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i].UpdateColor();
        }
    }

    int CheckWin()
    {
        if (CheckStalemate())
        {
            return 3;
        }

        for (int i = 0; i < Lines.Length; i++)
        {
            if (Lines[i].IsComplete())
            {
                if (Lines[i].PlayerWon())
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
        return 0;
    }

    bool CheckStalemate()
    {
        int StaleMateCount = 0;
        for (int i = 0; i < Lines.Length; i++)
        {
            if (Lines[i].IsStaleMate())
            {
                StaleMateCount++;
            }
        }
        if (StaleMateCount >= Lines.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
