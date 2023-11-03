using FluidChanges;
using System;
using System.IO;

namespace COIExtended
{
    public class ModCFG
    {
        
        public static bool allowStoreAllFluids { get; set; }   
        public static bool addT5StorageTier { get; set; }  
        public static void LoadSettings()
        {
            string modDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Captain of Industry", "Mods", "COIExtended");
            string filePath = Path.Combine(modDirectory, "modOptions.txt");
            if (File.Exists(filePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);

                    // Process each line in the text file
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('=');

                        if (parts.Length == 2)
                        {
                            string variableName = parts[0].Trim();
                            string variableValue = parts[1].Trim();

                            switch (variableName)
                            {
                                case "allowStoreAllFluids":
                                    allowStoreAllFluids = bool.Parse(variableValue);
                                    break;
                                case "addT5StorageTier":
                                    addT5StorageTier = bool.Parse(variableValue);
                                    break;

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reading the text file: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("The text file does not exist: " + filePath);
            }
        }
    }
}
