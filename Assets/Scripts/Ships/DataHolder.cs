using System.Linq;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static DataHolder inst;

    public ShipSO[] Ships;

    public void Awake()
    {
        inst = this;
        DontDestroyOnLoad(this);
    }

    public ShipSO GetByName(string name)
    {
        return Ships.FirstOrDefault(n => n.Name == name);
    }
}
