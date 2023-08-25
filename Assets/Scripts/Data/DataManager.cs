using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private string path;

    public Data data { get; private set; }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        path = Application.persistentDataPath + "/Data.dat";
        LoadData();

        Events.Balance.Invoke(data.Money);
    }

    private void LoadData()
    {
        if (!File.Exists(path))
        {
            data = new Data();
            return;
        }

        data = JsonConvert.DeserializeObject<Data>(File.ReadAllText(path));

        data.firstLaunch = false;

        if (data.version < 1)
        {
            data.version = 1.1;
            data.Money += 400;
        }
    }

    private void SaveData()
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(data));
    }

    public class Data
    {
        public Harbor harbor;
        public int Money;
        public double version;
        public bool firstLaunch;

        public Data()
        {
            harbor = new Harbor(DataHolder.inst.Ships[0]);
            Money = 0;
            version = 1.1;
            firstLaunch = true;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
