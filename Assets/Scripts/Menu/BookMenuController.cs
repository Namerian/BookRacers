using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookMenuController : BaseMenu
{
    [Header("Stat Bar")]

    [SerializeField]
    private Button _backButton;

    [SerializeField]
    private Text _nameText;

    [SerializeField]
    private Text _xpText;

    [SerializeField]
    private Text _moneyText;

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
    private Text _upgradeMoneyPriceText;

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

    public override EMenuScreen MenuType { get { return EMenuScreen.BookMenu; } }

    //==================================================================
    //
    //==================================================================

    // Use this for initialization
    void Start()
    {
        _upgradeCanvasGroup.alpha = 0;
        _upgradeCanvasGroup.interactable = false;
        _upgradeCanvasGroup.blocksRaycasts = false;
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
        BookData bookData = GameController.Instance.BookData[playerData.CurrentBookIndex];

        LoadStatBar(playerData, bookData);

        if (bookData.Unlocked)
            LoadUpgradePanel(playerData, bookData);
        else
            LoadBuyPanel(playerData, bookData);
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

    private void LoadStatBar(PlayerData playerData, BookData bookData)
    {
        _nameText.text = "Name: " + bookData.Name;
        _xpText.text = "XP: " + bookData.Experience;
        _moneyText.text = "Money: " + playerData.Money;

        _accelerationText.text = "Acceleration: " + bookData.Acceleration;
        _turnSpeedText.text = "Turn Speed: " + bookData.TurnSpeed;
        _massText.text = "Mass: " + bookData.Mass;
    }

    private void LoadUpgradePanel(PlayerData playerData, BookData bookData)
    {
        _buyCanvasGroup.alpha = 0;
        _buyCanvasGroup.interactable = false;
        _buyCanvasGroup.blocksRaycasts = false;

        _upgradeCanvasGroup.alpha = 1;
        _upgradeCanvasGroup.interactable = true;
        _upgradeCanvasGroup.blocksRaycasts = true;

        _upgradeLevelText.text = "UpgradeLevel: " + bookData.Upgrade.CurrentLevel + "/" + bookData.Upgrade.NumLevels;

        switch (bookData.Upgrade.Stat)
        {
            case EVehicleStat.acceleration:
                _upgradeBonusText.text = "Acceleration: +" + bookData.Upgrade.CurrentLevel * bookData.Upgrade.ValuePerLevel;
                break;
            case EVehicleStat.mass:
                _upgradeBonusText.text = "Mass: +" + bookData.Upgrade.CurrentLevel * bookData.Upgrade.ValuePerLevel;
                break;
            case EVehicleStat.turnSpeed:
                _upgradeBonusText.text = "Turn Speed: +" + bookData.Upgrade.CurrentLevel * bookData.Upgrade.ValuePerLevel;
                break;
        }

        if (bookData.Upgrade.CurrentLevel < bookData.Upgrade.NumLevels)
        {
            _upgradeButton.enabled = true;

            int xpPrice = (bookData.Upgrade.CurrentLevel + 1) * bookData.Upgrade.LevelExpCost;
            int moneyPrice = (bookData.Upgrade.CurrentLevel + 1) * bookData.Upgrade.LevelMoneyCost;

            _upgradeXpPriceText.text = xpPrice + " XP";
            _upgradeMoneyPriceText.text = moneyPrice + " Money";

            switch (bookData.Upgrade.Stat)
            {
                case EVehicleStat.acceleration:
                    _upgradeImprovementText.text = "Acceleration: +" + bookData.Upgrade.ValuePerLevel;
                    break;
                case EVehicleStat.mass:
                    _upgradeImprovementText.text = "Mass: +" + bookData.Upgrade.ValuePerLevel;
                    break;
                case EVehicleStat.turnSpeed:
                    _upgradeImprovementText.text = "Turn Speed: +" + bookData.Upgrade.ValuePerLevel;
                    break;
            }

            if (bookData.Experience >= xpPrice && playerData.Money >= moneyPrice)
                _upgradeButton.interactable = true;
            else
                _upgradeButton.interactable = false;
        }
        else
        {
            _upgradeButton.enabled = false;
        }
    }

    private void LoadBuyPanel(PlayerData player, BookData book)
    {
        _upgradeCanvasGroup.alpha = 0;
        _upgradeCanvasGroup.interactable = false;
        _upgradeCanvasGroup.blocksRaycasts = false;

        _buyCanvasGroup.alpha = 1;
        _buyCanvasGroup.interactable = true;
        _buyCanvasGroup.blocksRaycasts = true;

        _buyCostText.text = "Cost: " + book.Cost;

        if (player.Money >= book.Cost)
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
        BookData bookData = GameController.Instance.BookData[playerData.CurrentBookIndex];

        int xpPrice = (bookData.Upgrade.CurrentLevel + 1) * bookData.Upgrade.LevelExpCost;
        int moneyPrice = (bookData.Upgrade.CurrentLevel + 1) * bookData.Upgrade.LevelMoneyCost;

        if (bookData.Experience >= xpPrice && playerData.Money >= moneyPrice)
        {
            bookData.Experience -= xpPrice;
            playerData.Money -= moneyPrice;

            bookData.Upgrade.CurrentLevel++;

            OnEnter();
        }
    }

    public void OnBuyButtonPressed()
    {
        PlayerData playerData = GameController.Instance.PlayerData;
        BookData bookData = GameController.Instance.BookData[playerData.CurrentBookIndex];

        if (!bookData.Unlocked && playerData.Money >= bookData.Cost)
        {
            playerData.Money -= bookData.Cost;
            bookData.Unlocked = true;

            OnEnter();
        }
    }
}
