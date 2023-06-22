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
            shipName.text = "Пусто";
            shipLvl.text = string.Empty;
            shipExp.text = string.Empty;

            return;
        }

        shipImage.gameObject.SetActive(true);
        shipImage.sprite = ship.Sprite;
        shipName.text = ship.Name;
        shipLvl.text = $"Уровень: {ship.Level}";
        shipExp.text = $"Опыт: {ship.Exp}/{ship.MaxExp}";
    }
}