using System.Text.Json.Serialization;

namespace MapboxModels
{
    public class MapboxSuggestResponse
    {
        public List<MapboxSuggestion> Suggestions {get; set;} = new List<MapboxSuggestion>();
    }

    public class MapboxSuggestion
    {
        public string Name {get; set;} = "";
        [JsonPropertyName("mapbox_id")]
        public string MapboxId {get; set;} = "";
        [JsonPropertyName("place_formatted")]
        public string PlaceFormatted {get; set;} = "";
        public MapboxContext Context {get; set;} = new();
    }

    public class MapboxContext
    {
        public MapboxCountry Country {get; set;} = new();
        public MapboxCity Place {get; set;} = new(); 
    }

    public class MapboxCountry
    {
        public string Name {get; set;} = "";
    }

    public class MapboxCity
    {
        public string Name {get; set;} = "";
    }
}