using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    public void EnterMenu()
    {
        ShowMenu();

        OnEnter();
    }

    public void ExitMenu()
    {
        HideMenu();

        OnExit();
    }

    public void ShowMenu()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void HideMenu()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    protected abstract void OnEnter();
    protected abstract void OnExit();

    public abstract EMenuScreen MenuType { get; }
}
