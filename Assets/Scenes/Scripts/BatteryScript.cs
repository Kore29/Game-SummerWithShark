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

    public GameObject CamerasUI;

    public TextMeshProUGUI batteryText;

    public GameObject TVCube;
    public GameObject RoomLight;
    public GameObject LightSphere;

    public GameObject LampLight;

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
            TVCube.SetActive(false);
            RoomLight.SetActive(false);
            LampLight.SetActive(false);
            LightSphere.SetActive(false);
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
