using Newtonsoft.Json;
using UnityEngine;

public class ShipData
{
    public string Name;
    [JsonIgnore] public Sprite Sprite { get { return DataHolder.inst.GetByName(Name).Sprite; } }
    public int Level;
    public int Exp, MaxExp;
    public float BaseSpeed;
    public int HP;

    public string LvlUpStr;

    public string[] LvlUpShips;

    public void AddExp(int exp)
    {
        if (Level >= 5) return;

        Exp += exp;
        if (Exp >= MaxExp)
        { 
            Exp -= MaxExp;
            Level++;
            MaxExp = 100 * Level;
            if (Level >= 5)
            {
                Exp = 777;
                MaxExp = 777;
            }
        }
    }

    public ShipData()
    {
        Level = 1;
        MaxExp = 100;
        Exp = 0;
    }

    public ShipData(ShipSO ship)
    { 
        Name = ship.Name;
        BaseSpeed = ship.BaseSpeed;
        HP = ship.HP;
        LvlUpShips = ship.LvlUpShips;
        Level = 1;
        MaxExp = 100;
        Exp = 0;
    }
}