using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab, ship, coin;
    [SerializeField] private Cannon cannon;

    [SerializeField] private GameObject endMenu;
    [SerializeField] private Button restart, exit, adBtn;

    [SerializeField] private TMP_Text endText;

    private int coinCollected, expToAdd;

    private int coinLeft = 16;

    private void Awake()
    {
        ship = Instantiate(prefab);
        ship.GetComponent<Ship>().Setup(DataHolder.inst.GetByName(Global.Name));

        Events.AddCoin.AddListener(CoinRemove);
        Events.End.AddListener(End);

        restart.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        exit.onClick.AddListener(() => SceneManager.LoadScene("MenuScene"));

        cannon.Setup(ship);

        SpawnCoins();

        AdManager.inst.AdReward += AdReward;

        adBtn.onClick.AddListener(AdManager.inst.ShowRewardedAd);
    }

    private void AdReward()
    {
        endText.text = $"{TranslateManager.inst.GetText("coinsGained")} : {coinCollected * 2}(2X)" +
            $"\n{TranslateManager.inst.GetText("expGained")} : {expToAdd * 2}(2X)";

        adBtn.gameObject.SetActive(false);

        DataManager.instance.data.Money += coinCollected;

        DataManager.instance.data.harbor.GetThis().AddExp(expToAdd);
    }

    private void End()
    {
        if (coinCollected > 16)
        {
            adBtn.gameObject.SetActive(true);

            expToAdd = (int)Random.Range(coinCollected * 0.25f, coinCollected * 0.75f);

            DataManager.instance.data.harbor.GetThis().AddExp(expToAdd);
        }

        endText.text = $"{TranslateManager.inst.GetText("coinsGained")} : {coinCollected}" +
            $"\n{TranslateManager.inst.GetText("expGained")} : {expToAdd}";

        DataManager.instance.data.Money += coinCollected;

        endMenu.SetActive(true);
    }

    private void CoinRemove()
    {
        coinCollected++;
        coinLeft--;

        if (coinLeft > 0) return;

        coinLeft = 16;
        Events.Level.Invoke();

        SpawnCoins();
    }

    private void SpawnCoins()
    {
        float x = 0;
        float y = 0;
        float angle = 0;

        float radius = 5;

        float step = (360 / Mathf.Rad2Deg) / 16;

        for (int i = 1; i <= 16; i++)
        {
            angle = step * i;

            x = Mathf.Cos(angle) * radius;
            y = Mathf.Sin(angle) * radius;

            var obj = Instantiate(coin);
            obj.transform.position = new Vector2(x, y);
        }
    }

    private void OnDestroy()
    {
        Events.AddCoin.RemoveListener(CoinRemove);
        Events.End.RemoveListener(End);
        Events.Balance.Invoke(DataManager.instance.data.Money);
        AdManager.inst.AdReward -= AdReward;
    }
}
