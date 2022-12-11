using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoAndPreset : MonoBehaviour
{
    [SerializeField] GameObject[] dominoPreset, domino;

    private void Start()
    {
        dominoPreset = GameObject.FindGameObjectsWithTag("Domino Preset");
        domino = GameObject.FindGameObjectsWithTag("Domino");
    }


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < dominoPreset.Length; i++)
            {
                if (dominoPreset[i].tag == "Domino Preset")
                {
                    Debug.Log("Ini Preset!");
                    dominoPreset[i].SetActive(false);
                    domino[i].SetActive(true);
                }
            }

            for (int i = 0; i < domino.Length; i++)
            {
                if (domino[i].tag == "Domino")
                {
                    Debug.Log("INI DOMINO");
                    domino[i].SetActive(false);
                    dominoPreset[i].SetActive(true);
                }
            }
        }
    }
}
