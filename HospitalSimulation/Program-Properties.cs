using HospitalSimulation.Models;
using System;
using System.Collections.Generic;

namespace HospitalSimulation
{
    public partial class Program
    {
        // Some values corresponding to duration's estimations
        // Delay in milliseconds that a Patient can spawn in
        private static int SPAWN_PATIENT_MIN = 1000;
        private static int SPAWN_PATIENT_MAX = 60000;

        // Delay in milliseconds before the hospital checks if it can give a resource
        private static int DELAY_CHECK_GIVE_ROOM = 30000;
        private static int DELAY_CHECK_GIVE_NURSE = 30000;
        private static int DELAY_CHECK_GIVE_PHYSICIAN = 30000;

        // Delay in milliseconds before the hospital checks if it can take a resource
        private static int DELAY_CHECK_TAKE_ROOM = 30000;
        private static int DELAY_CHECK_TAKE_NURSE = 30000;
        private static int DELAY_CHECK_TAKE_PHYSICIAN = 30000;

        // Threshold to which a hospital gives a resource
        private static Dictionary<ResourceType, int> THRESHOLDS_GIVE = new Dictionary<ResourceType, int>
        {
            [ResourceType.Room] = 1,
            [ResourceType.Nurse] = 4,
            [ResourceType.Physician] = 3
        };

        // Threshold to which a hospital takes a resource
        private static Dictionary<ResourceType, int> THRESHOLDS_TAKE = new Dictionary<ResourceType, int>
        {
            [ResourceType.Room] = 1,
            [ResourceType.Nurse] = 4,
            [ResourceType.Physician] = 3
        };


        // Some colors for the consoles DarkMagenta
        private static ConsoleColor COLOR_SIMULATION = ConsoleColor.Cyan;
        private static ConsoleColor COLOR_ERROR = ConsoleColor.Red;
        private static ConsoleColor COLOR_HOSPITAL = ConsoleColor.Blue;
        private static ConsoleColor COLOR_PATIENT = ConsoleColor.Yellow;
        private static ConsoleColor COLOR_RESOURCE_GIVE = ConsoleColor.Green;
        private static ConsoleColor COLOR_RESOURCE_TAKE = ConsoleColor.Red;
    }
}
