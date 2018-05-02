using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;     //name of wave
        public Transform enemy; //enemy object to spawn
        public int count;       //count of enemies to spawn
        public float rate;      //rate to spawn enemies into wave
    }

    public Wave[] enemyWaves;   //array that holds all waves
    private int nextWave = 0;   //tracker for next wave

    public float timeBetweenWaves = 3f; //tracks time between wave spawns
    public float waveCountdown;         //countdown until next wave

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;  //default spawnstate

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemiesAreAlive()) //if no enemies are alive
            {
                Debug.Log("Wave Completed");
            }
            else
            {
                return; //skip the spawning wave as this one is not completed
            }
        }

        if(waveCountdown <= 0)  //if it is time to spawn a wave
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(enemyWaves[nextWave]));    //spawn next wave
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    }

    bool EnemiesAreAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;   //no enemies are alive
            }
            searchCountdown = 1f;   //reset countdown
        }
        return true;    //enemies are alive
    }

    IEnumerator SpawnWave (Wave enemies)
    {
        Debug.Log("Spawning Wave");
        state = SpawnState.SPAWNING;    //begin spawning enemies

        for (int i = 0; i < enemies.count; i++) //spawn all of the enemies
        {
            SpawnEnemy(enemies.enemy);  //spawn enemy
            yield return new WaitForSeconds(1f / enemies.rate); //wait to spawn the next enemy
        }

        state = SpawnState.WAITING;     //wait until next spawn cycle

        yield break;
    }

    void SpawnEnemy (Transform thisEnemy)
    {
        Debug.Log("Spawning Enemy");
        Instantiate(thisEnemy, transform.position, transform.rotation, this.transform.parent);  //instantiate enemy
    }
}
