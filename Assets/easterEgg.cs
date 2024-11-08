using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easterEgg : MonoBehaviour
{
    public UppgradeManager uppgrademanager;
    private KeyCode[] Code = new KeyCode[]
    {
        KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };

    private int currentIndex = 0;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(Code[currentIndex]))
            {
                currentIndex++;
                if (currentIndex == Code.Length)
                {
                    uppgrademanager.Money = 99999999;
                    currentIndex = 0; // Reset for future inputs
                }
            }
            else
            {
                currentIndex = 0; // Reset if the wrong key is pressed
            }
        }
    }
}
