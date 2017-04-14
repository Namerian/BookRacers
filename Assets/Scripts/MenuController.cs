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
    private List<BaseMenu> _menuList;

    //===============================================================================
    //
    //===============================================================================

    private BaseMenu _currentMenu;
    private Dictionary<EMenuScreen, BaseMenu> _menuDictionary = new Dictionary<EMenuScreen, BaseMenu>();

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
        _menuDictionary.Clear();
        foreach (BaseMenu menu in _menuList)
        {
            _menuDictionary.Add(menu.MenuType, menu);
        }

        SwitchMenu(EMenuScreen.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            _menuDictionary.Clear();
            foreach (BaseMenu menu in _menuList)
            {
                _menuDictionary.Add(menu.MenuType, menu);
            }

            if (_currentMenu != null)
                _currentMenu.HideMenu();

            _currentMenu = _menuDictionary[_visibleMenu];

            if (_currentMenu != null)
                _currentMenu.ShowMenu();
        }
    }

    //===============================================================================
    //
    //===============================================================================

    public void SwitchMenu(EMenuScreen newMenuType)
    {
        BaseMenu newMenu = _menuDictionary[newMenuType];

        if (newMenu != null)
        {
            if (_currentMenu != null)
                _currentMenu.ExitMenu();

            _currentMenu = newMenu;

            _currentMenu.EnterMenu();
        }
    }
}
