using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDominoPreset : MonoBehaviour
{
    [SerializeField] GameObject domino;
    public bool isSudahClick;
    [SerializeField] StageManager stageManager;

    private void Start() {
        stageManager = FindObjectOfType<StageManager>();
    }

    private void OnMouseDown()
    {
        if (this.tag == "Domino Preset" && !isSudahClick)
        {
            isSudahClick = true;
            this.gameObject.SetActive(false);
            domino.SetActive(true);
            //Debug.Log("Domino" + gameObject.name);
            //GameObject clone;
            //clone = Instantiate(domino, transform.position, transform.rotation);
            NgeClick();
            stageManager.jumlahDomino++;
        }
    }

    void NgeClick()
    {
        isSudahClick = !isSudahClick;
    }
}
