using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveLevelManager(LevelManager levelManager)
    {
        Debug.Log("Save berhasil, YEY");
        BinaryFormatter bf = new BinaryFormatter();

        string path = Application.persistentDataPath + "/LevelData.datariq";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelManager);

        bf.Serialize(stream, data);
        stream.Close();

    }

    public static LevelData LoadLevelManager()
    {
        string path = Application.persistentDataPath + "/LevelData.datariq";
        if (File.Exists(path))
        {
            Debug.Log("LOAD BERHASIL, YEY!");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = bf.Deserialize(stream) as LevelData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
