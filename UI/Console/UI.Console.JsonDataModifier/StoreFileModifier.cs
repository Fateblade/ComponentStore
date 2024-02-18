using Spectre.Console;
using System.Globalization;
using System.Text.Json;

namespace UI.Console.JsonDataModifier
{
    internal class StoreFileModifier
    {
        public static void ModifyBudgetValues(int newStoreBudget)
        {
             AnsiConsole.WriteLine("Loading File");

            using (var fileStream = File.OpenRead("./StoreData.json"))
            {
                var deserializedData = JsonSerializer.Deserialize<KeyValuePair<string, DataStore<StoreDataStoreEntry>>[]>(fileStream);
                
                AnsiConsole.WriteLine($"Modifying Store Budgets of {deserializedData[0].Value.entries.Length} stores");

                foreach (var storeEntry in deserializedData[0].Value.entries)
                {
                        AnsiConsole.Write($"Modifying store '{storeEntry.Key}': ");
                        if (storeEntry.Value?.data?.StoreBudget?.Value?.InitValue != null)
                        {
                            AnsiConsole.Write(
                                $"increasing budget from {storeEntry.Value.data.StoreBudget.Value.InitValue.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat)} ");
                            AnsiConsole.Write(
                                $"to {newStoreBudget.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat)}");
                            
                            storeEntry.Value.data.StoreBudget.Value.InitValue = newStoreBudget;
                            storeEntry.Value.data.StoreBudget.Value._int = newStoreBudget;

                            AnsiConsole.WriteLine(" - done");
                        }
                        else
                        {
                            AnsiConsole.WriteLine("no budget found to increase - skipped");
                        }
                }

                AnsiConsole.WriteLine("Modifications done");

                AnsiConsole.WriteLine("Writing to new file");

                using (var fileStreamWrite = File.Create("./StoreData.json.modified"))
                {
                    JsonSerializer.Serialize(fileStreamWrite, deserializedData);
                }

            }
        }
    }
}
