using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanelController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private Text _text;

    // Use this for initialization
    void Start()
    {
        _canvasGroup.alpha = 0;
    }

    public void ShowMessage(string message, float time)
    {
        _text.text = message;
        _canvasGroup.alpha = 1;
        Invoke("HidePanel", time);
    }

    private void HidePanel()
    {
        _canvasGroup.alpha = 0;
    }
}
