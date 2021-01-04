using HospitalSimulation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace HospitalSimulation
{
    public partial class Program
    {
        // Nodes composing the petri-net
        public static List<Node> nodes = new List<Node>();
        public static Node initialNode { get => nodes.FirstOrDefault(_ => _.isStartingNode); }

        // List of hospitals
        public static List<Hospital> hospitals = new List<Hospital>();

        // Threshold to which a hospital gives a resource
        private static Dictionary<ResourceType, SemaphoreSlim> sharedSemaphores = new Dictionary<ResourceType, SemaphoreSlim>();



        /// <summary>
        /// Method to launch the application
        /// </summary>
        /// <param name="args">List of arguments given as parameters when launching the application</param>
        static void Main(string[] args)
        {
            // A small message
            CONSOLE.WriteLine(COLOR_SIMULATION, "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            CONSOLE.WriteLine(COLOR_SIMULATION, "\t\t FORMAL MODELLING - Hospital Simulation");
            CONSOLE.WriteLine(COLOR_SIMULATION, "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");


            // STEP 1 - initialize the variables
            Initialization();


            // Step 2 - verify if the simulation can be started
            Verification();


            // Step 3 - We start the simulation
            Start();
        }
    }
}
