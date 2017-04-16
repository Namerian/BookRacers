using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceReportMenuController : BaseMenu
{

    public override EMenuScreen MenuType { get { return EMenuScreen.RaceReportMenu; } }

    //==================================================================
    //
    //==================================================================

    [Header("Object References")]

    [SerializeField]
    private Text _timeText;

    [SerializeField]
    private Text _moneyText;

    [SerializeField]
    private Text _pilotXpText;

    [SerializeField]
    private Text _bookXpText;

    //==================================================================
    //
    //==================================================================

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //==================================================================
    //
    //==================================================================

    protected override void OnEnter()
    {
        RaceReport report = GameController.Instance.LastRaceReport;

        if (report == null)
        {
            MenuController.Instance.SwitchMenu(EMenuScreen.MainMenu);
            return;
        }

        string minutes = Mathf.Floor(report.Time / 60).ToString("00");
        string seconds = Mathf.Floor(report.Time % 60).ToString("00");
        string milliseconds = Mathf.Floor((report.Time * 1000) % 1000).ToString("000");

        _timeText.text = "Time: " + minutes + ":" + seconds + ":" + milliseconds;

        _moneyText.text = "Money: +" + report.MoneyGain;
        _pilotXpText.text = GameController.Instance.PilotData[GameController.Instance.PlayerData.CurrentPilotIndex].Name + ": +" + report.PilotXpGain + " Xp";
        _bookXpText.text = GameController.Instance.BookData[GameController.Instance.PlayerData.CurrentBookIndex].Name + ": +" + report.BookXpGain + "Xp";

        GameController.Instance.SaveData();
    }

    protected override void OnExit()
    {
    }

    //==================================================================
    //
    //==================================================================

    public void OnBackButtonPressed()
    {
        MenuController.Instance.SwitchMenu(EMenuScreen.MainMenu);
    }
}
