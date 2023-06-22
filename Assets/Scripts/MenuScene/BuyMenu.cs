using UnityEngine;
using UnityEngine.UI;

public class BuyMenu : MonoBehaviour
{
    [SerializeField] private Sprite on, off;
    [SerializeField] private Button fregate, drakkar, galleon, privateer;
    [SerializeField] private Button buyBtn;

    [SerializeField] private HarborManager harb;

    private string toBuy = string.Empty;

    private void Awake()
    {
        fregate.onClick.AddListener(() => SetShip(fregate, "Фрегат"));
        drakkar.onClick.AddListener(() => SetShip(drakkar, "Драккар"));
        galleon.onClick.AddListener(() => SetShip(galleon, "Галеон"));
        privateer.onClick.AddListener(() => SetShip(privateer, "Капер"));

        buyBtn.onClick.AddListener(BuyShip);
    }

    private void SetShip(Button btn, string name)
    {
        if (DataManager.instance.data.Money < 200) return;

        fregate.GetComponent<Image>().sprite = off;
        drakkar.GetComponent<Image>().sprite = off;
        galleon.GetComponent<Image>().sprite = off;
        privateer.GetComponent<Image>().sprite = off;

        btn.GetComponent<Image>().sprite = on;

        toBuy = name;
    }

    private void BuyShip()
    {
        if (toBuy == string.Empty) return;

        harb.AddShip(DataHolder.inst.GetByName(toBuy));
        fregate.GetComponent<Image>().sprite = off;
        drakkar.GetComponent<Image>().sprite = off;
        galleon.GetComponent<Image>().sprite = off;
        privateer.GetComponent<Image>().sprite = off;
        toBuy = string.Empty;
        gameObject.SetActive(false);
    }
}
