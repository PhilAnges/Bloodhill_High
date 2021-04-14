using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameController.ChangeMusic(1, 100f, true);
    }

    private void OnTriggerExit(Collider other)
    {
        gameController.ChangeMusic(0, 100f, true);
    }
}
