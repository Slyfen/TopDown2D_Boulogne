using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneLemming : MonoBehaviour
{

    int currentLemmingCount;

    [SerializeField] int lemmingWinNumber = 7;

    [SerializeField] GameObject canvasWin;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SI C'EST UN LEMMING
        if (collision.gameObject.tag == "Lemming")
        {
            // JE LE DETRUIT
            Destroy(collision.gameObject);

            // J'AUGMENTE MON NOMBRE DE LEMMING QUI A REUSSI
            currentLemmingCount++;

        }

        // SI LE NOMBRE DE LEMMING QUI A REUSSI EST SUPERIEUR AU NOMBRE REQUIS
        if (currentLemmingCount >= lemmingWinNumber)
        {
            // JE GAGNE
            canvasWin.SetActive(true);
        }
    }
}
