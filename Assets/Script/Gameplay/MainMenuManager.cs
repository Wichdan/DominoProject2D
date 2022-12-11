using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] List<Gambar> dataGambar = new List<Gambar>();
    [SerializeField] List<Tombol> dataTombolStages = new List<Tombol>();
    //[SerializeField] Button[] dataTombolLevel;
    [SerializeField] GameObject[] levelPanel;

    [SerializeField] List<TextMeshProUGUI> teksTotalBintang = new List<TextMeshProUGUI>();
    [SerializeField] int levelTerpilih = 1;

    private void Start()
    {
        levelManager = GameObject.Find("Level Data").GetComponent<LevelManager>();
        SaveData();
        LoadData();
        WarnaBintang();

        for (int i = 0; i < levelManager.levelData.Count; i++)
        {
            for (int j = 0; j < levelManager.levelData[i].stageTerbuka; j++)
            {
                dataTombolStages[i].stageButton[j].interactable = true;
            }
            teksTotalBintang[i].text = "Total Bintang: " + levelManager.levelData[i].totalSemuaBintang + " / " +
            levelManager.levelData[i].maksBintang;
        }
        levelTerpilih = PlayerPrefs.GetInt("LevelTerpilih");

        for (int i = 0; i < levelPanel.Length; i++)
        {
            if(levelTerpilih == i + 1)
                levelPanel[i].SetActive(true);
            else
                levelPanel[i].SetActive(false);
        }
    }

    public void SaveData()
    {
        SaveSystem.SaveLevelManager(levelManager);
    }

    public void LoadData()
    {
        LevelData data = SaveSystem.LoadLevelManager();
        levelManager.levelData = data.dataLevel;
        WarnaBintang();
    }

    public void ResetData()
    {
        for (int i = 0; i < levelManager.levelData.Count; i++)
        {
            for (int j = 0; j < levelManager.levelData[i].stages.Count; j++)
            {
                for (int k = 0; k < levelManager.levelData[i].stages[j].bintang.Length; k++)
                {
                    levelManager.levelData[i].stages[j].bintang[k] = 0;
                }
                levelManager.levelData[i].stages[j].isClear = false;
            }
            levelManager.levelData[i].stageTerbuka = 1;
            levelManager.levelData[i].maksBintang = 21;
            levelManager.levelData[i].totalSemuaBintang = 0;
        }
        SaveSystem.SaveLevelManager(levelManager);
        Scene nameScane = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneName: nameScane.name);
        PlayerPrefs.DeleteAll();
    }

    void WarnaBintang()
    {
        for (int i = 0; i < levelManager.levelData.Count; i++)
        {
            for (int j = 0; j < levelManager.levelData[i].stages.Count; j++)
            {
                for (int k = 0; k < levelManager.levelData[i].stages[j].bintang.Length; k++)
                {
                    if (levelManager.levelData[i].stages[j].bintang[k] == 1)
                        dataGambar[i].stages[j].bintang[k].color = Color.yellow;
                    else
                        dataGambar[i].stages[j].bintang[k].color = Color.white;
                }
            }
        }
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetCurrentLevel(int i)
    {
        PlayerPrefs.SetInt("LevelTerpilih", i);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class Tombol
{
    public string nama;
    public Button[] stageButton;
}
