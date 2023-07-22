using NUnit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab, ship, coin;
    [SerializeField] private Cannon cannon;

    [SerializeField] private GameObject endMenu;
    [SerializeField] private Button restart, exit;

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
    }

    private void End()
    {
        DataManager.instance.data.harbor.GetThis().AddExp(Random.Range(5, 15));
        endMenu.SetActive(true);
    }

    private void CoinRemove()
    {
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
    }
}
