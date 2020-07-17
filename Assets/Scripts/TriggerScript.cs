using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            jumper.combo++;
            jumper.instance.sound_platform_gouot.pitch *= jumper.combo * 0.02f + 1 ;
            jumper.instance.sound_platform_gouot.Play();
            settings.instance.setScore(jumper.combo);
            Instantiate(settings.instance.smoke, transform.parent.position, Quaternion.Euler(-90f,0f,0f));
            Destroy(transform.parent.gameObject);
        }
    }
}
