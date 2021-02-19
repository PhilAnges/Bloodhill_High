using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeEnd : MonoBehaviour
{
    public bool nopenope;
    public string playerTag;
    public string escapeSuccessful;

    // Start is called before the first frame update
    void Start()
    {
        nopenope = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if(nopenope == true)
            {
                SceneManager.LoadScene(escapeSuccessful);
            }
        }
    }
}
