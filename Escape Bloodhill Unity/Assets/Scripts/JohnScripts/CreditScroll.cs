using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScroll : MonoBehaviour
{
    public GameObject credits;
    public float creditsOver;
    public float multiplier;
    private float scrollTime;
    public Vector3 target;
    public string mainMenu;
    private float tolerance;

    // Start is called before the first frame update
    void Start()
    {
        scrollTime = creditsOver * multiplier;
        target = new Vector3(credits.transform.position.x, -((credits.transform.position.y - gameObject.transform.position.y)*2), 0);
        tolerance = scrollTime * Time.deltaTime;
    }

    private void Awake()
    {
        creditsOver = creditsOver + Time.time;
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
            SceneManager.LoadScene(mainMenu);
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