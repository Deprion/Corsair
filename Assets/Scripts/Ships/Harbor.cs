using System;
using System.Linq;

public class Harbor
{
    public ShipData[] ships = new ShipData[10];

    private int index;

    public ShipData GetFirst()
    {
        var toReturn = ships.FirstOrDefault((n) => n != null);
        index = Array.IndexOf(ships, toReturn);
        return toReturn;
    }

    public ShipData GetRight(ShipData ship)
    {
        index = index + 1 >= ships.Length ? 0 : index + 1;

        return ships[index];
    }

    public ShipData GetLeft(ShipData ship)
    {
        index = index - 1 < 0 ? ships.Length - 1 : index - 1;

        return ships[index];
    }

    public ShipData GetThis()
    {
        return ships[index];
    }

    public void AddShip(ShipSO so)
    {
        ships[index] = new ShipData(so);
    }

    public Harbor(ShipSO ship) 
    {
        ships[0] = new ShipData(ship);
    }
}
