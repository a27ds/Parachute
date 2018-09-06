using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PointController pointController;
    public GraphicController graphicController;
    public GameObject parachuterA;
    public GameObject parachuterB;
    public GameObject parachuterC;
    public GameObject parachutersInPlay;
    public Transform boat;
    public Transform gameMode;
    public Transform livesTrans;
    public Transform deadGraphics;
    List<GameObject> parachuters;

    public float spawnTime;
    public int lives;
    public bool pause = false;

    delegate void SpawnParachuters();

    private bool gameIsRunning;
    private int points;

    private void Start()
    {
        StartCoroutine(graphicController.SharkC());
        StartCoroutine(NewGame());
    }

    IEnumerator NewGame()
    {
        if (gameIsRunning)
        {
            StopAllCoroutines();
            foreach (Transform child in parachutersInPlay.transform)
            {
                Destroy(child.gameObject);
            }
        }

        if (parachuters != null)
        {
            parachuters.Clear();
        }
        parachuters = new List<GameObject>
        {
            parachuterA,
            parachuterB,
            parachuterC
        };
        gameIsRunning = true;
        pause = false;
        graphicController.Miss(false);
        points = 0;
        lives = 3;
        spawnTime = 1.5f;
        ShowLivesLeft();
        UpdatePoints();
        yield return new WaitForSeconds(0.5f);
    }

    private void StopGame()
    {
        gameIsRunning = false;
        foreach (Transform child in parachutersInPlay.transform)
        {
            Destroy(child.gameObject);
        }
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

        GameAStart();
    }

    void Buttons_GameBPressed()
    {
        GameBStart();
    }

    void Buttons_TimePressed()
    {
        StartCoroutine(pointController.ShowTime(points));
    }

    void GameAStart()
    {
        StartCoroutine(NewGame());
        ShowGameAOrB(0);
        StartCoroutine(SpawnRandomParachuters());
    }

    void GameBStart()
    {
        //Nothing here yet
    }

    void ShowGameAOrB(int GameMode)  //Mode 0 = GameA. Mode 1 = GameB. 
    {
        if (GameMode == 0)
        {
            gameMode.GetChild(0).gameObject.SetActive(true);
            gameMode.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            gameMode.GetChild(0).gameObject.SetActive(false);
            gameMode.GetChild(1).gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnRandomParachuters()
    {
        while (gameIsRunning)
        {
            while (pause)
            {
                yield return null;
            }
            GameObject newParachuter = Instantiate(parachuters[Random.Range(0, parachuters.Count)]);
            newParachuter.GetComponentInChildren<ParachuterController>().gameManager = this;
            newParachuter.transform.SetParent(parachutersInPlay.transform, true);
            yield return new WaitForSeconds(spawnTime);
        }
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

    public void UpdatePoints()
    {
        pointController.Setpoint(points);
    }

    public void Miss()
    {
        graphicController.Miss(true);
        lives--;
        StartCoroutine(graphicController.SharkC());
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
            case 3:
                {
                    livesTrans.GetChild(0).gameObject.SetActive(false);
                    livesTrans.GetChild(1).gameObject.SetActive(false);
                    livesTrans.GetChild(2).gameObject.SetActive(false);
                    break;
                }
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
}