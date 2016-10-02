using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlipColorOnHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
{
    public Sprite OriginalSprite;
    public Sprite FlippedSprite;
    public Color OriginalTextColor;
    public Color FlippedTextColor;

    private List<TextMeshProUGUI> _texts;
    private Image _icon;

    private bool _highlighted;
    private bool _selected;

    private void Start()
    {
        _texts = GetComponentsInChildren<TextMeshProUGUI>().ToList();
        try
        {
            _icon = GetComponentsInChildren<Image>()[1];
        }
        catch (IndexOutOfRangeException)
        {
            _icon = null;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _highlighted = true;
        ApplyFlipped();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _highlighted = false;
        if (_selected)
        {
            ApplyFlipped();
        }
        else
        {
            ApplyOriginal();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ApplyOriginal();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_highlighted || _selected)
        {
            ApplyFlipped();
        }
        else
        {
            ApplyOriginal();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _selected = true;
        ApplyFlipped();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _selected = false;
        if (!_highlighted) ApplyOriginal();
    }

    private void ApplyFlipped()
    {
        if (_icon != null) _icon.sprite = FlippedSprite;
        if (_texts != null)
            for (int i = 0; i < _texts.Count; ++i)
            {
                _texts[i].color = FlippedTextColor;
            }
    }

    private void ApplyOriginal()
    {
        if (_icon != null) _icon.sprite = OriginalSprite;
        if (_texts != null)
            for (int i = 0; i < _texts.Count; ++i)
            {
                _texts[i].color = OriginalTextColor;
            }
    }
}
