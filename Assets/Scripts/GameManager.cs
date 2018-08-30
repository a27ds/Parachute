using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public GameObject parachuterA;
    public GameObject parachuterB;
    public GameObject parachuterC;
    public GameObject parachutersInPlay;
    public Transform boat;
    public Transform miss;
    public Transform gameMode;
    public Transform livesTrans;
    public Transform deadGraphics;
    public Transform sharkGraphics;
    public Text pointsText;



    public float spawnTime;
    public int lives;

    delegate void SpawnParachuters();

    private bool gameIsRunning;
    private int points;


    private void Start()
    {
        gameIsRunning = false;
        points = 0;
        lives = 3;
        spawnTime = 1.5f;
        UpdatePoints();
    }
    private void OnEnable()
    {
        Buttons.GameAPressed += Buttons_GameAPressed;
        Buttons.GameBPressed += Buttons_GameBPressed;
        Buttons.TimePressed += Buttons_TimePressed;
    }

    private void OnDisable()
    {
        Buttons.GameAPressed -= Buttons_GameAPressed;
        Buttons.GameBPressed -= Buttons_GameBPressed;
        Buttons.TimePressed -= Buttons_TimePressed;
    }



    void Buttons_GameAPressed()
    {
        IsGameRunning();
        GameAStart();
    }

    void Buttons_GameBPressed()
    {
        IsGameRunning();
        GameBStart();
    }

    void IsGameRunning()
    {
        if (gameIsRunning)
        {
            StopGame();
        }
    }

    void Buttons_TimePressed()
    {
        StartCoroutine(ShowTime());
    }

    void GameAStart() 
    {
        gameIsRunning = true;
        gameMode.GetChild(0).gameObject.SetActive(true);
        gameMode.GetChild(1).gameObject.SetActive(false);
        Invoke("SpawnRandomParachuters", spawnTime);
    }

    void GameBStart()
    {
        gameIsRunning = true;
        gameMode.GetChild(0).gameObject.SetActive(false);
        gameMode.GetChild(1).gameObject.SetActive(true);
        NewParachuterB();
    }

    IEnumerator ShowTime()
    {
        string time = DateTime.Now.ToString("tt h:mm");
        pointsText.text = time;
        yield return new WaitForSeconds(2.0f);
        UpdatePoints();
    }

    void SpawnRandomParachuters()
    {
        List<SpawnParachuters> spawnParachuters = new List<SpawnParachuters>
        {
            NewParachuterA,
            NewParachuterB,
            NewParachuterC
        };
        spawnParachuters[UnityEngine.Random.Range(0, spawnParachuters.Count)]();
        Invoke("SpawnRandomParachuters", spawnTime);
    }


    void NewParachuterA()
    {
        GameObject newParachuterA = Instantiate(parachuterA);
        newParachuterA.GetComponentInChildren<ParachuterController>().gameManager = this;
        newParachuterA.GetComponentInChildren<ParachuterController>().dead = deadGraphics;
        newParachuterA.GetComponentInChildren<ParachuterController>().shark = sharkGraphics;
        newParachuterA.transform.SetParent(parachutersInPlay.transform, true);
    }

    void NewParachuterB()
    {
        GameObject newParachuterB = Instantiate(parachuterB);
        newParachuterB.GetComponentInChildren<ParachuterController>().gameManager = this;
        newParachuterB.transform.SetParent(parachutersInPlay.transform, true);
    }

    void NewParachuterC()
    {
        GameObject newParachuterC = Instantiate(parachuterC);
        newParachuterC.GetComponentInChildren<ParachuterController>().gameManager = this;
        newParachuterC.transform.SetParent(parachutersInPlay.transform, true);
    }

    public bool InWater(Transform parachuter)
    {
        foreach (Transform t in boat.Find("Placement"))
        {
            if (t.gameObject.activeSelf && t.tag.Equals(parachuter.tag))
            {
                OnePoint();
                return false;
            }
        }
        return true;
    }

    public void OnePoint()
    {
        points++;
        UpdatePoints();
    }

    public void UpdatePoints() {
        pointsText.text = points.ToString();
    }

    public void Miss()
    {
        miss.gameObject.SetActive(true);
        lives--;
        ShowLivesLeft();
        if (lives == 0)
        {
            StopGame();
        }
    }

    private void ShowLivesLeft() 
    {
        switch (lives)
        {
            case 2:
                {
                    
                    livesTrans.GetChild(0).gameObject.SetActive(true);
                    break;
                }

            case 1:
                {
                    livesTrans.GetChild(1).gameObject.SetActive(true);
                    break;
                }

            case 0:
                {
                    livesTrans.GetChild(2).gameObject.SetActive(true);
                    break;
                }

            default:
                break;
        }
    }

    private void StopGame()
    {
        gameIsRunning = false;
        CancelInvoke();
        foreach (Transform child in parachutersInPlay.transform)
        {
            Destroy(child.gameObject);
        }
    }
}