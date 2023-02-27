using Newtonsoft.Json;

namespace WebApi.Models; 


public class Deserialization
{
    [JsonProperty(PropertyName = "body")]
    public dynamic Body { get; set; }
}
