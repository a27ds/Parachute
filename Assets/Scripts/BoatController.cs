using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour {

    public List<GameObject> boatPlacement = new List<GameObject>();

    int currentPosition = 0;

    private void OnEnable()
    {
        Buttons.LeftPressed += Buttons_LeftPressed;
        Buttons.RightPressed += Buttons_RightPressed;
    }

    private void OnDisable()
    {
        Buttons.LeftPressed -= Buttons_LeftPressed;
        Buttons.RightPressed -= Buttons_RightPressed;
    }

    void Start () 
    {
        foreach (var boat in boatPlacement)
        {
            boat.gameObject.SetActive(false);
        }
        boatPlacement[currentPosition].gameObject.SetActive(true);
	}



    void Buttons_LeftPressed()
    {
        if (currentPosition > 0) 
        {
            boatPlacement[currentPosition].gameObject.SetActive(false);
            currentPosition--;
            boatPlacement[currentPosition].gameObject.SetActive(true);
        }
    }

    void Buttons_RightPressed()
    {
        if (currentPosition < boatPlacement.Count - 1)
        {
            boatPlacement[currentPosition].gameObject.SetActive(false);
            currentPosition++;
            boatPlacement[currentPosition].gameObject.SetActive(true);

        }
    }

}
