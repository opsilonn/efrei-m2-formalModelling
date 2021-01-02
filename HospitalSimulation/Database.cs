﻿using HospitalSimulation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace HospitalSimulation
{
    public static class Database
    {
        // Some useful stuff
        private static string sourcePath = @"../../../database";
        private static string pathNodesFile { get { return sourcePath + "/nodes.json"; } }
        private static string pathHospitalFolder { get { return sourcePath + "/hospitals"; } }


        /// <summary>
        /// Reads the nodes file from the database and build the according Node List
        /// </summary>
        /// <returns>A list of Nodes set up</returns>
        public static List<Node> ReadNodes()
        {
            // We declare a list
            List<Node> nodes = new List<Node>();

            try
            {
                // We read the json
                string json = System.IO.File.ReadAllText(pathNodesFile);

                // We convert the json in a Node list
                nodes = new List<Node>(JsonConvert.DeserializeObject<Node[]>(json));
            }
            catch (Exception e)
            {
                // We display the error
                Console.WriteLine("Error when reading the nodes");
                Console.WriteLine(e.StackTrace);
            }

            // We return the list
            return nodes;
        }

        /// <summary>
        /// Reads the hospital's folder and files from the database and build the according Hospital List
        /// </summary>
        /// <returns>A list of Hospital set up</returns>
        public static List<Hospital> ReadHospitals()
        {
            // We declare a list
            List<Hospital> hospitals = new List<Hospital>();

            try
            {
                // test
                string[] filePaths = Directory.GetFiles(@"C:\Users\hugue\Desktop\m2-formal-modelling\HospitalSimulation\bin\Debug\netcoreapp3.1\database\hospitals");
                
                // We iterate through the hospital's files
                foreach (string path in filePaths)
                {
                    // We read the json
                    string json = System.IO.File.ReadAllText(path);

                    // We convert the json in a Hospital instance
                    Hospital newHospital = Hospital.Deserialize(json);

                    // We add it to the list
                    hospitals.Add(newHospital);
                }
            }
            catch (Exception e)
            {
                // We display the error
                Console.WriteLine("Error when reading the hospitals");
                Console.WriteLine(e.StackTrace);
            }

            // We return the list
            return hospitals;
        }
    }
}
