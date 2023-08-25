using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceBtn : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;

    public void Setup(ShipSO ship)
    {
        image.sprite = ship.Sprite;
        text.text = $"{TranslateManager.inst.GetText(ship.Name)}\n" +
            $"{TranslateManager.inst.GetText("hp")}: {ship.HP}\n" +
            $"{TranslateManager.inst.GetText("speed")}: {ship.BaseSpeed * 100}";
    }
}
