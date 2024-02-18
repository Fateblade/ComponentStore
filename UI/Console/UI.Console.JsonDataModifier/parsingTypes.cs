using System.Text.Json.Serialization;

namespace UI.Console.JsonDataModifier
{
    class DataStore<T>
    {
        public KeyValuePair<string, DataStoreEntryContainer<T>>[] entries { get; set; }
    }

    class DataStoreEntryContainer<T>
    {
        public bool selected { get; set; }
        public T data { get; set; }
    }

    class StoreDataStoreEntry
    {
        public object StoreID { get; set; }
        public object StoreName { get; set; }
        public object StoreDescription { get; set; }
        public object FocusTab { get; set; }
        public object OfferTable { get; set; }
        public object AmountOffer { get; set; }
        public object OfferPriceOffset { get; set; }
        public object OfferRestock { get; set; }
        public object OfferCondition { get; set; }
        public object OfferQuality { get; set; }
        public object PurchaseIDTable { get; set; }
        public object PurchaseIDOffset { get; set; }
        public object PurchaseTypeAndSubTypeTable { get; set; }
        public object PurchaseTypeOffset { get; set; }
        public object Currency { get; set; }
        public object InventoryRestockInterval { get; set; }
        public object BudgetRestockInterval { get; set; }
        public IntTypeExternal StoreBudget { get; set; }
    }

    class AirframeDataStoreEntry
    {
        public object AirframeID { get; set; }
        public object PrefabsID { get; set; }
        public StringTypeExternal FrameName { get; set; }
        public object MaterialID { get; set; }
        public object Drag { get; set; }
        public object Fuel { get; set; }
        public IntTypeExternal FuelConsumption { get; set; }
        public object Cargo { get; set; }
        public object Crew { get; set; }
        public object Mass { get; set; }
        public AirframeClassTypeExternal AirframeClass { get; set; }
        public object AirframeSize { get; set; }
        public object BoardingPower { get; set; }
        public object BoardingResist { get; set; }
        public object PropulsionType { get; set; }
        public object ThrustEfficiency { get; set; }
        public object ManeuveringEfficiency { get; set; }
        public object TurnEfficiency { get; set; }
        public object PowerGenerationEfficiency { get; set; }
        public object ShieldGenerationEfficiency { get; set; }
        public object Description { get; set; }
        public FloatTypeExternal DriveReadyingTime { get; set; }
        public FloatTypeExternal DrivePrimingTime { get; set; }
        public FloatTypeExternal AccelerationFactor { get; set; }
        public FloatTypeExternal MaxCruisingDriveFactor { get; set; }
        public object PVPWhitelist { get; set; }
        public object ProtectedCrew { get; set; }
        public object MaterialStorage { get; set; }
        public object MaterialConsumption { get; set; }
        public object PowerStorage { get; set; }
        public object AmmoStorage { get; set; }
        public object RepairTeam { get; set; }
        public object MedianPrice { get; set; }
        [JsonPropertyName("Fleet Power")] public object FleetPower { get; set; }
    }

    class IntTypeExternal
    {
        [JsonPropertyName("IntType")] public IntTypeInternal Value { get; set; }
    }

    class IntTypeInternal
    {
        public int _int { get; set; }
        public int InitValue { get; set; }
        [JsonPropertyName("$type")] 
        public string type { get; set; }
    }

    class FloatTypeExternal
    {
        [JsonPropertyName("FloatType")] 
        public FloatTypeInternal Value { get; set; }
    }

    class FloatTypeInternal
    {
        public float _float { get; set; }
        public float InitValue { get; set; }
        [JsonPropertyName("$type")] 
        public string type { get; set; }
    }

    class StringTypeExternal
    {
        [JsonPropertyName("StringType")]
        public StringTypeInternal Value { get; set; }
    }

    class StringTypeInternal
    {
        public string _string { get; set; }
        public string InitValue { get; set; }
        [JsonPropertyName("$type")]
        public string type { get; set; }
    }

    class AirframeClassTypeExternal
    {
        [JsonPropertyName("AirframeClassType")]
        public AirframeClassTypeInternal Value { get; set; }
    }

    class AirframeClassTypeInternal
    {
        public string airframeClass { get; set; }
        [JsonPropertyName("$type")] 
        public string type { get; set; }
    }

    static class KnownAirframeTypes
    {
        public static string Gunboat => "Gunboat";
        public static string TorpedoBoat => "Torpedo_Boat";
        public static string LightFreighter => "Light_Freighter";
        public static string MediumFreighter => "Medium_Freighter";
        public static string HeavyFreighter => "Heavy_Freighter";
        public static string Corvette => "Corvette";
        public static string BoardingCorvette => "Boarding_Corvette";
        public static string LightFrigate => "Light_Frigate";
        public static string HeavyFrigate => "Heavy_Frigate";
        public static string DefenseFrigate => "Defense_Frigate";
        public static string TroopFrigate => "Troop_Frigate";
        public static string Cruiser => "Cruiser";
        public static string Battlecruiser => "Battlecruiser";
    }
}
