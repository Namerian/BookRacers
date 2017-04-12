using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }

    //===============================================================================
    //
    //===============================================================================

    [SerializeField]
    private EMenuScreen _visibleMenu;

    [Header("")]

    [SerializeField]
    private BaseMenu _mainMenu;

    [SerializeField]
    private BaseMenu _pilotMenu;

    [SerializeField]
    private BaseMenu _bookMenu;

    //===============================================================================
    //
    //===============================================================================

    private BaseMenu _currentMenu;

    //===============================================================================
    //
    //===============================================================================

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        SwitchMenu(EMenuScreen.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            SwitchMenu(_visibleMenu, true);
    }

    //===============================================================================
    //
    //===============================================================================

    public void SwitchMenu(EMenuScreen newMenu, bool editor = false)
    {
        //Debug.Log("SwitchMenu called: newMenu=" + newMenu + "; editor=" + editor);

        BaseMenu newCurrentMenu = null;

        switch (newMenu)
        {
            case EMenuScreen.MainMenu:
                newCurrentMenu = _mainMenu;
                break;
            case EMenuScreen.PilotMenu:
                newCurrentMenu = _pilotMenu;
                break;
            case EMenuScreen.BookMenu:
                newCurrentMenu = _bookMenu;
                break;
        }

        if (newCurrentMenu != null)
        {
            //Debug.Log("SwitchMenu: newMenu != null");

            /*if (newCurrentMenu == _currentMenu)
                Debug.Log("newCurrentMenu == _currentMenu");*/

            if (_currentMenu != null)
                _currentMenu.ExitMenu(editor);

            _currentMenu = newCurrentMenu;

            _currentMenu.EnterMenu(editor);
        }
        else if (_currentMenu != null)
        {
            _currentMenu.ExitMenu(editor);
            _currentMenu = null;
        }
    }
}
