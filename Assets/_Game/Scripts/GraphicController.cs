using System.Collections;
using UnityEngine;

public class GraphicController : MonoBehaviour
{
    public Transform sharkGraphics;
    public Transform missGraphics;

    float animationDelay = 0.5f;

    public IEnumerator SharkC()
    {
        for (int i = 0; i < sharkGraphics.childCount; i++)
        {
            sharkGraphics.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(animationDelay);
            sharkGraphics.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Miss(bool active)
    {
        missGraphics.gameObject.SetActive(active);
    }
}
