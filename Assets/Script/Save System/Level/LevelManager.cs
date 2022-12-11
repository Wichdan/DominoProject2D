using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<Level> levelData = new List<Level>();
    //public int levelTerbuka;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Level Data");

        if(objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
}

[System.Serializable]
public class Level
{
    public string nama;
    public List<Data> stages = new List<Data>();
    public int maksBintang;
    public int totalSemuaBintang;
    public int stageTerbuka;
}

[System.Serializable]
public class Data
{
    public string nama;
    public int[] bintang;
    public bool isClear;
    //public int totalBintang;
}

[System.Serializable]
public class Gambar
{
    public string nama;
    public List<DataGambar> stages = new List<DataGambar>();
}

[System.Serializable]
public class DataGambar
{
    public string nama;
    public Image[] bintang;
}
