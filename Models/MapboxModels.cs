using System.Text.Json.Serialization;

namespace Roamy.Models.MapboxModels
{
    public class MapboxSuggestResponse
    {
        [JsonPropertyName("suggestions")]
        public List<MapboxSuggestion> Suggestions {get; set;} = new List<MapboxSuggestion>();
    }

    public class MapboxSuggestion
    {
        [JsonPropertyName("name")]
        public string Name {get; set;} = "";

        [JsonPropertyName("mapbox_id")]
        public string MapboxId {get; set;} = "";

        [JsonPropertyName("place_formatted")]
        public string PlaceFormatted {get; set;} = "";

        [JsonPropertyName("context")]
        public MapboxContext Context {get; set;} = new();
    }

    public class MapboxContext
    {
        [JsonPropertyName("country")]
        public MapboxCountry Country {get; set;} = new();

        [JsonPropertyName("place")]
        public MapboxCity Place {get; set;} = new(); 
    }

    public class MapboxCountry
    {
        [JsonPropertyName("name")]
        public string Name {get; set;} = "";
    }

    public class MapboxCity
    {
        [JsonPropertyName("name")]
        public string Name {get; set;} = "";
    }

    public class MapboxRetrieveResponse
    {
        [JsonPropertyName("features")]
        public List<MapboxFeatures> Features { get; set; } = new();
    }

    public class MapboxFeatures
    {
        [JsonPropertyName("properties")]
        public MapboxProperties Properties { get; set; } = new();
    }

    public class MapboxProperties
    {
        [JsonPropertyName("coordinates")]
        public MapboxCoordinates Coordinates { get; set; } = new();
    }

    public class MapboxCoordinates
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}