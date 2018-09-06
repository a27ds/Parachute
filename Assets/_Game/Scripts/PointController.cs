using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class PointController : MonoBehaviour
{
    public TextMeshPro pointsText;

    void Start()
    {
        pointsText = GetComponent<TextMeshPro>();
        if (pointsText == null)
            Debug.Log("Textmesh component not found");
    }

    public void Setpoint(int points)
    {
        pointsText.SetText(points.ToString());
    }

    public IEnumerator ShowTime(int points)
    {
        string time = DateTime.Now.ToString("tt h:mm");
        pointsText.SetText(time);
        yield return new WaitForSecondsRealtime(2.0f);
        Setpoint(points);
    }
}