using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HospitalSimulation.Models
{
    public class Patient
    {
        private static int _idCpt = 1;
        public String id { get; set; }
        public string name { get; set; }


        private List<string> nameFirstList = new List<string>()
        {
            "Anne",
            "Baptiste",
            "Céline",
            "Dorian",
            "Etienne",
            "François",
            "Guillaume",
            "Hugues",
            "Isildur",
            "Jean",
            "Karl"
        };

        private List<string> nameLastList = new List<string>()
        {
            "Dorian",
            "Franz",
            "Hammett",
            "Hetfield",
            "Lixy",
            "Lombardo",
            "Piplup"
        };




        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Patient
        /// </summary>
        public Patient()
        {
            id = "pat-" + (_idCpt++).ToString();
            name = nameFirstList[(int)(new Random().NextDouble() * (nameFirstList.Count))] + " " + nameLastList[(int)(new Random().NextDouble() * (nameLastList.Count))];
        }


        /// <summary>
        /// Creates a new instance of a Patient with according parameters
        /// </summary>
        /// <param name="id">Id of the Patient</param>
        /// <param name="name">Name of the Patient</param>
        public Patient(string id, string name)
        {
            this.id = id;
            this.name = name;
        }



        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString() => $"patient : {id} - {name}";


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
        /// Returns a Patient instance from a JSON string
        /// </summary>
        /// <param name="json">String containing the instance data (from a JSON syntax)</param>
        /// <returns>A Patient instance from a JSON string</returns>
        public static Patient Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Patient>(json);
        }
    }
}
