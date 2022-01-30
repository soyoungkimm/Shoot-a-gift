using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentTrigger : MonoBehaviour
{
    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag.Equals("bullet") || col.tag.Equals("ball"))
        {
            player.GetComponent<Score>().increaseScore();
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        else if (col.tag.Equals("ground") || col.tag.Equals("table") || col.tag.Equals("wall") || col.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }

    }
}
