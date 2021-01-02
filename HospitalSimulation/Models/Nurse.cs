using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace HospitalSimulation.Models
{
    public class Nurse : Resource
    {
        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Nurse
        /// </summary>
        public Nurse() : base("nurse-0", String.Empty, ResourceType.Nurse)
        { }


        /// <summary>
        /// Creates a new instance of a Nurse with according parameters
        /// </summary>
        /// <param name="id">Id of the Nurse</param>
        /// <param name="name">Name of the Nurse</param>
        public Nurse(String id, string name) : base(id, name, ResourceType.Nurse)
        { }
    }
}
