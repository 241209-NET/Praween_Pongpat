using System.Text.Json;

namespace miniproject_p0;

public class Util{
    //menu option
    public static void PrintMenu(){
        Console.WriteLine("Please follow the menu:");
        Console.WriteLine("1: See current type of Drinks");
        Console.WriteLine("2. Add new type of Drinks");
        Console.WriteLine("3. Drinking your good old drinks");
        Console.WriteLine("4. Find total and avg. spent on specific Drink");
        Console.WriteLine("5. Quit");
    }

    //load, draft up, might implement later
    public static List<DrinkModel> LoadFromFile(string fileName){
        if (File.Exists(fileName)){
            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<DrinkModel>>(json) ?? new List<DrinkModel>();
        }
        return new List<DrinkModel>();
    }

    //save, draft up, might implement later
    public static void SaveToFile(string fileName, List<DrinkModel> list){
        string json = JsonSerializer.Serialize(list);
        File.WriteAllText(fileName, json);
    }
}