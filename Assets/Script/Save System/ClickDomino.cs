using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDomino : MonoBehaviour
{
    [SerializeField] GameObject dominoPreset;
    [SerializeField] bool isSudahClick;
    [SerializeField] StageManager stageManager;

    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    private void OnMouseDown()
    {
        if (this.tag == "Domino" && !isSudahClick && stageManager.isStartGame == false)
        {
            isSudahClick = true;
            this.gameObject.SetActive(false);
            dominoPreset.SetActive(true);
            //Debug.Log("Domino" + gameObject.name);
            //GameObject clone;
            //clone = Instantiate(dominoPreset, transform.position, transform.rotation);
            NgeClick();
            stageManager.jumlahDomino--;
        }
    }

    void NgeClick()
    {
        isSudahClick = !isSudahClick;
    }
}
