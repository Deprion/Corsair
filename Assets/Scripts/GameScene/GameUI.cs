using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject HPPrefab;
    [SerializeField] private TMP_Text balance, level;

    private int counter = 0;
    private int levelTxt = 1;

    private void Awake()
    {
        balance.text = $"{TranslateManager.inst.GetText("coins")}: {counter}";
        level.text = $"{TranslateManager.inst.GetText("level")}: {levelTxt}";
        Events.AddCoin.AddListener(AddCoin);
        Events.Level.AddListener(Level);
        Events.GetHit.AddListener(Hit);

        for (int i = 0; i < DataManager.instance.data.harbor.GetThis().HP; i++)
        {
            Instantiate(HPPrefab, parent, false);
        }
    }

    private void Hit()
    {
        Destroy(parent.GetChild(parent.childCount - 1).gameObject);
    }

    private void AddCoin()
    {
        counter++;
        balance.text = $"{TranslateManager.inst.GetText("coins")}: {counter}";
    }

    private void Level()
    {
        levelTxt++;
        level.text = $"{TranslateManager.inst.GetText("level")}: {levelTxt}";

        if (PlayerPrefs.GetInt("Lvl") < levelTxt)
        {
            Analytics.inst.UpdateRecord(levelTxt);
            PlayerPrefs.SetInt("Lvl", levelTxt);
        }
    }

    private void OnDestroy()
    {
        Events.AddCoin.RemoveListener(AddCoin);
        Events.Level.RemoveListener(Level);
        Events.GetHit.RemoveListener(Hit);
    }
}
