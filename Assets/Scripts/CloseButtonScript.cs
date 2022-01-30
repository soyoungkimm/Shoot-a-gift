using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButtonScript : MonoBehaviour
{
    public GameObject resultCanvas;
    public Score score;
    public StartButtonScript startButtonScript;
    public GameObject closeButton;
    

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("hand"))
        {
            
            resultCanvas.SetActive(false);
            closeButton.SetActive(false);

            // 세팅 초기화
            score.score = 0;
            score.scoreTxt.text = "score : 0";
            score.isFinish = false;

            startButtonScript.isFinishCountDown = false;
            startButtonScript.isClickStart = false;
           
        }
        
    }
}
