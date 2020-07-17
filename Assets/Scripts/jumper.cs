using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumper : MonoBehaviour
{
    public static jumper instance;
    public float force = 6f;
    public Rigidbody player;
    public Rigidbody platform;
    public static float combo = 0f;
    public List<AudioSource> sound_platform = new List<AudioSource>();
    public AudioSource sound_platform_gouot;
    public AudioSource sound_platform_combo;
    public AudioSource sound_platform_gameOver;
    public AudioSource sound_platform_win;

    private Animation jump_animation;
    private float startPith;
    private Vector3 basicTransform;
    private bool isjump = true;

    // Start is called before the first frame update

    void Start()
    {
        combo = 0f;
        player = GetComponent<Rigidbody>();
        platform = GetComponent<Rigidbody>();
        startPith = sound_platform_gouot.pitch;
        basicTransform = player.transform.localScale;
        jump_animation = GetComponent<Animation>();
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    IEnumerator jumpFalseCorutine()
    {
        yield return new WaitForSeconds(0.5f);
        isjump = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        jump_animation.Stop();
        jump_animation.Play("Jumper");
        if (collision.collider.tag == "ground")
        {
            
            player.velocity = Vector3.zero;
            sound_platform[UnityEngine.Random.Range(1, sound_platform.Count)].Play();
            if (isjump)
            {
                player.AddForce(Vector3.up * force, ForceMode.Impulse);
                isjump = false;
                StartCoroutine(jumpFalseCorutine());
            }
            if (combo >= 3) {
                Instantiate(settings.instance.smoke, collision.transform.position, Quaternion.identity);
                Destroy(collision.transform.gameObject);
                settings.instance.setScore(jumper.combo);
                sound_platform_combo.Play();
            }
            combo=0;
            sound_platform_gouot.pitch = startPith;
        }
        if (collision.collider.tag == "gameOver")
        {
            if (combo < 3)
            {
                settings.instance.died();
                sound_platform_gameOver.Play();
            }
            else
            {
                player.velocity = Vector3.zero;
                player.AddForce(Vector3.up * force, ForceMode.Impulse);
                Instantiate(settings.instance.smoke, collision.transform.position, Quaternion.identity);
                Destroy(collision.transform.parent.gameObject);
                settings.instance.setScore(jumper.combo);
                sound_platform_combo.Play();
                combo = 0;
                sound_platform_gouot.pitch = startPith;
            }
        }
        if (collision.collider.tag == "finish")
        {
            settings.instance.win();
            sound_platform_win.Play();
        }
    }


    void Update() {

    }

    private void transformJump()
    {
       

    }

    
}
