using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypePasscode : MonoBehaviour
{
    public Text fromKeyboard;
    public string password;

    void Start()
    {
        fromKeyboard = GetComponent<Text>();
    }

    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (fromKeyboard.text.Length != 0)
                {
                    fromKeyboard.text = fromKeyboard.text.Substring(0, fromKeyboard.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                password = fromKeyboard.text;
                //print(password);
            }
            else
            {
                fromKeyboard.text += c;
            }
        }
    }
}