using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJump : MonoBehaviour
{
    private PlayerController script;
    private KillPlayer killbox;
    public float fadeOutWaitTime = 3f;
    public float timeToFadeOut = 3f;
    private AudioSource sound;
    private GameController gameController;

    public GameObject clearScreen, blackScreen;
    // Start is called before the first frame update
    void Awake()
    {
        //killbox = GetComponent<KillPlayer>();
        //killbox.enabled = false;
        sound = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            script = other.GetComponent<PlayerController>();
            script.cam.ChangeCam();
            script.suspendControls = true;
            gameController.music[2].volume = 0f;
            sound.Play();
            StartCoroutine("Wait");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(fadeOutWaitTime);
        Instantiate(clearScreen);
        yield return new WaitForSeconds(timeToFadeOut);
        Instantiate(blackScreen);
        script.hp.currentHealth = 0;
        


    }
}
