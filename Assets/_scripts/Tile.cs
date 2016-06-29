using UnityEngine;
using UnityEngine.Events;
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
    public UnityEvent Clicked;
    public bool IsClaimed = false;
    public bool IsPlayer = false;
    #endregion

    #region Private Vars
    int m_xLoc = 0;
    int m_yLoc = 0;
    #endregion

    void Start()
    {
        if(Clicked == null)
        {
            Clicked = new UnityEvent();
        }

        IsClaimed = false;
        if (BackgroundImage != null)
        {
            BackgroundImage.color = BackgroundColor;
        }
    }

    public void Reset()
    {
        Clicked = null;
        Clicked = new UnityEvent();
        IsClaimed = false;
        IsPlayer = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!IsClaimed)
        {
            SetXorO(true);
            Clicked.Invoke();
        }
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
                UpdateColor();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (BackgroundImage != null)
        {
            UpdateColor();
        }
    }

    public void UpdateColor()
    {
        if (!IsClaimed)
        {
            BackgroundImage.color = BackgroundColor;
        }
        else
        {
            if(IsPlayer)
            {
                BackgroundImage.color = PlayerColor;
            }
            else
            {
                BackgroundImage.color = ComputerColor;
            }
        }
    }

    public void SetXorO(bool isX)
    {
        IsClaimed = true;
        IsPlayer = isX;
        if(DeBugging)
        {
            Debug.Log(string.Format("Tile is now {0}", IsPlayer ? "X" : "O"));
        }
        UpdateColor();
    }
}
