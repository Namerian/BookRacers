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

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        SwitchMenu(_visibleMenu);
    }

    //===============================================================================
    //
    //===============================================================================

    public void SwitchMenu(EMenuScreen newMenu)
    {
        BaseMenu newCurrentMenu = null;

        switch (_visibleMenu)
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
            if (newCurrentMenu != _currentMenu)
            {
                if (_currentMenu != null)
                    _currentMenu.ExitMenu();

                _currentMenu = newCurrentMenu;

                _currentMenu.EnterMenu();
            }
            
        }
        else if(_currentMenu != null)
        {
            _currentMenu.ExitMenu();
            _currentMenu = null;
        }
    }
}
