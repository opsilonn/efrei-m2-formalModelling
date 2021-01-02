using HospitalSimulation.Models;
using System;
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

        // List of resources semaphores
        SemaphoreSlim semSharedRooms = new SemaphoreSlim(0);
        SemaphoreSlim semSharedNurses = new SemaphoreSlim(0);
        SemaphoreSlim semSharedPhysicians = new SemaphoreSlim(0);





        static void Main(string[] args)
        {
            CONSOLE.WriteLine(ConsoleColor.Cyan, "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            CONSOLE.WriteLine(ConsoleColor.Cyan, "\t\t FORMAL MODELLING - Hospital Simulation");
            CONSOLE.WriteLine(ConsoleColor.Cyan, "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");


            // STEP 1 - initialize the variables
            Initialization();


            // Step 2 - verify if the simulation can be started
            Verification();


            // Step 3 - We start the simulation
            Start();
        }
    }
}
