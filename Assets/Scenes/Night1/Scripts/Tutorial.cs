using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public float tutorial = 0;
    GameObject currentView;
    public TextMeshProUGUI tutorialText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TutorialCheck());
    }

    IEnumerator TutorialCheck()
    {
        currentView = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;
        if (tutorial == 0)
        {
            tutorialText.text = "Press D to see the right side.";
            if (currentView.name == "RightView")
            {
                yield return new WaitForSeconds(3f);
                tutorial = 1;
            }
        }
        if (tutorial == 1)
        {
            tutorialText.text = "Hold down the F key to turn on the flashlight.";
            if (Input.GetKeyDown(KeyCode.F))
            {
                yield return new WaitForSeconds(3f);
                tutorial = 2;
            }
        }
        if (tutorial == 2) {
            tutorialText.text = "If you hear a breathing, close the door by pressing space.";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("uwu");
                yield return new WaitForSeconds(3f);
                tutorial = 3;
            }
        }
        if (tutorial == 3)
        {
            tutorialText.text = "Now go to the left side by pressing A twice.";
            if (currentView.name == "LeftView")
            {
                yield return new WaitForSeconds(3f);
                tutorial = 4;
            }
        }
        if (tutorial == 4)
        {
            tutorialText.text = "Now go to the left side by pressing A twice.";
            if (currentView.name == "LeftView")
            {
                yield return new WaitForSeconds(3f);
                tutorial = 5;
            }
        }
        if (tutorial == 5)
        {
            tutorialText.text = "Turn on the flashlight and if there is someone there, hide under the table by pressing D to go back, and then S to hide under the table.";
            if (currentView.name == "DeskView")
            {
                yield return new WaitForSeconds(3f);
                tutorial = 6;
            }
        }
        if (tutorial == 6)
        {
            tutorialText.text = "If he's already gone, press W to get up.";
            if (currentView.name == "FrontView")
            {
                yield return new WaitForSeconds(3f);
                tutorial = 7;
                tutorialText.text = "Tutorial completado.";
            }
        }
    }
}
