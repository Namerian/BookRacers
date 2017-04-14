using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotMenuController : BaseMenu
{

    [Header("Stat Bar")]

    [SerializeField]
    private Button _backButton;

    [SerializeField]
    private Text _nameText;

    [SerializeField]
    private Text _xpText;

    [SerializeField]
    private Text _accelerationText;

    [SerializeField]
    private Text _turnSpeedText;

    [SerializeField]
    private Text _massText;

    [Header("Upgrade Panel")]

    [SerializeField]
    private CanvasGroup _upgradeCanvasGroup;

    [SerializeField]
    private Text _upgradeLevelText;

    [SerializeField]
    private Text _upgradeBonusText;

    [SerializeField]
    private Button _upgradeButton;

    [SerializeField]
    private Text _upgradeXpPriceText;

    [SerializeField]
    private Text _upgradeImprovementText;

    //==================================================================
    //
    //==================================================================

    public override EMenuScreen MenuType { get { return EMenuScreen.PilotMenu; } }

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
        PlayerData playerData = GameController.Instance.PlayerData;
        PilotData pilotData = GameController.Instance.PilotData[playerData.CurrentPilotIndex];

        LoadStatBar(playerData, pilotData);

        LoadUpgradePanel(playerData, pilotData);
    }

    protected override void OnExit()
    {
        _upgradeCanvasGroup.alpha = 0;
        _upgradeCanvasGroup.interactable = false;
        _upgradeCanvasGroup.blocksRaycasts = false;
    }

    //==================================================================
    //
    //==================================================================

    private void LoadStatBar(PlayerData playerData, PilotData pilotData)
    {
        _nameText.text = "Name: " + pilotData.Name;
        _xpText.text = "XP: " + pilotData.Experience;

        _accelerationText.text = "Acceleration: +" + pilotData.Acceleration + "%";
        _turnSpeedText.text = "Turn Speed: +" + pilotData.TurnSpeed + "%";
        _massText.text = "Mass: +" + pilotData.Mass + "%";
    }

    private void LoadUpgradePanel(PlayerData playerData, PilotData pilotData)
    {
        _upgradeCanvasGroup.alpha = 1;
        _upgradeCanvasGroup.interactable = true;
        _upgradeCanvasGroup.blocksRaycasts = true;

        _upgradeLevelText.text = "UpgradeLevel: " + pilotData.Upgrade.CurrentLevel + "/" + pilotData.Upgrade.NumLevels;

        switch (pilotData.Upgrade.Stat)
        {
            case EVehicleStat.acceleration:
                _upgradeBonusText.text = "Acceleration: +" + pilotData.Upgrade.CurrentLevel * pilotData.Upgrade.ValuePerLevel + "%";
                break;
            case EVehicleStat.mass:
                _upgradeBonusText.text = "Mass: +" + pilotData.Upgrade.CurrentLevel * pilotData.Upgrade.ValuePerLevel + "%";
                break;
            case EVehicleStat.turnSpeed:
                _upgradeBonusText.text = "Turn Speed: +" + pilotData.Upgrade.CurrentLevel * pilotData.Upgrade.ValuePerLevel + "%";
                break;
        }

        if (pilotData.Upgrade.CurrentLevel < pilotData.Upgrade.NumLevels)
        {
            _upgradeButton.enabled = true;

            int xpPrice = (pilotData.Upgrade.CurrentLevel + 1) * pilotData.Upgrade.LevelExpCost;

            _upgradeXpPriceText.text = xpPrice + " XP";

            switch (pilotData.Upgrade.Stat)
            {
                case EVehicleStat.acceleration:
                    _upgradeImprovementText.text = "Acceleration: +" + pilotData.Upgrade.ValuePerLevel + "%";
                    break;
                case EVehicleStat.mass:
                    _upgradeImprovementText.text = "Mass: +" + pilotData.Upgrade.ValuePerLevel + "%";
                    break;
                case EVehicleStat.turnSpeed:
                    _upgradeImprovementText.text = "Turn Speed: +" + pilotData.Upgrade.ValuePerLevel + "%";
                    break;
            }

            if (pilotData.Experience >= xpPrice)
                _upgradeButton.interactable = true;
            else
                _upgradeButton.interactable = false;
        }
        else
        {
            _upgradeButton.enabled = false;
        }
    }

    //==================================================================
    //
    //==================================================================

    public void OnBackButtonPressed()
    {
        MenuController.Instance.SwitchMenu(EMenuScreen.MainMenu);
    }

    public void OnUpgradeButtonPressed()
    {
        PlayerData playerData = GameController.Instance.PlayerData;
        PilotData pilotData = GameController.Instance.PilotData[playerData.CurrentPilotIndex];

        int xpPrice = (pilotData.Upgrade.CurrentLevel + 1) * pilotData.Upgrade.LevelExpCost;

        if (pilotData.Experience >= xpPrice)
        {
            pilotData.Experience -= xpPrice;

            pilotData.Upgrade.CurrentLevel++;

            OnEnter();
        }
    }
}
