using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;


namespace HospitalSimulation.Models
{
    public class Hospital
    {
        // GETTER - SETTERs
        public string id { get; set; }
        public string name { get; set; }
        public List<Patient> patients { get; set; }
        public Dictionary<ResourceType, List<Resource>> resources { get; set; }
        [JsonIgnore]
        public Dictionary<ResourceType, SemaphoreSlim> resourceSemaphores { get; set; }




        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Node
        /// </summary>
        public Hospital()
        {
            id = null;
            name = null;
            patients = new List<Patient>();
            resources = new Dictionary<ResourceType, List<Resource>>();
            resourceSemaphores = new Dictionary<ResourceType, SemaphoreSlim>();

            // We iterate through the ResourceTypes
            ResourceTypeStuff.GetAllResourceTypes().ForEach(resourceType =>
            {
                // 1 - We create a row in resources (if it does not exist)
                if (!resources.ContainsKey(resourceType))
                {
                    // Since it does not exist, we create the missing row
                    resources.Add(resourceType, new List<Resource>());
                }

                // 2 - We fill the resourceSemaphores dictionary
                // We get the number of resources to add
                int cpt = resources[resourceType].Count;
                resourceSemaphores.Add(resourceType, new SemaphoreSlim(cpt));
            });
        }


        /// <summary>
        /// Creates a new instance of a Node with according parameters
        /// </summary>
        /// <param name="id">Id of the instance</param>
        /// <param name="name">Name of the instance</param>
        /// <param name="patients">List of patients of the instance</param>
        /// <param name="resourceSemaphores">Resources of the instance</param>
        /// <param name="resourceSemaphores">Semaphores of the Resources of the instance</param>
        public Hospital(string id, string name, List<Patient> patients, Dictionary<ResourceType, List<Resource>> resources, Dictionary<ResourceType, SemaphoreSlim> resourceSemaphores)
        {
            this.id = id;
            this.name = name;
            this.patients = patients;
            this.resources = resources;
            this.resourceSemaphores = resourceSemaphores;
        }


        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString() => $"{id} - {name}";


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
        /// Returns a Hospital instance from a JSON string
        /// </summary>
        /// <param name="json">String containing the instance data (from a JSON syntax)</param>
        /// <returns>A Node instance from a JSON string</returns>
        public static Hospital Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Hospital>(json);
        }



        [JsonIgnore]
        public bool isComplete { get => id != null && name != null; }
        [JsonIgnore]
        public List<Resource> rooms { get => resources[ResourceType.Room]; }
        [JsonIgnore]
        public List<Resource> nurses { get => resources[ResourceType.Nurse]; }
        [JsonIgnore]
        public List<Resource> physicians { get => resources[ResourceType.Physician]; }

        /// <summary>
        /// Adds a patient to the list
        /// </summary>
        /// <param name="patient">Patient to add</param>
        public void addPatient(Patient patient)
        {
            patients.Add(patient);
        }


        /// <summary>
        /// Removes a patient to the list
        /// </summary>
        /// <param name="patient">Patient to remove</param>
        public void removePatient(Patient patient)
        {
            patients.Remove(patient);
        }
    }
}
