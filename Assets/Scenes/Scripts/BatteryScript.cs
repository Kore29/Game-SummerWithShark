using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BatteryScript : MonoBehaviour
{
    public float EnergiaActual = 100.9f;
    public float EnergiaMinima = 100.9f;
    public float VelocidadConsumo = 0;
    public int Usage = 1;

    public Image batteryIcon;
    public Sprite[] batteryStates;  // Debes asignar las imágenes desde el inspector

    public GameObject FlashlightScript;
    public Light flashlightLight;
    public Light flashlightCamera;

    public TextMeshProUGUI batteryText;

    public GameObject TVCube;

    public GameObject RoomLightGreen;
    public GameObject RoomLightRed;

    public GameObject RoomLightDetras;
    public GameObject LampLight;

    public AudioSource ElectricStatic;
    public AudioSource PowerOuter;

    public AudioClip powerSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnergiaActual -= Time.deltaTime * VelocidadConsumo;
        if (Usage < 1)
        {
            Usage = 1;
        }
        if (EnergiaActual <= 0)
        {
            EnergiaActual = 0;
            flashlightLight.enabled = false;
            FlashlightScript.SetActive(false);

            RoomLightGreen.SetActive(false);
            RoomLightRed.SetActive(true);

            LampLight.SetActive(false);
            RoomLightDetras.SetActive(false);

            PowerOuter.clip = powerSound;
            PowerOuter.time = 0f;
            PowerOuter.Play();
            ElectricStatic.Stop();
        }
        batteryText.text = Mathf.FloorToInt(EnergiaActual).ToString() + "%";

        batteryIcon.sprite = batteryStates[Usage];



        VelocidadConsumo = Usage * 0.1f;
    }

    public void ModifyUsage(bool add)
    {
        if (add)
        {
            Usage = Usage + 1;
        }
        else
        {
            Usage = Usage - 1;
        }
    }


}
