using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundButton : MonoBehaviour
{
    public Transform puntoRuta;
    public int indicePuntoActual;
    GameObject currentView;
    GameObject aView;
    public Image whiteScreen;
    public float flashDuration = 0.1f;
    public AudioSource cameraAudio;

    void Start()
    {

    }

    void Update()
    {
        currentView = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;
        aView = GameObject.Find("Enemie1").GetComponent<AI_Enemie1>().puntosRuta[GameObject.Find("Enemie1").GetComponent<AI_Enemie1>().indicePuntoActual].gameObject;
    }

    public void Sound()
    {
        if (currentView.name == aView.name)
        {
            StartCoroutine(Animation());
        }
    }

    private IEnumerator Animation()
    {
        whiteScreen.gameObject.SetActive(true);
        cameraAudio.Play();

        whiteScreen.color = new Color(1f, 1f, 1f, 0f);

        yield return new WaitForSeconds(0.1f);

        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / flashDuration);
            whiteScreen.color = new Color(1f, 1f, 1f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        GameObject.Find("Enemie1").GetComponent<AI_Enemie1>().indicePuntoActual = 0;
        GameObject.Find("Enemie1").transform.position = new Vector3(puntoRuta.position.x, GameObject.Find("Enemie1").transform.position.y, puntoRuta.position.z);
        elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / flashDuration);
            whiteScreen.color = new Color(1f, 1f, 1f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        whiteScreen.gameObject.SetActive(false);
    }
}