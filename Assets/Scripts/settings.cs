using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class settings : MonoBehaviour
{
    public static settings instance;

    private static float level = 1;
    private float score = 0;
    private float record = 0;

    public float gameSpeed = 1.5f;
    public float rotationSpeed = 300f;
    public float height = 3.5f;

    public static bool gameOver = false;
    private static bool winGame = false;

    public GameObject gameOvertext;
    public GameObject LevelPassed;
    public Text ScoreText;
    public Text LevelText;
    public Text RecordText;
    public GameObject obj_start;
    public GameObject obj_finish;
    public Transform smoke;
    public List<GameObject> prefabList = new List<GameObject>();

    private int numOfPlatform = 0;
    private static Color ballcolor;
    private int num_platforms = 10;


    internal void setScore(float i)
    {
        score += level * (i);
    }

    void Start()
    {
        num_platforms = 10 + Convert.ToInt32(Math.Pow((level*70), 1 / 2.4));
        generatePlatforms(num_platforms);
        setPrefs();
        textActive();
        Time.timeScale = gameSpeed;
        StartCoroutine(colorset());
    }

    private void textActive()
    {
        gameOvertext.SetActive(false);
        LevelPassed.SetActive(false);
    }

    private void setPrefs()
    {
        if (PlayerPrefs.GetFloat("Level") == 0)
        {
            PlayerPrefs.SetFloat("Level", 1);
        }
        level = PlayerPrefs.GetFloat("Level");
        record = PlayerPrefs.GetFloat("Record");
    }

    private IEnumerator colorset()
    {
        yield return new WaitForSeconds(0);
        if (winGame == true)
        {
            ballcolor = UnityEngine.Random.ColorHSV();
            winGame = false;
        }
        jumper.instance.player.GetComponent<MeshRenderer>().material.color = ballcolor;
    }

    private void generatePlatforms(int count)
    {
        Instantiate(obj_start, new Vector3(0, numOfPlatform * height, 0), Quaternion.Euler(0f, 150f, 0f));
        for (int i = 0; i < count; i++)
        {
            
            generate(UnityEngine.Random.Range(0, prefabList.Count), height, UnityEngine.Random.Range(0, 360));
        }
        numOfPlatform--;
        Instantiate(obj_finish, new Vector3(0, numOfPlatform * height, 0), Quaternion.Euler(0f, 0f, 0f));
    }

    private void generate(int platform, float height, int rotation)
    {
        numOfPlatform--;
        Instantiate(prefabList[platform], new Vector3(0, numOfPlatform * height, 0), Quaternion.Euler(0f, rotation, 0f));
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelText.text = "Lvl: " + level;
        ScoreText.text = "" + score;
        RecordText.text = "Best: " + record;
        if (Input.anyKeyDown && gameOver == true)
        {
            Time.timeScale = gameSpeed;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameOver = false;
            
        }
    }

    internal void died()
    {
        gameOver = true;
        Time.timeScale = 0;
        gameOvertext.SetActive(true);
    }
    internal void win()
    {
        gameOver = true;
        winGame = true;
        level++;
        PlayerPrefs.SetFloat("Level", level);
        if (score > record)
        {
            record = score;
            PlayerPrefs.SetFloat("Record", record);
        }
        Time.timeScale = 0;
        LevelPassed.SetActive(true);
    }
}

