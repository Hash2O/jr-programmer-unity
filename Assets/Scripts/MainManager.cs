using UnityEngine;
using System.IO;

// Singleton pattern
public class MainManager : MonoBehaviour
{
    // Enables to access the MainManager object from any other script.  
    // static : means that the values stored in this class member will be shared by all the instances of that class.
    public static MainManager instance;

    public Color TeamColor;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // this : the current instance of MainManager.
        instance = this;
        DontDestroyOnLoad(gameObject);

        // load the saved color (if one exists) when the application starts
        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
}