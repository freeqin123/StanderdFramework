using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using LYFrameWork;
using System.Collections.Generic;

public delegate void OnClickHandler(ButtonNotification note);

public class UIButtonAdvance : UIButtonBase
{
    protected Image m_image;
    protected virtual void Awake()
    {
        //if (!GetComponent<ButtonSound>())
        //{
        //    ButtonSound buttonSound = gameObject.AddComponent<ButtonSound>();
        //    buttonSound.hoverSoundType = E2DSoundEffect.normalHover;
        //    buttonSound.clickSoundType = E2DSoundEffect.normalClick;
        //}
    }
    public virtual void Enable()
    {

    }

    public virtual void Disable()
    {

    }
}
namespace LYFrameWork
{
    public delegate void VoidDelegate();
    public enum UIInteractableType
    {
        normalSprite,
        hoverSprite,
        selectSprite,
        disableSprite
    }
    public interface IInteractable
    {
        void FreezedInteractable();
        void UnFreezedInteractable();
        KeyValuePair<Sprite, Color> GetSprite(UIInteractableType uIInteractableType);
        void SetSprite(UIInteractableType uIInteractableType, KeyValuePair<Sprite, Color> spriteInfo);

        void SetUIInteractableType(UIInteractableType uIInteractableType);
    }
}
