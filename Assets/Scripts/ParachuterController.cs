using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuterController : MonoBehaviour {

    public GameManager gameManager;
    public Transform positions;
    public Transform dead;
    public Transform shark;
    public float showTime = 0.5f;

    public delegate void ParachuterAction();
    public static event ParachuterAction Move;
    public static event ParachuterAction Inwater;

    public int currentPlacement = 0;
    float lastMoveTime;
    bool stop = false;

	void Start () 
    {
        StartCoroutine(SpawnParachuter());
        //positions.GetChild(currentPlacement).gameObject.SetActive(true);

        //if (Move != null)
        //    Move();

        //lastMoveTime = Time.time;
        //StartCoroutine(ShowHide());
	}

    IEnumerator SpawnParachuter ()
    {
        
        foreach (Transform parachuter in positions)
        {
            if (currentPlacement == positions.childCount - 1)
            {
                if (gameManager.InWater(gameObject.transform))
                {
                    StartCoroutine(ParachuterInWater());
                }
            }
            else 
            {
                if (Move != null)
                {
                    Move();
                }
                parachuter.gameObject.SetActive(true);
                yield return new WaitForSeconds(showTime);
                parachuter.gameObject.SetActive(false);
                currentPlacement++;
            }
        }
    }

    IEnumerator ParachuterInWater()
    {
        if (Inwater != null)
            Inwater();

        gameManager.Miss();
        //HideLastPlacement();
        //positions.GetChild(positions.childCount - 1).gameObject.SetActive(true);

        //// lägg till döds animation.. player och shark
        //shark.GetChild(0).gameObject.SetActive(true);
        //dead.GetChild(0).gameObject.SetActive(true);
        //yield return new WaitForSeconds(2.0f);



        //stop = true;
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(3.0f);
        //gameManager.dead.length
        gameManager.miss.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        Destroy(positions.parent.gameObject);
    }
	
    //IEnumerator ShowHide()
    //{
    //    while (!stop)
    //    {
    //        HideLastPlacement();
    //        yield return new WaitForSeconds(showTime);
    //        //ShowNextPlacement();
    //    }
    //}

    //void HideLastPlacement()
    //{
    //    if (currentPlacement != 0)
    //    {
    //        positions.GetChild(currentPlacement - 1).gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        positions.GetChild(positions.childCount - 2).gameObject.SetActive(false);
    //    }
    //}

    //void ShowNextPlacement() 
    //{
    //    //currentPlacement++;
    //    //if (Move != null)
    //        //Move();
    //    //if (currentPlacement == positions.childCount - 1)
    //    //{
    //    //    if (gameManager.InWater(gameObject.transform)) 
    //    //    {
    //    //        StartCoroutine(ParachuterInWater());
    //    //    }
    //    //} 
    //    else
    //    {
    //        positions.GetChild(currentPlacement).gameObject.SetActive(true);
    //        lastMoveTime = Time.time;
    //    }
    //}


}