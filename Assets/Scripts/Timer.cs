using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time;
    public Text timeTxt;
    string timeStr;
    public StartButtonScript start_btn;
    public Score score;

    void Start()
    {
        time = 60.0f;   
    }


    void Update()
    {
        if (start_btn.isFinishCountDown && !score.isFinish)
        {
            time -= Time.deltaTime;
            timeStr = time.ToString("00.00");
            timeStr = timeStr.Replace(".", ":");
            timeTxt.text = "timer : " + timeStr;
        }
    }
}
