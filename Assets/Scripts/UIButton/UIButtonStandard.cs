using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using LYFrameWork;
//using DG.Tweening;

public class UIButtonStandard : UIButtonAdvance, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IInteractable,IProcessEvent
{
    ushort[] m_msgIDs;

    public Sprite m_normalSprite;

    public Sprite NormalSrpite
    {
        set
        {
            m_normalSprite = value;
            m_image.sprite = m_normalSprite;
        }
    }

    public Sprite m_hoverSprite;

    public Sprite m_pressSprite;

    public Sprite m_freezeSprite;

    public Color m_normalColor;

    public Color m_hoverColor;

    public Color m_pressColor;

    public Color m_freezeColor;

    //public bool isAnimScale = false;

    public bool isEnableSelectMode = false;

    public bool isSelect = false;

    public bool isInteractable = true;

    void OnEnable()
    {
        isHover = false;
        if (isInteractable)
        {
            pointerExit();
        }
        else
        {
            m_image.sprite = m_freezeSprite;
            if (m_freezeColor != Color.clear)
                m_image.color = m_freezeColor;
        }
    }

    protected virtual void pointerClick()
    {
        clickTrigger(new ButtonNotification(null, this));
        if (!isInteractable)
        {
            return;
        }
        if (m_hoverColor != Color.clear)
        {
            m_image.color = m_hoverColor;
        }
        if (isEnableSelectMode)
        {
            isSelect = true;
            NoteCenter.Instance.SendMsg((ushort)UIBaseEvent.unselectOther, name);
        }

        else
        {
            m_image.sprite = m_hoverSprite;
        }
    }

    protected virtual void pointerExit()
    {
        if (!isSelect)
            m_image.sprite = m_normalSprite;
        else
            m_image.sprite = m_pressSprite;
        if (m_normalColor != Color.clear)
            m_image.color = m_normalColor;
        //if (isAnimScale)
        //{
        //    DOTween.Kill(transform);
        //    transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        //}
    }

    protected virtual void pointerEnter()
    {
        isHover = true;
        if (m_hoverColor != Color.clear)
            m_image.color = m_hoverColor;
        if (!isSelect)
            m_image.sprite = m_hoverSprite;
        //if (isAnimScale)
        //{
        //    DOTween.Kill(transform);
        //    transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.15f);
        //}
        hoverTrigger();
    }

    protected virtual void pointerDown()
    {
        if (m_pressColor != Color.clear)
            m_image.color = m_pressColor;
        if (!isSelect)
            m_image.sprite = m_pressSprite;
        PressDownTrigger();
    }

    protected override void Awake()
    {
        base.Awake();
        m_image = GetComponent<Image>();
        m_msgIDs = new ushort[] {
            (ushort)UIBaseEvent.lockOther,
            (ushort)UIBaseEvent.unlockOther,
            (ushort)UIBaseEvent.unselectOther
        };
        NoteCenter.Instance.Regist(m_msgIDs, this);
    }

    void OnDestroy()
    {
        NoteCenter.Instance.UnRegist(m_msgIDs, this);
    }

    public KeyValuePair<Sprite, Color> GetSprite(UIInteractableType uIInteractableType)
    {
        switch (uIInteractableType)
        {
            case UIInteractableType.normalSprite:
                return new KeyValuePair<Sprite, Color>(m_normalSprite, m_normalColor);
                break;
            case UIInteractableType.hoverSprite:
                return new KeyValuePair<Sprite, Color>(m_hoverSprite, m_hoverColor);
                break;
            case UIInteractableType.selectSprite:
                return new KeyValuePair<Sprite, Color>(m_pressSprite, m_pressColor);
                break;
            case UIInteractableType.disableSprite:
                return new KeyValuePair<Sprite, Color>(m_freezeSprite, m_freezeColor);
                break;
            default:
                return new KeyValuePair<Sprite, Color>(null, Color.clear);
                break;
        }
    }
    public void SetSprite(UIInteractableType uIInteractableType, KeyValuePair<Sprite, Color> _sprite)
    {
        switch (uIInteractableType)
        {
            case UIInteractableType.normalSprite:
                m_normalSprite = _sprite.Key;
                m_normalColor = _sprite.Value;
                break;
            case UIInteractableType.hoverSprite:
                m_hoverSprite = _sprite.Key;
                m_hoverColor = _sprite.Value;
                break;
            case UIInteractableType.selectSprite:
                m_pressSprite = _sprite.Key;
                m_pressColor = _sprite.Value;
                break;
            case UIInteractableType.disableSprite:
                m_freezeSprite = _sprite.Key;
                m_freezeColor = _sprite.Value;
                break;
            default:
                break;
        }
    }

    void Unselect()
    {
        isSelect = false;
        pointerExit();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable)
            return;
        isHover = false;
        hoverEndTrigger();
        if (isLock)
            return;
        pointerExit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable)
            return;
        if (isLock)
            return;
        pointerEnter();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isInteractable)
            return;
        if (isLock || eventData.button != PointerEventData.InputButton.Left)
            return;
        pointerDown();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable)
            return;
        if (isLock || eventData.button != PointerEventData.InputButton.Left)
            return;
        pointerClick();
    }

    public override void Enable()
    {
        isLock = false;
        if (isHover)
        {
            pointerEnter();
        }
    }

    public override void Disable()
    {
        isLock = true;
        if (isHover)
        {
            pointerExit();
        }
    }


    public void FreezedInteractable()
    {
        isInteractable = false;
        if (!m_image)
            m_image = GetComponent<Image>();
            m_image.sprite = m_freezeSprite;
        if (m_freezeColor != Color.clear)
            m_image.color = m_freezeColor;
        //if (isAnimScale)
        //{
        //    DOTween.Kill(transform);
        //    transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        //}
    }

    public void UnFreezedInteractable()
    {
        isInteractable = true;
        pointerExit();
    }

    public void SetUIInteractableType(UIInteractableType uIInteractableType)
    {
        switch (uIInteractableType)
        {
            case UIInteractableType.normalSprite:
                m_image.sprite = m_normalSprite;
                isSelect = false;
                isInteractable = true;
                break;
            case UIInteractableType.hoverSprite:
                m_image.sprite = m_hoverSprite;
                isInteractable = true;
                break;
            case UIInteractableType.selectSprite:
                m_image.sprite = m_pressSprite;
                isSelect = true;
                isInteractable = true;
                break;
            case UIInteractableType.disableSprite:
                m_image.sprite = m_freezeSprite;
                isSelect = false;
                isInteractable = false;
                break;
            default:
                break;
        }
    }

    public void ProcessEvent(Notification note)
    {
        if (note.noteID == (ushort)UIBaseEvent.lockOther)
        {
            if ((string)note.data != name)
                Lock();
        }
        else if (note.noteID == (ushort)UIBaseEvent.unlockOther)
        {
            if ((string)note.data != name)
                Unlock();
        }
        else if (note.noteID == (ushort)UIBaseEvent.unselectOther)
        {
            if ((string)note.data != name && isSelect)
                Unselect();
        }
    }
}
