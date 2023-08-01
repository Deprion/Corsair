using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text balance, level;

    private int counter = 0;
    private int levelTxt = 1;

    private void Awake()
    {
        balance.text = $"{TranslateManager.inst.GetText("coins")}: {counter}";
        level.text = $"{TranslateManager.inst.GetText("level")}: {levelTxt}";
        Events.AddCoin.AddListener(AddCoin);
        Events.Level.AddListener(Level);
    }

    private void AddCoin()
    {
        counter++;
        balance.text = $"{TranslateManager.inst.GetText("coins")}: {counter}";

        DataManager.instance.data.Money++;
    }

    private void Level()
    {
        levelTxt++;
        level.text = $"{TranslateManager.inst.GetText("level")}: {levelTxt}";

        if (PlayerPrefs.GetInt("Lvl") < levelTxt)
        {
            PlayerPrefs.SetInt("Lvl", levelTxt);
        }
    }

    private void OnDestroy()
    {
        Events.AddCoin.RemoveListener(AddCoin);
        Events.Level.RemoveListener(Level);
    }
}
