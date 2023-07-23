using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;

    public Wave [] enemiesArray = null;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5.0f;
    private float countdown = 2.0f;
    

    public Text waveCountdownText;

    private int waveIndex = 0;

    public GManager gameManager;

    void Start()
    {
        EnemiesAlive = 0;
    }

    void Update()
    {
        if(EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == enemiesArray.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
            return;
        }

        if (countdown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0.0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
       
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        Wave wave = enemiesArray[waveIndex];
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1.0f / wave.rate);
        } 
        
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position,spawnPoint.rotation);
        
    }
}
