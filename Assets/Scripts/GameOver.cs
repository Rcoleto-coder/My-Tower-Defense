using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    
    public void Retry()
    {

        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {

        sceneFader.FadeTo(menuSceneName);
    }
}
