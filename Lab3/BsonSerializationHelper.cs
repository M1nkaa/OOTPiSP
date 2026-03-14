using Lab3;
using Lab3.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.IO;

namespace Lab3.Helpers
{
    public static class BsonSerializationHelper
    {
        // JSON settings for type handling
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Newtonsoft.Json.Formatting.None
        };

        // Save collection to BSON file
        public static void Serialize(string filePath, VehicleCollection collection)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            using (BsonDataWriter writer = new BsonDataWriter(fs))
            {
                Newtonsoft.Json.JsonSerializer serializer = Newtonsoft.Json.JsonSerializer.Create(_settings);
                serializer.Serialize(writer, collection);
            }
        }

        // Load collection from BSON file
        public static VehicleCollection Deserialize(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            using (BsonDataReader reader = new BsonDataReader(fs))
            {
                Newtonsoft.Json.JsonSerializer serializer = Newtonsoft.Json.JsonSerializer.Create(_settings);
                return serializer.Deserialize<VehicleCollection>(reader);
            }
        }
    }
}