using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TranslateManager : MonoBehaviour
{
    public static TranslateManager inst;
    private Dictionary<string, string> translate = new Dictionary<string, string>();

    private void Awake()
    {
        DontDestroyOnLoad(this);

        inst = this;
    }

    private void Start()
    {
        AddressableLoader.Loaded.AddListener(Setup);
    }

    private void Setup()
    {
        if (PlayerPrefs.HasKey("language"))
        {
            GetLanguage(PlayerPrefs.GetInt("language"));

            AddressableLoader.Loaded.RemoveListener(Setup);

            return;
        }

        GetLanguage((int)Application.systemLanguage);

        PlayerPrefs.SetInt("language", (int)Application.systemLanguage);

        AddressableLoader.Loaded.RemoveListener(Setup);
    }

    public void Reload()
    {
        Setup();

        Analytics.inst.UpdateLang();
    }

    private void GetLanguage(int num)
    {
        switch (num)
        {
            case 30:
                Fill(AddressableLoader.inst.GetLanguage("Russian"));
                break;
            case 34:
                Fill(AddressableLoader.inst.GetLanguage("Spanish"));
                break;
            case 10:
                Fill(AddressableLoader.inst.GetLanguage("English"));
                break;
            default: 
                Fill(AddressableLoader.inst.GetLanguage("English"));
                break;
        }
    }

    private void Fill(TextAsset txt)
    {
        Regex regex = new Regex(":|;\r?\n?");
        var arr = regex.Split(txt.text);

        for (int i = 0; i < arr.Length - 1; i+=2)
        {
            translate[arr[i]] = arr[i + 1];
        }
    }

    public string GetText(string text)
    {
        return translate[text];
    }
}
