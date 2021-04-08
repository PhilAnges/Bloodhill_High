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
    public float[] volumes;

    public int basementPhase = 0;

    public float fadeSpeed;

    void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Instantiate(playerPrefab, transform.position, transform.rotation);
        }
        if (enemyActive && !GameObject.FindGameObjectWithTag("Enemy"))
        {
            SpawnEnemy();
        }
        music = GetComponents<AudioSource>();
        volumes = new float[3];

        for (int i = 0; i < music.Length; i++)
        {
            volumes[i] = music[i].volume;
        }

        music[1].volume = 0f;
        music[1].Play();
        ChangeMusic(0, volumes[0]);
    }

    void Update()
    {

    }

    public void SpawnEnemy(Vector3 spawnPosition, Path path)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<AIController>().SetPath(path);
    }

    public void ChangeMusic(int songIndex, float targetVolume)
    {

        for (int i = 0; i < music.Length; i++)
        {
            if (i != songIndex)
            {
                StartCoroutine(FadeOut(i, true, targetVolume));
            }
        }
        StartCoroutine(FadeIn(songIndex, false, targetVolume));
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

    IEnumerator FadeOut(int songIndex, bool playing, float targetVolume)
    {
        if (music[songIndex].volume > 0f)
        {
            music[songIndex].volume -= fadeSpeed;
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(FadeOut(songIndex, true, targetVolume));
        }
        else if (songIndex != 1)
        {
            music[songIndex].Stop();
        }
    }

    IEnumerator FadeIn(int songIndex, bool playing, float targetVolume)
    {
        Debug.Log("Starting FadeIn");
        if (!playing)
        {
            music[songIndex].Play();
        }

        if (music[songIndex].volume < volumes[songIndex])
        {
            music[songIndex].volume += fadeSpeed;
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(FadeIn(songIndex, true, targetVolume));
        }
    }

}
