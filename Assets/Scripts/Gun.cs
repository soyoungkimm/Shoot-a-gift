using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;
    

    public void Update()
    {
        Debug.DrawRay(barrel.transform.position, barrel.transform.forward * 100, Color.red);
    }


    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().AddForce(speed * barrel.forward);
        audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 1);
    }
}
