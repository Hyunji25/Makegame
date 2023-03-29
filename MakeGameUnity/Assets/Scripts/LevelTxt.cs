using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTxt : MonoBehaviour
{
    public Text scriptTxt;
    private int Plevel;
    private EXPBarController expBar;

    void Start()
    {
        scriptTxt.text = "0";
    }

    void Update()
    {
        expBar = GameObject.Find("Slider").GetComponent<EXPBarController>();

        if (expBar != null)
        {
            Plevel = expBar.level;
        }
        scriptTxt.text = Plevel.ToString();
    }
}
