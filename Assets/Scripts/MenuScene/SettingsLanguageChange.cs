using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsLanguageChange : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Button btn;
    [SerializeField] private Image image;
    [SerializeField] private Button backBtn;

    [SerializeField] private GameObject menu, stgs;

    private int[] indexes = new int[] { 30, 34, 10};

    private int index;

    private void Awake()
    {
        btn.onClick.AddListener(ChangeLanguage);

        if (PlayerPrefs.HasKey("language"))
        {
            switch (PlayerPrefs.GetInt("language"))
            {
                case 30: 
                    image.sprite = sprites[0];
                    index = 0;
                    break;
                case 34:
                    image.sprite = sprites[1];
                    index = 1;
                    break;
                case 10:
                    image.sprite = sprites[2];
                    index = 2;
                    break;
            }
        }

        backBtn.onClick.AddListener(() =>
        {
            stgs.SetActive(false);
            menu.SetActive(true);
        });
    }

    private void ChangeLanguage()
    {
        index = index + 1 >= sprites.Length ? 0 : index + 1;

        image.sprite = sprites[index];

        PlayerPrefs.SetInt("language", indexes[index]);

        backBtn.onClick.RemoveAllListeners();

        backBtn.onClick.AddListener(() =>
        {
            TranslateManager.inst.Reload();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }
}
