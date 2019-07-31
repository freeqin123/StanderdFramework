using UnityEngine;
using System.Collections;
using LYFrameWork;

public class UIButtonBase : MonoBase {

	protected bool isHover = false;

	public event OnClickHandler click;
	public event VoidDelegate hover;
	public event VoidDelegate hoverEnd;
    public event VoidDelegate pressDown;
    public event VoidDelegate pressUp;

    public void addClickListener(OnClickHandler action)
	{
		click += action;
	}

	public void addHoverListener(VoidDelegate action)
	{
		hover += action;
	}

    public void addPressDownListener(VoidDelegate action)
    {
        pressDown += action;
    }

    public void addPressUpListener(VoidDelegate action)
    {
        pressUp += action;
    }

    public void addHoverEndListender(VoidDelegate action)
	{
		hoverEnd += action;
	}

    public virtual void downTrigger(ButtonNotification note)
    {
        if (click != null)
            click(note);
    }

    public void PressDownTrigger()
    {
        if (pressDown != null)
            pressDown();
    }

    public void PressUpTrigger()
    {
        if (pressUp != null)
            pressUp();
    }

    public void hoverTrigger()
	{
		if (hover != null)
			hover ();
	}

	public virtual void clickTrigger(ButtonNotification note)
	{
		if (click != null)
			click (note);
	}

	public void hoverEndTrigger()
	{
		if (hoverEnd != null)
			hoverEnd ();
	}
}
