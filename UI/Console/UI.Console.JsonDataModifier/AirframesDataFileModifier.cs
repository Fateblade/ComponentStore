using System.Globalization;
using System.Text.Json;
using Spectre.Console;

namespace UI.Console.JsonDataModifier;

class AirframesDataFileModifier
{
    public static void ModifyDriveValues(float cruisingDriveFactorMultiplier, float driveReadyMultiplier,
        float drivePrimingMultiplier, float fuelConsumptionMultiplier)
    {
        AnsiConsole.WriteLine("Loading File");

        using (var fileStream = File.OpenRead("./AirframesDatabase.json"))
        {
            var deserializedData = JsonSerializer.Deserialize<KeyValuePair<string, DataStore<AirframeDataStoreEntry>>[]>(fileStream);

            AnsiConsole.WriteLine($"Modifying {deserializedData[0].Value.entries.Length} air ship frames");

            foreach (var airshipEntry in deserializedData[0].Value.entries)
            {
                AnsiConsole.WriteLine($"Modifying airframe '{airshipEntry.Key}' - {airshipEntry.Value?.data?.FrameName?.Value?.InitValue}: ");

                if (airshipEntry.Value?.data?.MaxCruisingDriveFactor?.Value?.InitValue != null)
                {
                    var oldValue = airshipEntry.Value?.data?.MaxCruisingDriveFactor?.Value?.InitValue;
                    var modifiedValue = oldValue * cruisingDriveFactorMultiplier;

                    AnsiConsole.Write("modifying cruising drive factor from ");
                    AnsiConsole.Write(oldValue?.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat) ??
                                      "undefined");
                    AnsiConsole.Write("to ");
                    AnsiConsole.Write(modifiedValue?.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat) ??
                                      "unchanged");

                    if (oldValue.HasValue)
                    {
                        airshipEntry.Value.data.MaxCruisingDriveFactor.Value.InitValue = modifiedValue.Value;
                        airshipEntry.Value.data.MaxCruisingDriveFactor.Value._float = modifiedValue.Value;
                    }

                    AnsiConsole.WriteLine(" - done");
                }

                if (airshipEntry.Value?.data?.DriveReadyingTime?.Value?.InitValue != null)
                {
                    var oldValue = airshipEntry.Value?.data?.DriveReadyingTime?.Value?.InitValue;
                    var modifiedValue = oldValue * driveReadyMultiplier;

                    AnsiConsole.Write("modifying drive readying time from ");
                    AnsiConsole.Write(oldValue?.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat) ??
                                      "undefined");
                    AnsiConsole.Write("to ");
                    AnsiConsole.Write(modifiedValue?.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat) ??
                                      "unchanged");

                    if (oldValue.HasValue)
                    {
                        airshipEntry.Value.data.DriveReadyingTime.Value.InitValue = modifiedValue.Value;
                        airshipEntry.Value.data.DriveReadyingTime.Value._float = modifiedValue.Value;
                    }

                    AnsiConsole.WriteLine(" - done");
                }

                if (airshipEntry.Value?.data?.DrivePrimingTime?.Value?.InitValue != null)
                {
                    var oldValue = airshipEntry.Value?.data?.DrivePrimingTime?.Value?.InitValue;
                    var modifiedValue = oldValue * drivePrimingMultiplier;

                    AnsiConsole.Write("modifying drive priming time from ");
                    AnsiConsole.Write(oldValue?.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat) ??
                                      "undefined");
                    AnsiConsole.Write("to ");
                    AnsiConsole.Write(modifiedValue?.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat) ??
                                      "unchanged");

                    if (oldValue.HasValue)
                    {
                        airshipEntry.Value.data.DrivePrimingTime.Value.InitValue = modifiedValue.Value;
                        airshipEntry.Value.data.DrivePrimingTime.Value._float = modifiedValue.Value;
                    }

                    AnsiConsole.WriteLine(" - done");
                }

                if (airshipEntry.Value?.data?.FuelConsumption?.Value?.InitValue != null)
                {
                    var oldValue = airshipEntry.Value?.data?.FuelConsumption?.Value?.InitValue;
                    var modifiedValue = (int?)(oldValue * fuelConsumptionMultiplier);

                    AnsiConsole.Write("modifying drive priming time from ");
                    AnsiConsole.Write(oldValue?.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat) ??
                                      "undefined");
                    AnsiConsole.Write("to ");
                    AnsiConsole.Write(modifiedValue?.ToString("N", CultureInfo.GetCultureInfo("de-DE").NumberFormat) ??
                                      "unchanged");

                    if (oldValue.HasValue)
                    {
                        airshipEntry.Value.data.FuelConsumption.Value.InitValue = modifiedValue.Value;
                        airshipEntry.Value.data.FuelConsumption.Value._int = modifiedValue.Value;
                    }

                    AnsiConsole.WriteLine(" - done");
                }
            }

            AnsiConsole.WriteLine("Modifications done");

            AnsiConsole.WriteLine("Writing to new file");

            using (var fileStreamWrite = File.Create("./AirframesDatabase.json.modified"))
            {
                JsonSerializer.Serialize(fileStreamWrite, deserializedData);
            }

        }
    }
}