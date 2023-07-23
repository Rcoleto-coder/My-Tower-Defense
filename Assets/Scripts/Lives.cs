using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = PlayerStats.lives.ToString() + " Lives";
    }
}
