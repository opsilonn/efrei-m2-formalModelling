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
        public List<Room> rooms { get; set; }
        public List<Nurse> nurses { get; set; }
        public List<Physician> physicians { get; set; }

        public SemaphoreSlim semRooms { get; set; }
        public SemaphoreSlim semNurses { get; set; }
        public SemaphoreSlim semPhysicians { get; set; }


        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Node
        /// </summary>
        public Hospital()
        {
            id = "hos-0";
            name = "default hospital";
            patients = new List<Patient>();
            rooms = new List<Room>();
            nurses = new List<Nurse>();
            physicians = new List<Physician>();
            semRooms = new SemaphoreSlim(rooms.Count);
            semNurses = new SemaphoreSlim(nurses.Count);
            semPhysicians = new SemaphoreSlim(physicians.Count);
        }


        /// <summary>
        /// Creates a new instance of a Node with according parameters
        /// </summary>
        /// <param name="id">Id of the instance</param>
        /// <param name="name">Name of the instance</param>
        /// <param name="patients">List of patients of the instance</param>
        /// <param name="rooms">List of rooms of the instance</param>
        /// <param name="nurses">List of nurses of the instance</param>
        /// <param name="physicians">List of physicians of the instance</param>
        public Hospital(string id, string name, List<Patient> patients, List<Room> rooms, List<Nurse> nurses, List<Physician> physicians)
        {
            this.id = id;
            this.name = name;
            this.patients = patients;
            this.rooms = rooms;
            this.nurses = nurses;
            this.physicians = physicians;
            semRooms = new SemaphoreSlim(rooms.Count);
            semNurses = new SemaphoreSlim(nurses.Count);
            semPhysicians = new SemaphoreSlim(physicians.Count);
        }


        /// <summary>
        /// Textual representation of the instance
        /// </summary>
        /// <returns> A textual representation of the instance</returns>
        public override string ToString() => $"{id} - {name} with {rooms.Count} rooms, {nurses.Count} nurses and {physicians.Count} physicians";


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
