/* Notes.cs
*  Title: Notes
*  Name of Project: Assignment3 - Clinical notes
*  Purpose: Build a basic app to create, edit, read, and delete clinical encounter notes that will be stored in a simple text file
* 
*  Revision History
*   David Florez, 2023.04.09: Created
*/
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Notes_ClassLibrary
{
    // Class Record
    public class Record
    {
        //====================
        // Fields
        //====================
        private string _file = "patients.txt";

        //====================
        // Properties
        //====================
        public int ID { get; set; }
        public List<Patient> Patients { get; set; }

        //====================
        // Constructors
        //====================
        // Default
        public Record()
        {
            ID = 0;
            Patients = new List<Patient>();

        }

        // Non-Default
        public Record(Patient patient)
        {
            ID = int.Parse(DateTime.Now.ToString("yyMMddHHmmss")); // Sets Patient Id as follows YearMonthDay_HourMinuteSecond
            Patients.Add(patient);
        }

        //====================
        // Methods
        //====================
        //--------------------
        // ReadRecords
        //--------------------
        // Parameters: None
        // Returns: List of string arrays of already splitted records by '|'
        // Description: Reads records from file 'patients.txt'. Splits records by '|' and returns them in a list of string arrays
        // Will be used to load records from file when the application loads
        // public List<string[]> ReadRecords() // Returns string arrays
        public List<string> ReadRecords()
        {
            // Initial Declarations
            string record = "";
            // List<string[]> records = new List<string[]>(); // Stores string arrays
            List<string> records = new List<string>(); // Store strings

            // Reads stored records
            using (StreamReader reader = new StreamReader(_file))
            {                
                while (!reader.EndOfStream)
                {
                    record = reader.ReadLine();
                    // records.Add(record.Split("|")); // Splits records by '||'
                    records.Add(record);
                }

            }

            // Returns a list of string arrays
            return records;
        }

        //--------------------
        // AddRecord
        //--------------------
        // Parameters: None
        // Returns: 
        // Description: Adds a new record
        public void AddRecord() // int id, Patient patient
        {

            /*
            Console.WriteLine("File Created");
            string directory = Directory.GetCurrentDirectory();
            Console.WriteLine(directory);
            string fullPath = directory + "\\" + "patients.txt";
            File.Create(fullPath);
            */

            // Adds User to the file patients.txt
            using (StreamWriter writer = new StreamWriter(_file, true))
            {
                writer.WriteLine($"230410120218|Lisa Simpson|31 May 2004|Diabetes;Hypertension|BP: 120/80;;Diabetes under control|");
                writer.WriteLine($"230410120436|Homer Simpson|31 May 2004|Overweight|BP: 134/89;BP: 130/85;HR: 44;HR: 101|");
                writer.Close();
            }
        }

    }

    // Class Patient
    public class Patient
    {
        //====================
        // Properties / Fields
        //====================
        public string PatientName { get; set; }
        public DateTime PatientDoB { get; set; }
        // public List<Problem> Problems { get; set; }
        public Dictionary<int, List<Problem>> Problems { get; set; } // TODO: USE DICTIONARY FOR PROBLEMS key: "patient", value: "list of problems"

        //====================
        // Constructors
        //====================
        // Default
        public Patient()
        {
            PatientName = null;
            PatientDoB = DateTime.Now;
            // Problems = new List<Problem>();
            Problems  = new Dictionary<int, List<Problem>>();
        }

        // Non-Default
        public Patient(string patientName, DateTime patientDoB, Problem problem)
        {
            PatientName = patientName;
            PatientDoB = patientDoB;
            // Problems.Add(problem);
            Problems = new Dictionary<int, List<Problem>>();
        }

        //====================
        // Methods
        //====================

    }

    // Class Problem
    public class Problem
    {
        //====================
        // Properties / Fields
        //====================        
        public string PatientName { get; set; }
        public DateTime PatientDoB { get; set; }

        //====================
        // Constructors
        //====================
        // Default
        public Problem()
        {

        }

        // Non-Default
        public Problem(string patientName, DateTime patientDoB)
        {
            PatientName = patientName;
            PatientDoB = patientDoB;
        }

        //====================
        // Methods
        //====================

    }


}