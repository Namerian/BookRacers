using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : BaseMenu
{
    [Header("Right Bar")]

    [SerializeField]
    private Text _moneyText;

    [SerializeField]
    private Button _playButton;

    [Header("Pilot Selection")]

    [SerializeField]
    private Text _pilotNameText;

    [SerializeField]
    private Text _pilotXpText;

    [SerializeField]
    private Button _pilotLeftButton;

    [SerializeField]
    private Button _pilotRightButton;

    [Header("Book Selection")]

    [SerializeField]
    private Text _bookNameText;

    [SerializeField]
    private Text _bookXpText;

    [SerializeField]
    private Button _bookLeftButton;

    [SerializeField]
    private Button _bookRightButton;

    //==================================================================
    //
    //==================================================================

    public override EMenuScreen MenuType { get { return EMenuScreen.MainMenu; } }

    //==================================================================
    //
    //==================================================================

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {

    }

    //==================================================================
    //
    //==================================================================

    protected override void OnEnter()
    {
        _moneyText.text = "Money: " + GameController.Instance.PlayerData.Money;

        UpdatePilotSelection();
        UpdateBookSelection();
        UpdatePlayButton();
    }

    protected override void OnExit()
    {
    }

    //==================================================================
    //
    //==================================================================

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnPilotLeftButtonPressed()
    {
        int index = GameController.Instance.PlayerData.CurrentPilotIndex;

        if (index > 0)
        {
            GameController.Instance.PlayerData.CurrentPilotIndex = index - 1;

            UpdatePilotSelection();
            UpdatePlayButton();
        }
    }

    public void OnPilotRightButtonPressed()
    {
        int index = GameController.Instance.PlayerData.CurrentPilotIndex;

        if (index < GameController.Instance.PilotData.Count - 1)
        {
            GameController.Instance.PlayerData.CurrentPilotIndex = index + 1;

            UpdatePilotSelection();
            UpdatePlayButton();
        }
    }

    public void OnBookLeftButtonPressed()
    {
        int index = GameController.Instance.PlayerData.CurrentBookIndex;

        if (index > 0)
        {
            GameController.Instance.PlayerData.CurrentBookIndex = index - 1;

            UpdateBookSelection();
            UpdatePlayButton();
        }
    }

    public void OnBookRightButtonPressed()
    {
        int index = GameController.Instance.PlayerData.CurrentBookIndex;

        if (index < GameController.Instance.BookData.Count - 1)
        {
            GameController.Instance.PlayerData.CurrentBookIndex = index + 1;

            UpdateBookSelection();
            UpdatePlayButton();
        }
    }

    public void OnPilotMenuButtonPressed()
    {
        MenuController.Instance.SwitchMenu(EMenuScreen.PilotMenu);
    }

    public void OnBookMenuButtonPressed()
    {
        MenuController.Instance.SwitchMenu(EMenuScreen.BookMenu);
    }

    //==================================================================
    //
    //==================================================================

    private void UpdatePilotSelection()
    {
        int index = GameController.Instance.PlayerData.CurrentPilotIndex;
        PilotData pilot = GameController.Instance.PilotData[index];

        _pilotNameText.text = pilot.Name;

        if (pilot.Unlocked)
            _pilotXpText.text = "XP: " + pilot.Experience;
        else
            _pilotXpText.text = "Cost: " + pilot.Cost;

        if (index == 0)
            _pilotLeftButton.interactable = false;
        else
            _pilotLeftButton.interactable = true;

        if (index == GameController.Instance.PilotData.Count - 1)
            _pilotRightButton.interactable = false;
        else
            _pilotRightButton.interactable = true;
    }

    private void UpdateBookSelection()
    {
        int index = GameController.Instance.PlayerData.CurrentBookIndex;
        BookData book = GameController.Instance.BookData[index];

        _bookNameText.text = book.Name;

        if (book.Unlocked)
            _bookXpText.text = "XP: " + book.Experience;
        else
            _bookXpText.text = "Cost: " + book.Cost;

        if (index == 0)
            _bookLeftButton.interactable = false;
        else
            _bookLeftButton.interactable = true;

        if (index == GameController.Instance.BookData.Count - 1)
            _bookRightButton.interactable = false;
        else
            _bookRightButton.interactable = true;
    }

    private void UpdatePlayButton()
    {
        if (GameController.Instance.PilotData[GameController.Instance.PlayerData.CurrentPilotIndex].Unlocked && GameController.Instance.BookData[GameController.Instance.PlayerData.CurrentBookIndex].Unlocked)
            _playButton.interactable = true;
        else
            _playButton.interactable = false;
    }
}
