using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Oculta el cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtiene la posici�n del cursor en la pantalla
        Vector3 cursorPosition = Input.mousePosition;

        // Convierte la posici�n del cursor de pantalla a espacio de lienzo de UI
        cursorPosition.z = -Camera.main.transform.position.z;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        // Establece la posici�n de la imagen igual a la posici�n del cursor
        transform.position = cursorPosition;
    }
}
