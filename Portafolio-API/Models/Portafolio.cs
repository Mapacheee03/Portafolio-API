using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Portafolio_API.Models
{
    public class Portafolio
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;
       
        [BsonElement("Team")]
        public string Team { get; set; } = string.Empty;
        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;
        [BsonElement("Correo")]
        public string Correo { get; set; } = string.Empty;
        [BsonElement("Telefono")]
        public string Telefono { get; set; } = string.Empty;
        [BsonElement("FecNac")]
        public string FecNac { get; set; } = string.Empty;
    }
}
