using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipInfoUpgrade : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameTxt, descTxt;

    public void Setup(ShipData data)
    {
        image.sprite = data.Sprite;
        nameTxt.text = data.Name;
        descTxt.text = data.LvlUpStr;
    }
}
