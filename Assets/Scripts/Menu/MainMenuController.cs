using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
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

    // Use this for initialization
    private void Start()
    {
        _moneyText.text = "Money: " + GameController.Instance.PlayerData.Money;

        UpdatePilotSelection();
        UpdateBookSelection();
    }

    // Update is called once per frame
    private void Update()
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
        }
    }

    public void OnPilotRightButtonPressed()
    {
        int index = GameController.Instance.PlayerData.CurrentPilotIndex;

        if (index < GameController.Instance.PilotData.Count - 1)
        {
            GameController.Instance.PlayerData.CurrentPilotIndex = index + 1;

            UpdatePilotSelection();
        }
    }

    public void OnBookLeftButtonPressed()
    {
        int index = GameController.Instance.PlayerData.CurrentBookIndex;

        if (index > 0)
        {
            GameController.Instance.PlayerData.CurrentBookIndex = index - 1;

            UpdateBookSelection();
        }
    }

    public void OnBookRightButtonPressed()
    {
        int index = GameController.Instance.PlayerData.CurrentBookIndex;

        if (index < GameController.Instance.BookData.Count - 1)
        {
            GameController.Instance.PlayerData.CurrentBookIndex = index + 1;

            UpdateBookSelection();
        }
    }

    //==================================================================
    //
    //==================================================================

    private void UpdatePilotSelection()
    {
        int index = GameController.Instance.PlayerData.CurrentPilotIndex;
        PilotData pilot = GameController.Instance.PilotData[index];

        if (_pilotNameText.text != pilot.Name)
        {
            _pilotNameText.text = pilot.Name;
            _pilotXpText.text = "XP: " + pilot.Experience;
        }

        if (index == 0)
        {
            _pilotLeftButton.interactable = false;
        }
        else
        {
            _pilotLeftButton.interactable = true;
        }

        if (index == GameController.Instance.PilotData.Count - 1)
        {
            _pilotRightButton.interactable = false;
        }
        else
        {
            _pilotRightButton.interactable = true;
        }
    }

    private void UpdateBookSelection()
    {
        int index = GameController.Instance.PlayerData.CurrentBookIndex;
        BookData book = GameController.Instance.BookData[index];

        if (_bookNameText.text != book.Name)
        {
            _bookNameText.text = book.Name;
            _bookXpText.text = "XP: " + book.Experience;
        }

        if (index == 0)
        {
            _bookLeftButton.interactable = false;
        }
        else
        {
            _bookLeftButton.interactable = true;
        }

        if (index == GameController.Instance.BookData.Count - 1)
        {
            _bookRightButton.interactable = false;
        }
        else
        {
            _bookRightButton.interactable = true;
        }
    }
}
