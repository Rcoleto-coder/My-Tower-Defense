using UnityEngine;

public class EndGame : MonoBehaviour
{
    public SceneFader sceneFader;
    
    public void LoadMenu()
    {
        sceneFader.FadeTo("MainMenu");

    }
}
