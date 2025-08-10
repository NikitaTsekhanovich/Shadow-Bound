using UnityEngine;

public class ScreenOrientation : MonoBehaviour
{
    private void Awake()
    {
        Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
    }
}
