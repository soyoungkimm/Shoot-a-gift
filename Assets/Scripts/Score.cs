using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public bool isFinish;

    public AudioSource audioSource;
    public AudioClip audioClipPresent;
    public AudioClip audioClipBomb;

    public Timer timer;
    public Text scoreTxt;
    public Text resultTxt;
    public GameObject resultCanvas;
    public GameObject closeButton;

    private void Start()
    {
        score = 0;
        isFinish = false;
    }

    public void increaseScore()
    {
        scoreTxt.text = "score : " + score;
        audioSource.PlayOneShot(audioClipPresent);
        score += 10;
        scoreTxt.text = "score : " + score;
    }

    public void decreaseScore()
    {
        audioSource.PlayOneShot(audioClipBomb);
        score -= 4;
        if (score < 0)
        {
            score = 0;
        }
        scoreTxt.text = "score : " + score;
    }

    public void print_result()
    {
        isFinish = true;
        timer.timeTxt.text = "timer : 00:00";
        
        resultTxt.text = score.ToString();
        resultCanvas.SetActive(true);
        closeButton.SetActive(true);
    }
    
}
