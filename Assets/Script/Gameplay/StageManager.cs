using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [Header("Pengaturan Stage")]
    [SerializeField] float timer;
    [SerializeField] float kondisiTimer;
    [SerializeField] int ketentuanDomino;
    public int jumlahDomino;

    [Header("Kondisi")]
    [SerializeField] bool isEndGame;
    [SerializeField] bool isStageClear;
    public bool isPaused, isStartGame;
    [SerializeField] List<StageCondition> stageConditions = new List<StageCondition>();

    LastDomino lastDomino;
    [Header("Referensi Object")]
    [SerializeField] GameObject dominoAkhir, dominoObstacle;
    [SerializeField] GameObject endGameUI, pauseButton, mulaiButton, resetButton, homeButton;
    [SerializeField] TextMeshProUGUI[] teksKondisi, indikator;
    [SerializeField] TextMeshProUGUI teksTimer, teksJmlDomino;
    [SerializeField] Image[] imgBintang;
    [SerializeField] LevelManager levelManager;

    [Header("Buat Save")]
    [SerializeField] int[] dataBintang = { 0, 0, 0 };
    [SerializeField] int currentLevel, currentStage;

    [Header("Domino")]
    [SerializeField] GameObject[] dominoPresets;

    private void Start()
    {
        levelManager = GameObject.Find("Level Data").GetComponent<LevelManager>();
        dominoAkhir = GameObject.FindGameObjectWithTag("Domino Akhir");
        lastDomino = dominoAkhir.GetComponent<LastDomino>();


        for (int i = 0; i < stageConditions.Count; i++)
        {
            teksKondisi[i].text = stageConditions[i].namaKondisi;
        }
        dominoPresets = GameObject.FindGameObjectsWithTag("Domino Preset");

        if (levelManager.levelData[currentLevel - 1].stages[currentStage - 1].isClear == true)
        {
            for (int a = 0; a < levelManager.levelData[currentLevel - 1].stages[currentStage - 1].bintang.Length; a++)
            {
                if (levelManager.levelData[currentLevel - 1].stages[currentStage - 1].bintang[a] == 1)
                {
                    indikator[a].text = "SELESAI";
                    imgBintang[a].color = Color.yellow;
                }
                else
                    continue;
            }
        }
    }

    private void Update()
    {
        teksJmlDomino.text = "x " + jumlahDomino.ToString("00");
        if (!isStartGame) return;

        if (isEndGame)
        {
            ENDGame();
            endGameUI.SetActive(true);
            pauseButton.SetActive(false);
            mulaiButton.SetActive(false);
            resetButton.SetActive(false);
            homeButton.SetActive(false);
            return;
        }

        if (lastDomino.gameBerakhir)
            isEndGame = true;
        else
            isEndGame = false;

        for (int i = 0; i < stageConditions.Count; i++)
        {
            if (stageConditions[i].isUsingTimer)
            {
                timer -= Time.deltaTime;
                int pembulatan = Mathf.FloorToInt(timer);
                teksTimer.text = pembulatan.ToString();

                if (timer <= 0)
                    timer = 0;
            }

        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        mulaiButton.SetActive(false);
        
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        mulaiButton.SetActive(true);
        
    }

    public void ResetStage()
    {
        Scene nameScane = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneName: nameScane.name);
    }

    public void ChangeScene(int i)
    {
        SaveStage();
        SceneManager.LoadScene(i);
    }

    public void mulaiDomino(GameObject domino)
    {
        domino.GetComponent<Rigidbody2D>().simulated = true;

        if (dominoObstacle != null)
            dominoObstacle.GetComponent<Rigidbody2D>().simulated = true;

        for (int i = 0; i < dominoPresets.Length; i++)
        {
            dominoPresets[i].SetActive(false);
        }
        isStartGame = true;
    }

    void ENDGame()
    {
        for (int i = 0; i < stageConditions.Count; i++)
        {
            if ((isEndGame && stageConditions[i].isClearStage) || (stageConditions[i].isUsingTimer && kondisiTimer <= timer)
            || (stageConditions[i].isCertainDomino && jumlahDomino <= ketentuanDomino))
                stageConditions[i].isClear = true;

            if (stageConditions[i].isClear)
            {
                dataBintang[i] = 1;
                indikator[i].text = "SELESAI";
                imgBintang[i].color = Color.yellow;
            }
        }
        levelManager.levelData[currentLevel - 1].stages[currentStage - 1].isClear = true;
    }

    void SaveStage()
    {
        levelManager.levelData[currentLevel - 1].totalSemuaBintang = 0;
        if (levelManager.levelData[currentLevel - 1].stages[currentStage - 1].isClear)
        {
            int temp = currentStage;
            if (temp <= levelManager.levelData[currentLevel - 1].stageTerbuka)
                levelManager.levelData[currentLevel - 1].stageTerbuka = currentStage + 1;
        }
        for (int a = 0; a < levelManager.levelData[currentLevel - 1].stages[currentStage - 1].bintang.Length; a++)
        {
            if (levelManager.levelData[currentLevel - 1].stages[currentStage - 1].bintang[a] == 0)
            {
                levelManager.levelData[currentLevel - 1].stages[currentStage - 1].bintang[a] = dataBintang[a];
                //levelManager.levelData[currentLevel - 1].stages[currentStage - 1].totalBintang += dataBintang[a];
            }
            else
                continue;


        }

        for (int j = 0; j < levelManager.levelData[currentLevel - 1].stages.Count; j++)
        {
            for (int k = 0; k < levelManager.levelData[currentLevel - 1].stages[j].bintang.Length; k++)
            {
                levelManager.levelData[currentLevel - 1].totalSemuaBintang +=
                levelManager.levelData[currentLevel - 1].stages[j].bintang[k];
            }
        }
        SaveSystem.SaveLevelManager(levelManager);
    }
}

[System.Serializable]
public class StageCondition
{
    public string namaKondisi;
    public bool isUsingTimer, isCertainDomino, isClearStage, isClear;
}
