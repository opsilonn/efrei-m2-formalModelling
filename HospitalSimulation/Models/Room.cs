using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace HospitalSimulation.Models
{
    public class Room : Resource
    {
        // CONSTRUCTORS

        /// <summary>
        /// Creates a default instance of a Room
        /// </summary>
        public Room() : base("Room-0", String.Empty, ResourceType.Room)
        { }


        /// <summary>
        /// Creates a new instance of a Room with according parameters
        /// </summary>
        /// <param name="id">Id of the Room</param>
        /// <param name="name">Name of the Room</param>
        public Room(String id, string name) : base(id, name, ResourceType.Room)
        { }
    }
}
