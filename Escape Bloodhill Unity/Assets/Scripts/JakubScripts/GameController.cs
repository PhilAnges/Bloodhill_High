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

    public int basementPhase = -1;

    public float fadeSpeed;

    public int currentBackground = 4;

    public bool basement = true;
    public bool ambientStarted = false;
    public bool basementStarted = false;

    public bool paused = false;

    private void Start()
    {
        Time.timeScale = 1;

    }

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
        volumes = new float[4];

        for (int i = 0; i < music.Length; i++)
        {
            volumes[i] = music[i].volume;
        }

        music[currentBackground].Play();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
    }

    public void SpawnEnemy(Vector3 spawnPosition, Path path)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<AIController>().SetPath(path);
    }

    public void ChangeMusic(int songIndex, float targetVolume, bool fade)
    {
        if (songIndex != 3)
        {
            basementStarted = false;
        }
        else
        {
            basementStarted = true;
        }
        if (songIndex != 0)
        {
            ambientStarted = false;
        }
        else
        {
            ambientStarted = false;
        }

        //Debug.Log("Changing music to index " + songIndex);
        for (int i = 0; i < music.Length; i++)
        {
            if (i != songIndex)
            {
                StartCoroutine(FadeOut(i, false));
            }
        }
        if (fade)
        {
            StartCoroutine(FadeIn(songIndex, false));
        }
        else
        {
            music[songIndex].volume = volumes[songIndex];
            music[songIndex].Play();
        }
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

    IEnumerator FadeOut(int songIndex, bool playing)
    {
        //Debug.Log("Starting FadeOut");
        if (music[songIndex].volume > 0f)
        {
            music[songIndex].volume -= fadeSpeed;
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(FadeOut(songIndex, true));
        }
        else if (songIndex != 1)
        {
            music[songIndex].Stop();
        }
    }

    IEnumerator FadeIn(int songIndex, bool playing)
    {
        //Debug.Log("Starting FadeIn");
        if (!playing)
        {
            music[songIndex].Play();
        }

        if (music[songIndex].volume < volumes[songIndex])
        {
            music[songIndex].volume += fadeSpeed;
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(FadeIn(songIndex, true));
        }
    }

}
