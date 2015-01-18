using UnityEngine;
using System.Collections;

public class MenuSkript : MonoBehaviour {

    public void Play()
    {
        Application.LoadLevel(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
