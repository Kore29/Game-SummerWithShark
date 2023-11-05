using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bucle());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Bucle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 11));
            GetComponent<Light>().intensity = 10;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Light>().intensity = 33;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Light>().intensity = 10;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Light>().intensity = 33;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Light>().intensity = 10;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Light>().intensity = 33;
        }

    }
   
}
