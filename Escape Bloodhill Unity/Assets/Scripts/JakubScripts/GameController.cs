using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool playerIsSafe = false;
    public bool enemyActive = false;
    public bool enemyStartIdle = true;
    public bool playerIsHidden = false;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;
    public Path enemyStartingPath;
    public AIController enemyScript;

    public AudioSource ambientMusic, tenseMusic, chaseMusic;
    public AudioSource[] music;

    public int basementPhase = 0;

    void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        if (enemyActive && !GameObject.FindGameObjectWithTag("Enemy"))
        {
            SpawnEnemy();
        }
        music = GetComponents<AudioSource>();
        music[0].Play();
    }

    void Update()
    {

    }

    public void SpawnEnemy(Vector3 spawnPosition, Path path)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<AIController>().SetPath(path);
    }

    public void ChangeMusic(int songIndex)
    {
        foreach (AudioSource song in music)
        {
            song.Stop();
        }

        music[songIndex].Play();
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        enemyScript = enemy.GetComponent<AIController>();
        enemyScript.SetPath(enemyStartingPath);
        AIState startState;
        if (enemyStartIdle)
        {
            startState = new IdleState(enemyScript);
        }
        else
        {
            startState = new PatrolState(enemyScript);
        }

        enemyScript.SetState(startState);
    }
}
