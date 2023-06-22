using UnityEngine;

[CreateAssetMenu()]
public class ShipSO : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    [TextArea] public string LvlUpStr;
    public float BaseSpeed;
    public int HP;

    public string[] LvlUpShips;
}
