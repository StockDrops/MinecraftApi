using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinecraftApi.Integrations.Models.Patreon
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Attributes
    {
        [JsonPropertyName("patron_status")]

        public string PatronStatus { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("attributes")]
        public Attributes Attributes { get; set; }

        [JsonPropertyName("relationships")]
        public Relationships Relationships { get; set; }
    }

    public class Memberships
    {
        [JsonPropertyName("data")]
        public List<Data> Data { get; set; }
    }

    public class Relationships
    {
        [JsonPropertyName("memberships")]
        public Memberships Memberships { get; set; }

        [JsonPropertyName("currently_entitled_tiers")]
        public CurrentlyEntitledTiers CurrentlyEntitledTiers { get; set; }
    }

    public class CurrentlyEntitledTiers
    {
        [JsonPropertyName("data")]
        public List<Data> Data { get; set; }
    }

    public class Included
    {
        [JsonPropertyName("attributes")]
        public Attributes Attributes { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("relationships")]
        public Relationships Relationships { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("self")]
        public string Self { get; set; }
    }

    public class IdentityResponse
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonPropertyName("included")]
        public List<Included> Included { get; set; }

        [JsonPropertyName("links")]
        public Links Links { get; set; }
    }


}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
