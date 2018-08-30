using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

    public delegate void ButtonPressed();
    public static event ButtonPressed LeftPressed;
    public static event ButtonPressed RightPressed;
    public static event ButtonPressed GameAPressed;
    public static event ButtonPressed GameBPressed;
    public static event ButtonPressed TimePressed;

    public enum WhichButton
    {
        left, right, gameA, gameB, time
    };

    public WhichButton whichButton;

    private void OnMouseDown()
    {
        switch (whichButton)
        {
            case WhichButton.left:
                {
                    if (LeftPressed != null)
                        LeftPressed();
                    break;
                }
            case WhichButton.right:
                {
                    if (RightPressed != null)
                        RightPressed();
                    break;
                }
            case WhichButton.gameA:
                {
                    if (GameAPressed != null)
                        GameAPressed();
                    break;
                }
            case WhichButton.gameB:
                {
                    if (GameBPressed != null)
                        GameBPressed();
                    break;
                }
            case WhichButton.time:
                {
                    if (TimePressed != null)
                        TimePressed();
                    break;
                }
            default:
                break;
        }
    }
}
