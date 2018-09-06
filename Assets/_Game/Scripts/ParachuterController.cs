using System.Collections;
using UnityEngine;

public class ParachuterController : MonoBehaviour
{
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

    void Start()
    {
        StartCoroutine(SpawnParachuter());
    }

    IEnumerator SpawnParachuter()
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
                while (gameManager.pause)
                {
                    yield return null;
                }
                parachuter.gameObject.SetActive(false);
                currentPlacement++;
            }
        }
    }

    IEnumerator ParachuterInWater()
    {
        if (Inwater != null)
            Inwater();
        positions.GetChild(positions.childCount - 1).gameObject.SetActive(true);
        gameManager.pause = true;
        gameManager.Miss();
        yield return new WaitForSecondsRealtime(3.0f);
        gameManager.graphicController.Miss(false);
        gameManager.pause = false;
        Destroy(positions.parent.gameObject);
    }
}