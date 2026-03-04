using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public int _bestScore;
    public List<int> matchedCards;
}
public class SaveManager : MonoBehaviour
{
    string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/save.json";
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public SaveData Load()
    {
        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);

        return JsonUtility.FromJson<SaveData>(json);
    }
}