using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject timerObj;
    private GameObject pointsObj;
    private int timer;
    private int points = 0;
    public GameObject[] prefabs;
    private GameObject Player;
    private Sprite playerSprite;
    private GameObject boy;
    private GameObject girl;
    private GameObject bg;
    private GameObject bg1;
    private GameObject bg2;
    private int stage = 1;
    public GameObject winter;
    public GameObject b;
    public GameObject b1;
    public GameObject restart;

    void Start()
    {
        timerObj = GameObject.Find("Timer");
        pointsObj = GameObject.Find("Points");
        Player = GameObject.Find("Player");
        Player.SetActive(false);
        playerSprite = Player.GetComponent<SpriteRenderer>().sprite;
        boy = GameObject.Find("Boy");
        girl = GameObject.Find("Girl");
        girl.SetActive(false);
        bg = GameObject.Find("BG");
        bg1 = GameObject.Find("BG1");
        bg2 = GameObject.Find("BG2");
        bg1.SetActive(false);
        bg2.SetActive(false);

        PauseGame();
        StartCoroutine(TimerCR());
        StartCoroutine(SpawnGarbageCR());
    }

    public void ChangeGender()
    {
        if (girl.activeSelf)
        {
            playerSprite = boy.GetComponent<Image>().sprite;
            boy.SetActive(true);
            girl.SetActive(false);
        }
        else
        {
            playerSprite = girl.GetComponent<Image>().sprite;

            boy.SetActive(false);
            girl.SetActive(true);
        }


    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        Player.SetActive(true);
        Player.GetComponent<SpriteRenderer>().sprite = playerSprite;
        GameObject.Find("Start").SetActive(false);
        GameObject.Find("Quit").SetActive(false);
        GameObject.Find("LeftButton").SetActive(false);
        GameObject.Find("RightButton").SetActive(false);
        boy.SetActive(false);
        girl.SetActive(false);
    }
    public void AddTime(int time)
    {
        timer += time;
        full += time;
    }
    private int full = 45;
    IEnumerator TimerCR()
    {
        timer = 15;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timerObj.GetComponent<Text>().text = "Vrijeme: " + full.ToString();
            timer--;
            full--;
        }

        GameStateTimeOut();
    }

    IEnumerator SpawnGarbageCR()
    {
        Vector2 pos = new Vector2(Random.Range(7f, 9f), 7.4f);
        Vector2 pos2 = new Vector2(Random.Range(-9f, -7f), 7.4f);
        while (timer >= 2)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject tmp = Instantiate(prefabs[Random.Range(0, prefabs.Length)], pos, Quaternion.identity);
            tmp.transform.eulerAngles = Vector3.forward * Random.Range(15f, 45f);

            GameObject tmp2 = Instantiate(prefabs[Random.Range(0, prefabs.Length)], pos2, Quaternion.identity);
            tmp2.transform.eulerAngles = Vector3.forward * Random.Range(15f, 45f);
            tmp2.GetComponent<GarbageWindController>().invert = true;
        }

    }

    void GameStateTimeOut()
    {
        Time.timeScale = 0;
        StartCoroutine(TimerCR());
        StartCoroutine(SpawnGarbageCR());
        Time.timeScale = 1f;
        if (stage == 1)
        {
            bg1.SetActive(true);
            bg.SetActive(false);
            stage++;
            winter.SetActive(true);
        }
        else if (stage == 2)
        {
            bg1.SetActive(false);
            bg2.SetActive(true);
            winter.SetActive(false);
            stage++;
        }
        else
        {
            timerObj.GetComponent<Text>().text = "Vrijeme: 0";
            Time.timeScale = 0;
            b1.SetActive(true);
            b.SetActive(true);
            if (points >= 50)
            {
                b1.GetComponent<Text>().text = "Prikupio/la si dovoljno smeća!";
                GameObject.Find("AudioMng").GetComponent<AudioController>().PlaySoundWin(true);
            }
            else
            {
                b.GetComponent<Text>().text = "Žao nam je,";
                b1.GetComponent<Text>().text = "ali nisi prikupio/la dovoljno smeća!";
                GameObject.Find("AudioMng").GetComponent<AudioController>().PlaySoundWin(false);
            }
            restart.SetActive(true);
        }



    }

    public void AddPoints(int points_)
    {
        points += points_;
        UpdatePointsCanvas();
    }

    void UpdatePointsCanvas()
    {
        pointsObj.GetComponent<Text>().text = "Bodovi: " + points.ToString();
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
