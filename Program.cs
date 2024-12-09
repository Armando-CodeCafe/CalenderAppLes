namespace DateLes;

using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        DateTime day = DateTime.Now; // Huidige datum
        // Maak een prompt aan die ons uiteindelijk een string moet terug geven
        SelectionPrompt<string> prompt = new SelectionPrompt<string>();
        prompt.Title("Wat wil je doen?"); // Titel van de prompt
        // Voeg opties toe aan de prompt
        prompt.AddChoice("vorige"); // Optie om naar de vorige dag te gaan
        prompt.AddChoice("volgende"); // Optie om naar de volgende dag te gaan
        prompt.AddChoice("toevoegen"); // Optie om een todo toe te voegen
        prompt.AddChoice("stoppen"); // Optie om het programma te stoppen

        Dictionary<string, List<Todo>> todos = new Dictionary<string, List<Todo>>(); // Dictionary voor het opslaan van todos per datum

        while (true)
        {
            Console.Clear(); // Maak het console scherm schoon
            string dateString = day.ToString("d"); // Formatteer de datum als string

            // Vandaag is maandag de 9e december
            Console.WriteLine(
                $"Vandaag is {day.ToString("dddd")} de {day.Day}e van {day.ToString("MMMM")}, {day.ToString("yyyy")}\n"
            );
            Rule rule = new Rule();
            AnsiConsole.Write(rule);
            if (todos.ContainsKey(dateString))
            {
                Console.WriteLine("Todos op deze dag:");
                foreach (Todo t in todos[dateString])
                {
                    Console.WriteLine($"- {t.Time}: {t.Name}"); // Toon de todos voor de huidige datum
                }
            }

            // Stel de vraag aan de gebruiker
            string choice = AnsiConsole.Prompt(prompt);
            if (choice == "vorige")
            {
                // Ga een dag terug
                day = day.AddDays(-1);
            }
            else if (choice == "volgende")
            {
                // Ga een dag vooruit
                day = day.AddDays(1);
            }
            else if (choice == "toevoegen")
            {
                // Code voor het toevoegen van een todo
                Console.Clear();
                Console.WriteLine($"Je staat op het punt een todo toe te voegen");
                string name = AnsiConsole.Ask<string>("Wat is de naam van de todo? (bijv. Eten)"); // Vraag om de naam van de todo
                string time = AnsiConsole.Ask<string>("Om hoe laat is de todo? (bijv. 14:00)"); // Vraag om de tijd van de todo
                Todo todo = new Todo();
                todo.Name = name; // Stel de naam in
                todo.Time = time; // Stel de tijd in
                if (todos.ContainsKey(dateString))
                {
                    // Voeg de todo toe aan de lijst van todos
                    todos[dateString].Add(todo);
                }
                else
                {
                    // Maak eerst de lijst en voeg daarna de todo toe
                    todos.Add(dateString, new List<Todo>());
                    todos[dateString].Add(todo);
                }
                Console.ReadLine(); // Wacht op invoer van de gebruiker
            }
            else if (choice == "stoppen")
            {
                // Code voor stoppen
                break; // Breek de loop en stop het programma
            }
        }
    }
}
