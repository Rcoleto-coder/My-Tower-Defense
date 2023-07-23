using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{

    public static bool gameIsOver;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public EndGame callMenu;

    
    // Start is called before the first frame update
    void Start()
    {
          gameIsOver = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            return;
        }


        if (PlayerStats.lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameIsOver = true;
        //Enables the game over UI
        gameOverUI.SetActive(true); 
    }

    public void WinLevel()
    {
        gameIsOver = true;
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level02") 
        { 
            callMenu.LoadMenu();
        }
        completeLevelUI.SetActive(true);


    }

   

}
