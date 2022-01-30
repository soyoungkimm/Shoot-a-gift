using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    public Text countdownTxt;
    public AudioSource audioSource;
    public AudioClip audioClipStartButton;
    public bool isClickStart;
    public bool isFinishCountDown;
    public ShotBall shotball;
    public ShotBall shotball1;
    public ShotBall shotball2;
    public ShotBall shotball3;
    public ShotBall shotball4;
    public ShotBall shotball5;
    public GameObject resultCanvas;
    public Text start_btn_txt; 

    private void Start()
    {
        isClickStart = false;
        isFinishCountDown = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("hand"))
        {
            if (resultCanvas.activeSelf)
            {
                start_btn_txt.text = "Click Close Button!";
                return;
            }
            StartCoroutine(CountDown());
        }
    }


    IEnumerator CountDown()
    {
        if (!isClickStart)
        {
            isClickStart = true;
            // 카운트 다운 5 ~ 1
            int count;

            for (count = 5; count > 0; count--)
            {
                countdownTxt.text = count.ToString();
                audioSource.PlayOneShot(audioClipStartButton);
                yield return new WaitForSeconds(1.0f);
            }
            isFinishCountDown = true;
            countdownTxt.text = "";

            
            shotball.StartSpawnBall();
            shotball1.StartSpawnBall();
            shotball2.StartSpawnBall();
            shotball3.StartSpawnBall();
            shotball4.StartSpawnBall();
            shotball5.StartSpawnBall();
        }
    }
}
