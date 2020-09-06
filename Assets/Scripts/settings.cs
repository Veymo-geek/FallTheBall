using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;


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

    public List<GameObject> platforms_lvl_1 = new List<GameObject>();
    public List<GameObject> platforms_lvl_2 = new List<GameObject>();
    public List<GameObject> platforms_lvl_3 = new List<GameObject>();
    public List<GameObject> platforms_lvl_4 = new List<GameObject>();
    public List<GameObject> platforms_lvl_5 = new List<GameObject>();

    private static Color ballcolor;
    private int num_platforms = 10;
    private static int num_platforms_id = 0;


    internal void setScore(float i)
    {
        score += level * (i);
    }

    void Start()
    {
        level = PlayerPrefs.GetFloat("Level");
        num_platforms_id = 0;
        num_platforms = 10 + Convert.ToInt32(Math.Pow((level*70), 1 / 2.4));
        generatePlatforms();
        generatePlatforms();
        generatePlatforms();
        generatePlatforms();
        generatePlatforms();
        setPrefs();
        textActive();
        Time.timeScale = gameSpeed;
        StartCoroutine(colorset());

        if (Advertisement.isSupported) {
            Advertisement.Initialize("3796815", false);
        }
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

    public void generatePlatforms()
    {
        
        if (num_platforms_id == 0) Instantiate(obj_start, new Vector3(0, num_platforms_id * height * -1, 0), Quaternion.Euler(0f, 150f, 0f));
        if (num_platforms_id > 0 && num_platforms_id < num_platforms)
        {
            int number = getPlatform(level);
            generate(number, height, UnityEngine.Random.Range(0, 360));
        }

        if (num_platforms_id == num_platforms) Instantiate(obj_finish, new Vector3(0, num_platforms_id * height * -1, 0), Quaternion.Euler(0f, 0f, 0f));
        num_platforms_id++;
    }

    private int getPlatform(float lvl)
    {
        int resPlatform = 0;
        float range = UnityEngine.Random.Range(0, 100);
        if (lvl <= 1)
        {
            resPlatform = 1;
        }
        if (lvl > 1 && lvl <= 3) 
        {
            if (range < 50) resPlatform = 1;
            else resPlatform = 2;
        }
        if (lvl > 3 && lvl <= 10)
        {
            if (range < 10) resPlatform = 1;
            if (range >= 10 && range < 90) resPlatform = 2;
            if (range >= 90) resPlatform = 3;
        }
        if (lvl > 10 && lvl <= 20)
        {
            if (range < 50) resPlatform = 2;
            else resPlatform = 3;
        }
        if (lvl > 20 && lvl <= 30)
        {
            if (range < 10) resPlatform = 2;
            if (range >= 10 && range < 80) resPlatform = 3;
            if (range >= 80) resPlatform = 4;
        }
        if (lvl > 30 && lvl <= 50)
        {
            if (range < 50) resPlatform = 3;
            else resPlatform = 4;
        }
        if (lvl > 50 && lvl <= 70)
        {
            if (range < 20) resPlatform = 3;
            if (range >= 20 && range < 80) resPlatform = 4;
            if (range >= 80) resPlatform = 5;
        }
        if (lvl > 70 && lvl <= 90)
        {
            if (range < 10) resPlatform = 3;
            if (range >= 10 && range < 70) resPlatform = 4;
            if (range >= 70) resPlatform = 5;
        }
        if (lvl > 90 && lvl <= 130)
        {
            if (range < 50) resPlatform = 4;
            else resPlatform = 5;
        }
        if (lvl > 130 && lvl <= 160)
        {
            if (range < 20) resPlatform = 4;
            else resPlatform = 5;
        }
        if (lvl > 160)
        {
            if (range < 10) resPlatform = 4;
            else resPlatform = 5;
        }
        return resPlatform;
    }

    private void generate(int numlistOfPlatforms, float height, int rotation)
    {
        //num_platforms_id++;
        List<GameObject> listOfPlatforms = new List<GameObject>();
        if (numlistOfPlatforms < 2) listOfPlatforms = platforms_lvl_1;
        if (numlistOfPlatforms == 2) listOfPlatforms = platforms_lvl_2;
        if (numlistOfPlatforms == 3) listOfPlatforms = platforms_lvl_3;
        if (numlistOfPlatforms == 4) listOfPlatforms = platforms_lvl_4;
        if (numlistOfPlatforms > 4) listOfPlatforms = platforms_lvl_5;
        Instantiate(listOfPlatforms[UnityEngine.Random.Range(0, listOfPlatforms.Count)], new Vector3(0, num_platforms_id * height * -1, 0), Quaternion.Euler(0f, rotation, 0f));
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
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
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

