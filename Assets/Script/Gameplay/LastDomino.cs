using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDomino : MonoBehaviour
{
    public bool gameBerakhir = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            gameBerakhir = true;
        }
    }
}
