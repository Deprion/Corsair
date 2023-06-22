using UnityEngine;
using UnityEngine.UI;

public class SettingsBtn : MonoBehaviour
{
    [SerializeField] private Sprite on, off;
    [SerializeField] private string text;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ChangeSprite);
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (PlayerPrefs.GetInt(text) == 1) GetComponent<Image>().sprite = off;
        else GetComponent<Image>().sprite = on;
    }
}
