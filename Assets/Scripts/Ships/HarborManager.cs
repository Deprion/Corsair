using UnityEngine;
using UnityEngine.UI;

public class HarborManager : MonoBehaviour
{
    [SerializeField] private Button leftBtn, rightBtn, addBtn, upgradeBtn;
    private ShipData curShip;

    public Harbor harbor;

    private HarborUI harborUI;

    private void Awake()
    {
        harbor = DataManager.instance.data.harbor;

        curShip = harbor.GetFirst();
        Global.Name = curShip.Name;

        Events.ShipUpdate.Invoke(curShip);

        leftBtn.onClick.AddListener(() => Switch(false));
        rightBtn.onClick.AddListener(() => Switch(true));

        if (curShip != null)
        {
            addBtn.gameObject.SetActive(false);

            if (curShip.LvlUpShips.Length != 0 && curShip.Level >= 3)
                upgradeBtn.gameObject.SetActive(true);
        }
        else addBtn.gameObject.SetActive(true);

        harborUI = GetComponent<HarborUI>();

        UpdateUI();
    }

    public void AddShip(ShipSO so)
    {
        harbor.AddShip(so);
        DataManager.instance.data.Money -= 200;
        Events.Balance.Invoke(DataManager.instance.data.Money);

        curShip = harbor.GetThis();
        Global.Name = curShip.Name;

        Events.ShipUpdate.Invoke(curShip);
        UpdateUI();
        addBtn.gameObject.SetActive(false);
    }

    public void UpgradeShip(ShipSO so)
    {
        harbor.UpgradeShip(so);
        DataManager.instance.data.Money -= 500;
        Events.Balance.Invoke(DataManager.instance.data.Money);

        curShip = harbor.GetThis();
        Global.Name = curShip.Name;

        Events.ShipUpdate.Invoke(curShip);
        UpdateUI();
        upgradeBtn.gameObject.SetActive(false);
    }

    private void Switch(bool isRight)
    {
        upgradeBtn.gameObject.SetActive(false);

        if (isRight) curShip = harbor.GetRight(curShip);
        else curShip = harbor.GetLeft(curShip);

        Events.ShipUpdate.Invoke(curShip);

        if (curShip != null)
        {
            Global.Name = curShip.Name;
            addBtn.gameObject.SetActive(false);

            if (curShip.LvlUpShips.Length != 0 && curShip.Level >= 3)
                upgradeBtn.gameObject.SetActive(true);
        }
        else
        {
            Global.Name = string.Empty;
            addBtn.gameObject.SetActive(true);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        harborUI.UpdateUI(curShip);
    }
}
