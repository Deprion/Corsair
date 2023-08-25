using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Sprite on, off;

    [SerializeField] private ChoiceBtn choice1, choice2;

    [SerializeField] private HarborManager harbor;

    private string toBuy = string.Empty;

    private void Awake()
    {
        confirmBtn.onClick.AddListener(BuyShip);
    }

    private void SetShip(Button btn, string name)
    {
        if (DataManager.instance.data.Money < 500) return;

        choice2.GetComponent<Image>().sprite = off;
        choice1.GetComponent<Image>().sprite = off;

        btn.GetComponent<Image>().sprite = on;

        toBuy = name;
    }

    private void BuyShip()
    {
        if (toBuy == string.Empty) return;

        harbor.UpgradeShip(DataHolder.inst.GetByName(toBuy));

        choice2.GetComponent<Image>().sprite = off;
        choice1.GetComponent<Image>().sprite = off;
        toBuy = string.Empty;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ShipData ship = DataManager.instance.data.harbor.GetThis();

        choice1.GetComponent<Button>().onClick.AddListener(() =>
        SetShip(choice1.GetComponent<Button>(), ship.LvlUpShips[0]));

        choice2.GetComponent<Button>().onClick.AddListener(() =>
        SetShip(choice2.GetComponent<Button>(), ship.LvlUpShips[1]));

        choice1.Setup(DataHolder.inst.GetByName(ship.LvlUpShips[0]));
        choice2.Setup(DataHolder.inst.GetByName(ship.LvlUpShips[1]));
    }
}
