using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    //===============================================================================
    //
    //===============================================================================

    [Header("Object References")]

    [SerializeField]
    private MessagePanelController _messagePanel;

    [SerializeField]
    private Text _timerText;

    [Header("Variables")]

    [SerializeField]
    private int _numTurns = 1;

    [SerializeField]
    private int _moneyGain = 50;

    [SerializeField]
    private int _pilotXpGain = 50;

    [SerializeField]
    private int _bookXpGain = 50;

    private int _countDown = 4;
    private int _currentTurn = 0;
    private float _timer;

    public bool Running { get; private set; }

    //===============================================================================
    // MONOBEHAVIOUR METHODS
    //===============================================================================

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        Running = false;

        CountDown();
    }

    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            _timer += Time.deltaTime;
        }

        string minutes = Mathf.Floor(_timer / 60).ToString("00");
        string seconds = Mathf.Floor(_timer % 60).ToString("00");
        string milliseconds = Mathf.Floor((_timer * 1000) % 1000).ToString("000");

        _timerText.text = minutes + ":" + seconds + ":" + milliseconds;
    }

    //===============================================================================
    // 
    //===============================================================================

    public void OnPlayerCrossedFinishLine()
    {
        if (_currentTurn == _numTurns)
        {
            Running = false;
            _messagePanel.ShowMessage("Finish!", 1.5f);
            Invoke("EndOfRace", 4f);
        }
        else
        {
            _currentTurn++;

            if (_currentTurn > 1)
                _messagePanel.ShowMessage("Lap " + _currentTurn, 0.6f);
        }
    }

    //===============================================================================
    // 
    //===============================================================================

    private void CountDown()
    {

        switch (_countDown)
        {
            case 3:
                _messagePanel.ShowMessage("3", 0.6f);
                break;
            case 2:
                _messagePanel.ShowMessage("2", 0.6f);
                break;
            case 1:
                _messagePanel.ShowMessage("1", 0.6f);
                break;
            case 0:
                _messagePanel.ShowMessage("GO", 0.6f);
                Running = true;
                break;
        }

        if (_countDown > 0)
        {
            _countDown--;
            Invoke("CountDown", 1);
        }
    }

    private void EndOfRace()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.LastRaceReport = new RaceReport(_timer, _moneyGain, _pilotXpGain, _bookXpGain);

            GameController.Instance.PlayerData.Money += _moneyGain;
            GameController.Instance.PilotData[GameController.Instance.PlayerData.CurrentPilotIndex].Experience += _pilotXpGain;
            GameController.Instance.BookData[GameController.Instance.PlayerData.CurrentBookIndex].Experience += _bookXpGain;
        }

        SceneManager.LoadScene("Menu");
    }
}
