using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Analytics : MonoBehaviour
{
    public static Analytics inst;

    [SerializeField] private string titleId;
    private string userId;

    private void Awake()
    {
        PlayFabSettings.TitleId = titleId;

        Login();

        inst = this;

        DontDestroyOnLoad(this);

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            Login();
        }
    }

    private void Login()
    {
        if (!PlayerPrefs.HasKey("Id"))
            PlayerPrefs.SetString("Id", Random.value.ToString());

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CustomId = PlayerPrefs.GetString("Id"),
            CreateAccount = true
        },
        (result) =>
        {
            userId = result.PlayFabId;
            Debug.Log("Playfab login good");
            if (DataManager.instance.data.firstLaunch)
                UpdateLang();
        },
        (error) => { Debug.Log(error.ErrorMessage); });
    }

    public void UpdateLang()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        string lang = ((int)Application.systemLanguage).ToString();
        string gameLang;

        if (PlayerPrefs.HasKey("language")) gameLang = PlayerPrefs.GetInt("language").ToString();
        else gameLang = ((int)Application.systemLanguage).ToString();

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                { "SysLang", lang},
                { "Lang", gameLang}
            }
        },
        (result) =>{},
        (error) =>{});
    }

    public void UpdateRecord(int val)
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                { "Record", val.ToString()}
            }
        },
        (result) => { },
        (error) => { });
    }
}
