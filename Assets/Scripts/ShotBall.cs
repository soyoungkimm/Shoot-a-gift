using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBall : MonoBehaviour
{
    public float speed = 200;
    public GameObject present;
    public GameObject bomb;
    public Transform shot_location;
    private int randomInt;
    private int randomSecond;
    public Timer timer;
    public Score score;


    public void StartSpawnBall()
    {
        StartCoroutine(SpawnBall());
    }



    IEnumerator SpawnBall()
    {
        // timer 시작
        timer.time = 60.0f;

        // 무한 루프
        while (true)
        {
            // timer의 time이 끝나면 멈춤
            if (timer.time > 0)
            {
                GameObject spawnedThing;

                // 1 ~ 2의 값 중 하나 나옴
                randomInt = Random.Range(1, 3);

                if (randomInt == 1)
                {
                    spawnedThing = Instantiate(present, shot_location.position, shot_location.rotation);
                }
                else
                {
                    spawnedThing = Instantiate(bomb, shot_location.position, shot_location.rotation);
                }
                spawnedThing.GetComponent<Rigidbody>().AddForce(speed * shot_location.forward);

                // 1 ~ 3초 중 하나
                randomSecond = Random.Range(1, 4);

                switch (randomSecond)
                {
                    case 1:
                        yield return new WaitForSeconds(1.0f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(2.0f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(3.0f);
                        break;
                }
            }
            else
            {
                score.print_result();
                Debug.Log("코루틴 끝");
                break;
            }
        }
    }

}
