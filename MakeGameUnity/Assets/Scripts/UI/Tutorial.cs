using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Tutorial : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GameObject.Find("Button").GetComponent<Button>();
        if (button.interactable)
            button.interactable = false;
        else
            button.interactable = true;
    }

    void Update()
    {
        
    }
}
