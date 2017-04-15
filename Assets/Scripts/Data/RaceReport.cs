using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceReport
{
    public float Time { get; private set; }
    public int MoneyGain { get; private set; }
    public int PilotXpGain { get; private set; }
    public int BookXpGain { get; private set; }

    public RaceReport(float time, int moneyGain, int pilotXpGain, int bookXpGain)
    {
        Time = time;
        MoneyGain = moneyGain;
        PilotXpGain = pilotXpGain;
        BookXpGain = bookXpGain;
    }
}
