using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image curShip;
    [SerializeField] private Button playBtn, musicBtn, soundBtn, languageBtn;
    [SerializeField] private TMP_Text balanceTxt, lvlRecordTxt;

    private AudioManager audioManager;

    private void Awake()
    {
        Events.ShipUpdate.AddListener(UpdateShip, true);
        Events.Balance.AddListener(Balance, true);
        playBtn.onClick.AddListener(Play);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        musicBtn.onClick.AddListener(audioManager.MuteMusic);
        soundBtn.onClick.AddListener(audioManager.MuteSound);

        lvlRecordTxt.text = $"Рекорд: {PlayerPrefs.GetInt("Lvl")}";
    }

    private void Play()
    {
        if (Global.Name != string.Empty)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void UpdateShip(ShipData ship)
    {
        if (ship == null)
        {
            curShip.gameObject.SetActive(false);
        }
        else
        {
            curShip.gameObject.SetActive(true);
            curShip.sprite = ship.Sprite;
        }
    }

    private void Balance(int num) => balanceTxt.text = $"Баланс: {num}";

    private void OnDestroy()
    {
        Events.ShipUpdate.RemoveListener(UpdateShip);
        Events.Balance.AddListener(Balance);
    }
}
