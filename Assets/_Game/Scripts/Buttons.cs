using UnityEngine;
using DigitalRubyShared;

public class Buttons : MonoBehaviour
{
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

    private TapGestureRecognizer tapGesture;

    private void Start()
    {
        tapGesture = new TapGestureRecognizer();
        tapGesture.StateUpdated += TapGesture_StateUpdated;
        tapGesture.AllowSimultaneousExecutionWithAllGestures();
        tapGesture.ClearTrackedTouchesOnEndOrFail = true;
        tapGesture.MaximumNumberOfTouchesToTrack = 3;
        FingersScript.Instance.AddGesture(tapGesture);
    }

    void TapGesture_StateUpdated(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            Vector2 pos = new Vector2(gesture.FocusX, gesture.FocusY);
            pos = Camera.main.ScreenToWorldPoint(pos);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
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
    }
}