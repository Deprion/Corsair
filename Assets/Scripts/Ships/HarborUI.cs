using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HarborUI : MonoBehaviour
{
    [SerializeField] private Image shipImage;
    [SerializeField] private TMP_Text shipName, shipLvl, shipExp;

    public void UpdateUI(ShipData ship)
    {
        if (ship == null)
        {
            shipImage.gameObject.SetActive(false);
            shipName.text = TranslateManager.inst.GetText("empty");
            shipLvl.text = string.Empty;
            shipExp.text = string.Empty;

            return;
        }

        shipImage.gameObject.SetActive(true);
        shipImage.sprite = ship.Sprite;
        shipName.text = TranslateManager.inst.GetText(ship.Name);
        shipLvl.text = $"{TranslateManager.inst.GetText("level")}: {ship.Level}";
        shipExp.text = $"{TranslateManager.inst.GetText("exp")}: {ship.Exp}/{ship.MaxExp}";
    }
}