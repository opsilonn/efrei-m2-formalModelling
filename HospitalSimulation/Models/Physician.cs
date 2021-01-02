using System;


namespace HospitalSimulation.Models
{
    public class Physician : Resource
    {
        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Physician
        /// </summary>
        public Physician() : base("physician-0", String.Empty, ResourceType.Physician)
        { }


        /// <summary>
        /// Creates a new instance of a Physician with according parameters
        /// </summary>
        /// <param name="id">Id of the Physician</param>
        /// <param name="name">Name of the Physician</param>
        public Physician(String id, string name) : base(id, name, ResourceType.Physician)
        { }
    }
}
