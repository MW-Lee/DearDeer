using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaffold : MonoBehaviour
{
    public GameObject gObstacle;
    public GameObject gParticle;
    public AudioSource _audioSource;
    public float fMaxTime;

    private bool bActive = false;
    private float fTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gObstacle.SetActive(false);
        gParticle.SetActive(true);
        bActive = true;

        _audioSource.PlayOneShot(_audioSource.clip);
    }


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (bActive)
        {
            fTime += Time.deltaTime;

            if (fTime >= fMaxTime)
            {
                gObstacle.SetActive(true);
                gParticle.SetActive(false);
                bActive = false;
                fTime = 0;
            }
        }
    }
}
