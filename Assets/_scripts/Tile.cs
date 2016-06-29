using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    #region Public Vars
    public Image BackgroundImage;
    public Color BackgroundColor;
    public Color HighlightColor;
    public Color PlayerColor;
    public Color ComputerColor;
    public bool DeBugging = false;
    #endregion

    #region Private Vars
    public bool IsClaimed = false;
    public bool IsPlayers = false;
    int m_xLoc = 0;
    int m_yLoc = 0;
    #endregion

    void Start()
    {
        IsClaimed = false;
        if (BackgroundImage != null)
        {
            BackgroundImage.color = BackgroundColor;
        }
    }

    public void Reset()
    {
        IsClaimed = false;
        IsPlayers = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsClaimed = !IsClaimed;
        if (BackgroundImage != null)
        {
            if (!IsClaimed)
            {
                BackgroundImage.color = BackgroundColor;
            }
            else
            {
                BackgroundImage.color = PlayerColor;
            }
        }
        SetXorO(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (BackgroundImage != null)
        {
            if (!IsClaimed)
            {
                BackgroundImage.color = HighlightColor;
            }
            else
            {
                if (IsPlayers)
                {
                    BackgroundImage.color = PlayerColor;
                }
                else
                {
                    BackgroundImage.color = ComputerColor;
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (BackgroundImage != null)
        {
            if (!IsClaimed)
            {
                BackgroundImage.color = BackgroundColor;
            }
            else
            {
                if (IsPlayers)
                {
                    BackgroundImage.color = PlayerColor;
                }
                else
                {
                    BackgroundImage.color = ComputerColor;
                }
            }
        }
    }

    void SetXorO(bool isX)
    {
        IsPlayers = isX;
        if(DeBugging)
        {
            Debug.Log(string.Format("Tile is now {0}", IsPlayers ? "X" : "O"));
        }
    }
}
