using HospitalSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace HospitalSimulation
{
    public partial class Program
    {
        /// <summary>
        /// Initializes all variables of the simulation
        /// </summary>

        private static void Initialization()
        {
            // STEP 1 - initialize the variables
            CONSOLE.Write(COLOR_SIMULATION, "\n\nPhase 1 : reading the database... ");

            // We fill the nodes list
            nodes = Database.ReadNodes();

            // We fill the nodes list
            hospitals = Database.ReadHospitals();

            // We fill the shared resources pool
            ResourceTypeStuff.GetAllResourceTypes().ForEach(resourceType => sharedSemaphores.Add(resourceType, new SemaphoreSlim(0)));

            // If we reach this statement, we consider that the initialization is successful !
            CONSOLE.WriteLine(COLOR_SIMULATION, "reading complete !\n\n");
        }




        /// <summary>
        /// Proceeds to verify if the simulation's variables are valid, and stops the application if not
        /// </summary>
        private static void Verification()
        {
            // STEP 2 - verify the variables
            CONSOLE.Write(COLOR_SIMULATION, "\n\nPhase 2 : verifying the environment... ");

            // We check that the petri net is not empty
            if (nodes.Count == 0)
            {
                CONSOLE.WriteLine(COLOR_ERROR, "\n\nERROR - The Petri net is empty !!");
                System.Environment.Exit(0);
            }

            // We check that there is at least 1 hospital
            if (hospitals.Count == 0)
            {
                CONSOLE.WriteLine(COLOR_ERROR, "\n\nERROR - There is no hospital !!");
                System.Environment.Exit(0);
            }

            // We check that the petri net has at least one entrance state
            if (!nodes.Exists(_ => _.isStartingNode))
            {
                CONSOLE.WriteLine(COLOR_ERROR, "\n\nERROR - The Petri net has no starting node !!");
                System.Environment.Exit(0);
            }

            // We check that the petri net has at least one exit state
            if (!nodes.Exists(_ => _.isEndingNode))
            {
                CONSOLE.WriteLine(COLOR_ERROR, "\n\nERROR - The Petri net has no ending node !!");
                System.Environment.Exit(0);
            }

            // IMPORTANT - ONLY WORKS FOR A LINEAR PETRI NET
            // We check that, each Node either is an exit node or leads to another Node
            nodes.ForEach(node =>
            {
                if (!node.isEndingNode && node.idNodeTo == null)
                {
                    CONSOLE.WriteLine(COLOR_ERROR, "\n\nERROR - The Petri net has a Node that neither is an exit Node, nor leads to another Node !!");
                    System.Environment.Exit(0);
                }
            });

            // IMPORTANT - ONLY WORKS FOR A LINEAR PETRI NET
            // We check that, each Node that leads to a Node, leads to an existing Node
            nodes.ForEach(node =>
            {
                if (node.idNodeTo != null && !nodes.Any(n => n.id == node.idNodeTo))
                {
                    CONSOLE.WriteLine(COLOR_ERROR, "\n\nERROR - The Petri net has a Node that leads to a non-existing Node !!");
                    System.Environment.Exit(0);
                }
            });

            // IMPORTANT - ONLY WORKS FOR A LINEAR PETRI NET
            // We check that, from each entry states, a least one exit state can be reached
            nodes.ForEach(node =>
            {
                if (node.isStartingNode && !node.isEndingNode)
                {
                    Node n = node;
                    List<string> idNodesVisited = new List<string>() { node.id };
                    bool endLoop = true;
                    bool canReachEndNode = false;

                    do
                    {
                        // We get the current Node (we don't need to verify if it is null, thanks to previous tests)
                        n = nodes.Find(_ => _.id == n.idNodeTo);

                        // We check if an exit Node can be reached
                        canReachEndNode = n.isEndingNode;

                        // We check if we have to continue
                        endLoop = canReachEndNode || idNodesVisited.Contains(n.id);

                        // If we don't end the loop, we add the current Node's id to the list
                        if (!endLoop)
                        {
                            idNodesVisited.Add(n.id);
                        }
                    } while (!endLoop);


                    // If this node cannot reach an exit : ERROR
                    if (!canReachEndNode)
                    {
                        CONSOLE.WriteLine(COLOR_ERROR, "\n\nERROR - The Petri net has a Starting node that cannot reach any Exit Node !!");
                        System.Environment.Exit(0);
                    }
                }
            });

            // If we reach this statement, we consider that the verification is successful !
            CONSOLE.WriteLine(COLOR_SIMULATION, "verification complete !\n\n");
        }




        /// <summary>
        /// Starts the simulations
        /// </summary>
        private static void Start()
        {
            // STEP 3 - launch the simulation
            CONSOLE.WriteLine(COLOR_SIMULATION, "\n\nPhase 3 : Starting the simulation");

            // We iterate through the hospitals
            hospitals.ForEach(hospital => {
                // We create a thread
                var thread = new Thread(() => StartHospital(hospital));

                // We start the thread
                thread.Start();
            });
        }




        /// <summary>
        /// Starts the simulation of an Hospital
        /// </summary>
        /// <param name="hospital">Hospital to simulate</param>
        public static void StartHospital(Hospital hospital)
        {
            // A small message
            CONSOLE.WriteLine(ConsoleColor.Blue, $"hospital {hospital.id} - {hospital.name} has opened !");

            // Thread : patients arriving at the hospital
            // We create a thread
            Thread threadPatients = new Thread(() => {
                while (true)
                {
                    // We create a patient
                    Patient patient = new Patient();

                    // We add him to the hospital
                    hospital.addPatient(patient);

                    // We launch the client into the petriNet
                    Thread threadPatient = new Thread(() => StartPatient(hospital, patient));
                    threadPatient.Start();

                    // We randomly set a waiting time
                    int waitingTime = (int)(new Random().NextDouble() * (SPAWN_PATIENT_MAX - SPAWN_PATIENT_MIN) + SPAWN_PATIENT_MIN);

                    // We wait
                    Thread.Sleep(waitingTime);
                }
            });

            // We start the thread
            threadPatients.Start();
        }




        /// <summary>
        /// Starts the simulation of a Patient
        /// </summary>
        /// <param name="hospital">Hospital the patient is in</param>
        /// <param name="patient">Patient to simulate</param>
        private static async Task StartPatient(Hospital hospital, Patient patient)
        {
            // A small message
            CONSOLE.WriteLine(ConsoleColor.Yellow, $"patient {patient.id}  - {patient.name} has entered the hospital {hospital.id} !");

            // We declare the initial node
            Node node = initialNode;
            bool isFirstNode = true;

            // We iterate through the nodes of the petrinet while we do not reach an exit state
            do
            {
                // 1 - we get the next node (except is it is the first)
                if (!isFirstNode)
                {
                    node = nodes.Find(_ => _.id == node.idNodeTo);
                }
                else
                {
                    isFirstNode = false;
                }

                // 2 - node's action
                // We print what the node does
                Console.WriteLine($"{hospital.id} - {patient.id} : {node.message}");
                // The node does its stuff (aka : waiting)
                node.action();

                // 3 - node's resources needed
                // We check if a resource is needed
                foreach(ResourceType resourceType in node.resourceTypesNeeded)
                {
                    // We wait for the semaphore to be activated
                    await hospital.resourceSemaphores[resourceType].WaitAsync();

                    // If a resource should be taken
                    if (THRESHOLDS_TAKE[resourceType] >= hospital.resourceSemaphores[resourceType].CurrentCount)
                    {
                        // We create a thread
                        new Thread(async () =>
                        {
                            // We take a resource from the spare pool
                            await hospital.resourceSemaphores[resourceType].WaitAsync();

                            // We add the resource to the hospital's pool
                            hospital.resourceSemaphores[resourceType].Release();
                        }).Start();
                    }
                }


                // 4 - node's resources freed
                // We check if a resource is freed
                foreach (ResourceType resourceType in node.resourceTypesFreed)
                {
                    // We wait for the semaphore to be activated
                    hospital.resourceSemaphores[resourceType].Release();

                    // If a resource can be spared
                    if (THRESHOLDS_GIVE[resourceType] <= hospital.resourceSemaphores[resourceType].CurrentCount)
                    {
                        // We wait for the semaphore to be activated
                        new Thread(async () => await hospital.resourceSemaphores[resourceType].WaitAsync() ).Start();
                    }
                }
            } while (!node.isEndingNode);


            // A small message
            CONSOLE.WriteLine(ConsoleColor.Green, $"patient {patient.id}  - {patient.name} has left the hospital {hospital.id} !");

            // We remove the Patient of the hospital
            hospital.removePatient(patient);
        }
    }
}
