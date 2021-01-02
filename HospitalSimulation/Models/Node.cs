using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;


namespace HospitalSimulation.Models
{
    public class Node
    {
        // GETTER - SETTER
        public string id { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public bool isStartingNode { get; set; }
        public bool isEndingNode { get; set; }
        public string idNodeTo { get; set; }
        public List<ResourceType> resourceTypesNeeded { get; set; }
        public List<ResourceType> resourceTypesFreed { get; set; }
        public int waitMin { get; set; }
        public int waitMax { get; set; }


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Node
        /// </summary>
        public Node()
        {
            id = "-1";
            name = String.Empty;
            message = String.Empty;
            isStartingNode = false;
            isEndingNode = false;
            idNodeTo = null;
            resourceTypesNeeded = new List<ResourceType>();
            resourceTypesFreed = new List<ResourceType>();
            waitMin = 0;
            waitMax = 0;
        }


        /// <summary>
        /// Creates a new instance of a Node with according parameters
        /// </summary>
        /// <param name="id">Id of the Node</param>
        /// <param name="name">Name of the Node</param>
        /// <param name="message">Name of the Node</param>
        /// <param name="isStartingNode">Whether this Node is the initial of its petri net</param>
        /// <param name="isEndingNode">Whether this Node is a final of its petri net</param>
        /// <param name="idNodeTo">Id of the destination node</param>
        /// <param name="resourceTypesNeeded">Resources needed to go to the next node</param>
        /// <param name="resourceTypesFreed">Resources freed when going to the next node</param>
        /// <param name="waitMin">Minimum waiting time of the Node</param>
        /// <param name="waitMax">Maximum waiting time of the Node</param>
        public Node(String id, string name, string message, bool isStartingNode, bool isEndingNode, string idNodeTo, List<ResourceType> resourceTypesNeeded, List<ResourceType> resourceTypesFreed, int waitMin, int waitMax)
        {
            this.id = id;
            this.name = name;
            this.message = message;
            this.isStartingNode = isStartingNode;
            this.isEndingNode = isEndingNode;
            this.idNodeTo = idNodeTo;
            this.resourceTypesNeeded = resourceTypesNeeded;
            this.resourceTypesFreed = resourceTypesFreed;
            this.waitMin = waitMin;
            this.waitMax = waitMax;
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
        /// Returns a Node instance from a JSON string
        /// </summary>
        /// <param name="json">String containing the instance data (from a JSON syntax)</param>
        /// <returns>A Node instance from a JSON string</returns>
        public static Node Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Node>(json);
        }


        // PARAMETERS
        [JsonIgnore]
        public bool hasWaitingTime { get => waitMin != 0 && waitMax != 0; }


        /// <summary>
        /// This is the method that emulates the behaviour of the node. To keep things simple, it just waits (if it can)
        /// </summary>
        public void action() {
            // If this node has to wait
            if (hasWaitingTime) {
                // We randomly set a waiting time
                int waitingTime = (int)(new Random().NextDouble() * (waitMax - waitMin) + waitMin);

                // We wait
                Thread.Sleep(waitingTime);
            }
        }
    }
}
