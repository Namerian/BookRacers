﻿using System;
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
    private Text _upgradePriceText;

    [SerializeField]
    private Text _upgradeImprovementText;

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

        LoadUpgradePanel(playerData, bookData);
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

    private void LoadStatBar(PlayerData playerData, BookData bookData)
    {
        _nameText.text = "Name: " + bookData.Name;
        _xpText.text = "XP: " + bookData.Experience;
        _moneyText.text = "Money: " + playerData.Money;

        float acceleration = bookData.Acceleration;
        float turnSpeed = bookData.TurnSpeed;
        float mass = bookData.Mass;

        switch (bookData.Upgrade.Stat)
        {
            case EVehicleStat.acceleration:
                acceleration += bookData.Upgrade.CurrentLevel * bookData.Upgrade.ValuePerLevel;
                break;
            case EVehicleStat.mass:
                mass+= bookData.Upgrade.CurrentLevel * bookData.Upgrade.ValuePerLevel;
                break;
            case EVehicleStat.turnSpeed:
                turnSpeed += bookData.Upgrade.CurrentLevel * bookData.Upgrade.ValuePerLevel;
                break;
        }

        _accelerationText.text = "Acceleration: " + acceleration;
        _turnSpeedText.text = "Turn Speed: " + turnSpeed;
        _massText.text = "Mass: " + mass;
    }

    private void LoadUpgradePanel(PlayerData playerData, BookData bookData)
    {
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
                _upgradeBonusText.text = "Mass: +" + bookData.Upgrade.CurrentLevel * bookData.Upgrade.ValuePerLevel; break;
            case EVehicleStat.turnSpeed:
                _upgradeBonusText.text = "Turn Speed: +" + bookData.Upgrade.CurrentLevel * bookData.Upgrade.ValuePerLevel; break;
        }

        if(bookData.Upgrade.CurrentLevel < bookData.Upgrade.NumLevels)
        {
            _upgradeButton.enabled = true;
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

    }
}