using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Button confirmBtn;

    private void Awake()
    {
        Events.ShipUpgrade.AddListener(UpgradeInfo, true);
    }

    private void UpgradeInfo(ShipData data)
    {
        //var first = DataHolder.inst.GetByName
    }

    private void OnDestroy()
    {
       Events.ShipUpgrade.AddListener(UpgradeInfo);
    }
}
