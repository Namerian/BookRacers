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

    [Header("Buy Panel")]

    [SerializeField]
    private CanvasGroup _buyCanvasGroup;

    [SerializeField]
    private Button _buyButton;

    [SerializeField]
    private Text _buyCostText;

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

        if (pilotData.Unlocked)
            LoadUpgradePanel(playerData, pilotData);
        else
            LoadBuyPanel(playerData, pilotData);
    }

    protected override void OnExit()
    {
        _upgradeCanvasGroup.alpha = 0;
        _upgradeCanvasGroup.interactable = false;
        _upgradeCanvasGroup.blocksRaycasts = false;

        _buyCanvasGroup.alpha = 0;
        _buyCanvasGroup.interactable = false;
        _buyCanvasGroup.blocksRaycasts = false;
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
        _buyCanvasGroup.alpha = 0;
        _buyCanvasGroup.interactable = false;
        _buyCanvasGroup.blocksRaycasts = false;

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

    private void LoadBuyPanel(PlayerData player, PilotData pilot)
    {
        _upgradeCanvasGroup.alpha = 0;
        _upgradeCanvasGroup.interactable = false;
        _upgradeCanvasGroup.blocksRaycasts = false;

        _buyCanvasGroup.alpha = 1;
        _buyCanvasGroup.interactable = true;
        _buyCanvasGroup.blocksRaycasts = true;

        _buyCostText.text = "Cost: " + pilot.Cost;

        if (player.Money >= pilot.Cost)
            _buyButton.interactable = true;
        else
            _buyButton.interactable = false;
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

    public void OnBuyButtonPressed()
    {
        PlayerData playerData = GameController.Instance.PlayerData;
        PilotData pilotData = GameController.Instance.PilotData[playerData.CurrentPilotIndex];

        if (!pilotData.Unlocked && playerData.Money >= pilotData.Cost)
        {
            playerData.Money -= pilotData.Cost;
            pilotData.Unlocked = true;

            OnEnter();
        }
    }
}
