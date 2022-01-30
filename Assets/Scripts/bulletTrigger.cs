using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTrigger : MonoBehaviour
{
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("present"))
        {
            player.GetComponent<Score>().increaseScore();
            Destroy(col);
            Destroy(gameObject);
        }
        else if(col.tag.Equals("bomb"))
        {
            player.GetComponent<Score>().decreaseScore();
            Destroy(col);
            Destroy(gameObject);
        }
        else if (col.tag.Equals("ground") || col.tag.Equals("table") || col.tag.Equals("wall"))
        {
            Destroy(col);
            Destroy(gameObject);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
