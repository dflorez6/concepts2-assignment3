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
        public long ID { get; set; }
        public Patient Patient { get; set; }

        //====================
        // Constructors
        //====================
        // Default
        public Record()
        {
            ID = 0;
            Patient = new Patient();
        }

        // Non-Default
        public Record(long noteId, Patient patient)
        {            
            ID = noteId;
            Patient = patient;
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
        public List<string> ReadRecords()
        {
            // Initial Declarations
            string record = "";            
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

            // Returns a list of records
            return records;
        }

        //--------------------
        // AddRecord
        //--------------------
        // Parameters: long (noteId) & Patient (object)
        // Returns: void
        // Description: Writes a new record in the patients.txt file. The method receives passed parameters, formats the data to match
        // the correct format of stored records & uses StreamWriter to perform the actual insertion of the data into the file.
        public void AddRecord(long noteId, Patient patient) // int id, Patient patient
        {
            // Building note string to be inserted into .txt file
            string dataForInsertion = $"{noteId}|";
            dataForInsertion += $"{patient.PatientName}|";
            dataForInsertion += $"{patient.PatientDoB.ToString("dd MMM yyyy")}|";

            // Iterate over problems List and sets the appropiate string format
            foreach (var problem in patient.Problems)
            {
                // Checks if it is the last item of the Problems List
                if (problem == patient.Problems.Last())
                {
                    dataForInsertion += $"{problem.PatientProblem}|";
                }
                else
                {
                    dataForInsertion += $"{problem.PatientProblem};";
                }
            }

            // Replace all new lines (\n) with separator (;) and setting the clinical note in the proper format to be stored
            dataForInsertion += patient.Note.Replace("\n", ";");

            // Adds User to the file patients.txt
            using (StreamWriter writer = new StreamWriter(_file, true))
            {
                // Used as examples of how I want the data to be stored
                // writer.WriteLine($"230410120218|Lisa Simpson|31 May 2004|Diabetes;Hypertension|BP: 120/80;;Diabetes under control|");
                // writer.WriteLine($"230410120436|Homer Simpson|31 May 2004|Overweight|BP: 134/89;BP: 130/85;HR: 44;HR: 101|");

                writer.WriteLine(dataForInsertion);
                
                // Last time I had to use .Close() bc the computer kept on freezing. Just leaving this here in case I need it again
                // writer.Close();
            }

            // I am leaving this for reference.
            /*
            Console.WriteLine("File Created");
            string directory = Directory.GetCurrentDirectory();
            Console.WriteLine(directory);
            string fullPath = directory + "\\" + "patients.txt";
            File.Create(fullPath);
            */
        }

        //--------------------
        // CreateId
        //--------------------
        // Parameters: none
        // Returns: void
        // Description: This method enables inputs & controls when "Start new note" button is clicked
        public long CreateId()
        {
            // Sets Patient Id as follows YearMonthDay_HourMinuteSecond
            long id = long.Parse(DateTime.Now.ToString("yyMMddHHmmss")); 
            return id;
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
        public List<Problem> Problems { get; set; }
        public string Note { get; set; }

        //====================
        // Constructors
        //====================
        // Default
        public Patient()
        {
            PatientName = null;
            PatientDoB = DateTime.Now;
            // Problems = new List<Problem>();
            Problems  = new List<Problem>();
            Note = null;
        }

        // Non-Default
        public Patient(string patientName, DateTime patientDoB, List<Problem> problems, string note)
        {
            PatientName = patientName;
            PatientDoB = patientDoB;
            Problems = problems;
            Note = note;
        }

        //====================
        // Methods
        //====================

    }

    // Class Problem
    public class Problem : Patient
    {
        //====================
        // Properties / Fields
        //====================
        public string PatientProblem { get; set; }

        //====================
        // Constructors
        //====================
        // Default
        public Problem()
        {
            PatientProblem = null;
        }

        // Non-Default
        public Problem(string patientProblem)
        {
            PatientProblem = patientProblem;
        }

        //====================
        // Methods
        //====================

    }


}