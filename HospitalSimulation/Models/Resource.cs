using Newtonsoft.Json;
using System;


namespace HospitalSimulation.Models
{
    public class Resource
    {
        public string id { get; set; }
        public string name { get; set; }
        public ResourceType type { get; set; }



        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Resource
        /// </summary>
        public Resource()
        {
            id = "res-0";
            name = String.Empty;
            type = ResourceType.Nurse;
        }


        /// <summary>
        /// Creates a new instance of a Resource with according parameters
        /// </summary>
        /// <param name="id">Id of the Resource</param>
        /// <param name="name">Name of the Resource</param>
        /// <param name="role">ResourceType of the Resource</param>
        public Resource(String id, string name, ResourceType role)
        {
            this.id = id;
            this.name = name;
            this.type = role;
        }



        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString() => $"{id} - {name} : {type}";


        // SERIALIZATION

        /// <summary>
        /// Return a JSON string representing the instance
        /// </summary>
        /// <returns>A JSON string representing the instance</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        /// <summary>
        /// Returns a Resource instance from a JSON string
        /// </summary>
        /// <param name="json">String containing the instance data (from a JSON syntax)</param>
        /// <returns>A Resource instance from a JSON string</returns>
        public static Resource Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Resource>(json);
        }
    }
}
