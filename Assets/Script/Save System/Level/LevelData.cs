using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public List<Level> dataLevel = new List<Level>();
    //public int unlockedStages;

    public LevelData(LevelManager levelManager)
    {
        dataLevel = levelManager.levelData;
        //unlockedStages = levelManager.levelTerbuka;
    }
}
