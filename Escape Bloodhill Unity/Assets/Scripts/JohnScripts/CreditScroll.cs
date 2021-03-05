using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScroll : MonoBehaviour
{
    public GameObject credits;
    public float creditsOver;

    private float scrollTime;
    public Vector3 target;
    private float tolerance;

    // Start is called before the first frame update
    void Start()
    {
        scrollTime = creditsOver * 10.0f;
        target = new Vector3(credits.transform.position.x, -((credits.transform.position.y - gameObject.transform.position.y)*2), 0);
        tolerance = scrollTime * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
         if(credits.transform.position != target)
        {
            ScrollUp();
        }


        if(Time.time > creditsOver)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void ScrollUp()
    {
        Vector3 headingTo = target - credits.transform.position;
        credits.transform.position += (headingTo / headingTo.magnitude) * scrollTime * Time.deltaTime;
        if(headingTo.magnitude < tolerance)
        {
            credits.transform.position = target;
        }
    }
}
