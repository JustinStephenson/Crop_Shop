using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {

    [SerializeField]
    private float startingTimeInSeconds = 0;

    private Text minuteText;
    private Text secondTensText;
    private Text secondOnesText;
    private float timer;
    private float timerTemp;
    private int minuteTime;
    private int secondTime;
    private int secondTensTime;
    private int secondOnesTime;

    void Start()
    {
        minuteText = GetComponentsInChildren<Text>()[0];
        secondTensText = GetComponentsInChildren<Text>()[1];
        secondOnesText = GetComponentsInChildren<Text>()[2];
        minuteTime = (int)(startingTimeInSeconds / 60);
        secondTime = (int)(startingTimeInSeconds % 60);
        timer = startingTimeInSeconds + (minuteTime - 1);
        timerTemp = timer;
    }

    void Update()
    {
        startingTimeInSeconds -= Time.deltaTime;
        if (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            if (timer < timerTemp - 1)
            {
                secondTime--;
                timerTemp = timer;
            }
            if (secondTime <= 0.0f)
            {
                minuteTime--;
                secondTime = 60;
            }
        }
        minuteText.text = minuteTime.ToString();
        if (secondTime == 60)
        {
            minuteText.text = (minuteTime + 1).ToString();
            secondTensText.text = 0.ToString();
            secondOnesText.text = 0.ToString();
        }
        else
        {
            secondTensTime = secondTime / 10;
            secondOnesTime = secondTime % 10;
            secondTensText.text = secondTensTime.ToString();
            secondOnesText.text = secondOnesTime.ToString();
        }
    }

    public string GetMinute()
    {
        return minuteText.text;
    }

    public string GetSeconedTen()
    {
        return secondTensText.text;
    }

    public string GetSeconedOne()
    {
        return secondOnesText.text;
    }
}
