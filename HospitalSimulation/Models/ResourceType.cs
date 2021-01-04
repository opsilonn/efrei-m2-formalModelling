using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSimulation.Models
{
    public enum ResourceType
    {
        Room,
        Nurse,
        Physician
    }


    public static class ResourceTypeStuff
    {
        public class ResourceTypeData
        {
            public int id { get; }
            public string name { get; }

            public ResourceTypeData(int id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }
        /// <summary>
        /// Data of the instance
        /// </summary>
        /// <param name="resourceType">ResourceType we are analysing</param>
        /// <returns>Data of the instance</returns>
        private static ResourceTypeData data(this ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Room:
                    return new ResourceTypeData(0, "Room");
                case ResourceType.Nurse:
                    return new ResourceTypeData(1, "Nurse");
                case ResourceType.Physician:
                    return new ResourceTypeData(2, "Physician");

                default:
                    return new ResourceTypeData(0, "Room");
            }
        }


        /// <summary>
        /// Id of the instance
        /// </summary>
        /// <param name="resourceType">ResourceType we are analysing</param>
        /// <returns>Id of the instance</returns>
        public static int id(this ResourceType resourceType)
        {
            return resourceType.data().id;
        }


        /// <summary>
        /// Name of the instance
        /// </summary>
        /// <param name="resourceType">ResourceType we are analysing</param>
        /// <returns>Name of the instance</returns>
        public static string name(this ResourceType resourceType)
        {
            return resourceType.data().name;
        }


        /// <summary>
        /// Returns an array of all the ResourceType
        /// </summary>
        /// <returns>An array of all the ResourceType</returns>
        public static List<ResourceType> GetAllResourceTypes()
        {
            return Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>().ToList();
        }
    }
}
