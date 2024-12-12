namespace miniproject_p0;

public class Util{
    //menu option
    public static void PrintMenu(){
        Console.WriteLine("Welcome to Drink Expense tracker! Please follow the menu:");
        Console.WriteLine("1: See current type of Drinks");
        Console.WriteLine("2. Add new type of Drinks");
        Console.WriteLine("3. Drinking your good old drinks");
        Console.WriteLine("4. Find total and avg. spent on specific Drink");
        Console.WriteLine("5. Quit");
    }

    //load
    public static List<DrinkModel> LoadFromFile(string fileName){
        return [];
    }

    //save
    public static void SaveToFile(string fileName, List<DrinkModel> list){

    }
}