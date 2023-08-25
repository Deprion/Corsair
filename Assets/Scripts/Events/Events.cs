public static class Events
{
    public static SimpleEvent<ShipData> ShipUpdate = new SimpleEvent<ShipData>();
    public static SimpleEvent<ShipData> ShipUpgrade = new SimpleEvent<ShipData>();
    public static SimpleEvent<int> Balance = new SimpleEvent<int>();
    public static SimpleEvent AddCoin = new SimpleEvent();
    public static SimpleEvent Level = new SimpleEvent();
    public static SimpleEvent End = new SimpleEvent();
    public static SimpleEvent Click = new SimpleEvent();
    public static SimpleEvent GetHit = new SimpleEvent();
}
