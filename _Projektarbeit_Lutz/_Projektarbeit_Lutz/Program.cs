// Namespace für das Projekt
namespace _Projektarbeit_Lutz
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;

    /* (Zwischen) Projekt

    Inhalte:

    Klassen
    Methoden
    Objekte
    Logische Operatoren (if-else)
    Interface 
    Vererbungen    2


    Deadline: Abgabe am 23.10. */

    // Interface für Tiere
    public interface ILautVomTier
    {
        void LautGeben(); // Methode, die das Tier einen Laut geben lässt
    }

    // Basis-Klasse für Tiere
    public class Tier : ILautVomTier
    {
        public string Name { get; set; } // Name des Tieres
        public int Alter { get; set; } // Alter des Tieres

        // Konstruktor für die Klasse Tier
        public Tier(string name, int alter)
        {
            Name = name; // Setze den Namen
            Alter = alter; // Setze das Alter
        }

        // Methode, die einen allgemeinen Laut gibt
        public virtual void LautGeben()
        {
            Console.WriteLine($"{Name} macht einen Laut."); // Ausgabe des Lautes
        }
    }

    // Abgeleitete Klasse für Hunde
    public class Hund : Tier
    {
        public string Rasse { get; set; } // Rasse des Hundes

        // Konstruktor für die Klasse Hund
        public Hund(string name, int alter, string rasse) : base(name, alter)
        {
            Rasse = rasse; // Setze die Rasse
        }

        // Überschreibe die LautGeben-Methode für Hunde
        public override void LautGeben()
        {
            Console.WriteLine($"{Name}, der {Rasse}, bellt!"); // Spezifischer Laut für Hunde
        }
    }

    // Hauptprogramm
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Liste für die Tiere im Heim 
             * List<Tier> tiere = new List<Tier>();*/ //Vorher so definiert

            List<Tier> tiere = LoadTiereFromJson(); // Lade Tiere beim Start
            
            bool ersteZeile = true;


            // Endlosschleife für Benutzereingaben
            while (true)
            {
                if (!ersteZeile)
                {
                    Console.WriteLine(); // newline für allererste Zeile
                }
                ersteZeile = false;
                // Benutzeranweisung
                Console.WriteLine("Willkommen im Tierheim");
                Console.WriteLine("Wähle eine Option:");
                Console.WriteLine("1. Tier hinzufügen");
                Console.WriteLine("2. Alle Tiere anzeigen");
                Console.WriteLine("3. Programm beenden");
                Console.WriteLine("4. Tiere nochmal speichern"); // Neue Option zum Speichern
                string wahl = Console.ReadLine(); // Benutzereingabe

                // Überprüfen der Benutzerauswahl
                if (wahl == "1") // Option 1: Tier hinzufügen
                {
                    // Eingabe für den Namen des Tieres
                    Console.Write("Gib den Namen des Tieres ein: ");
                    string name = Console.ReadLine();

                    // Eingabe für das Alter des Tieres
                    Console.Write("Gib das Alter des Tieres ein: ");
                    int alter = int.Parse(Console.ReadLine()); // Konvertiere die Eingabe in eine Ganzzahl

                    // Eingabe für die Rasse, falls das Tier ein Hund ist
                    Console.Write("Ist das Tier ein Hund? (ja/nein): ");
                    string istHund = Console.ReadLine();

                    // Überprüfen, ob das Tier ein Hund ist
                    if (istHund.ToLower() == "ja")
                    {
                        // Eingabe für die Rasse des Hundes
                        Console.Write("Gib die Rasse des Hundes ein: ");
                        string rasse = Console.ReadLine();

                        // Erstelle ein neues Hund-Objekt
                        Hund hund = new Hund(name, alter, rasse);
                        tiere.Add(hund); // Füge den Hund zur Liste der Tiere hinzu
                        Console.WriteLine("Hund wurde hinzugefügt!");
                    }
                    else
                    {
                        // Erstelle ein neues Tier-Objekt
                        Tier tier = new Tier(name, alter);
                        tiere.Add(tier); // Füge das Tier zur Liste hinzu
                        Console.WriteLine("Tier wurde hinzugefügt!");
                    }
                    SaveTiereToJson(tiere);
                    Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                    Console.ReadKey();
                    Console.Clear(); // Leert die Konsole nach dem Hinzufügen eines Tieres
                }
                else if (wahl == "2") // Option 2: Alle Tiere anzeigen
                {
                    Console.WriteLine("Alle Tiere im Heim:");
                    LoadTiereFromJson();
                    foreach (var tier in tiere) // Gehe durch die Liste der Tiere
                    {
                        Console.WriteLine($"Name: {tier.Name}, Alter: {tier.Alter} Jahre");
                        tier.LautGeben(); // Rufe die LautGeben-Methode auf
                    }
                    Console.WriteLine("Drücke eine Taste, um fortzufahren...");
                    Console.ReadKey();
                    Console.Clear(); // Leert die Konsole nach der Anzeige

                }
                else if (wahl == "4") // Option 4: Tiere speichern
                {
                    SaveTiereToJson(tiere);
                    Console.WriteLine("Tier gespeichert\nDrücke eine Taste, um fortzufahren...");
                    Console.ReadKey();
                    Console.Clear(); // Leert die Konsole nach dem Speichern
                }
                else if (wahl == "3") // Option 3: Programm beenden
                {
                    Console.WriteLine("Programm wird beendet.");
                    break; // Beende die Schleife
                }
                else
                {
                    Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut."); // Fehlermeldung
                }
            }
        }

        // Methode zum Speichern der Tiere in einer JSON-Datei
        static void SaveTiereToJson(List<Tier> tiere)
        {
            string json = JsonSerializer.Serialize(tiere, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("tiere.json", json);
            Console.WriteLine("Tiere wurden in tiere.json gespeichert!");
        }
        static List<Tier> LoadTiereFromJson()
        {
            if (File.Exists("tiere.json")) // Überprüfen, ob die Datei existiert
            {
                string json = File.ReadAllText("tiere.json"); // Lese den JSON-String aus der Datei
                return JsonSerializer.Deserialize<List<Tier>>(json); // Deserialisiere den JSON-String in eine Liste von Tieren
            }
            return new List<Tier>(); // Gib eine leere Liste zurück, wenn die Datei nicht existiert
        }

    }
}
