namespace miniproject_p0;

using System.Collections.Generic;

//self note: maybe applying FileI/O to persist data
//also, might wanna wrap menu & option in while loop!

class Program
{
    static void Main(string[] args)
    {
        //declare list
        List<DrinkModel> drinkList = [];
        
        //load drinks---------------------
        

        bool isExit = false;
        while(!isExit){
            //prompt
            Util.PrintMenu();
            Console.Write("\nChoose your option: ");
            //read input, if null, set to 5 (exit)
            string? option = Console.ReadLine() ?? "5";

            //call method
            int validatedOption = Validations.ValidateNumber(option);

            switch(validatedOption){
                //show current drinks
                case 1:
                    ShowAllDrinks(drinkList);
                    break;
                //add new type of drinks
                case 2:
                    AddTypeDrinks(drinkList);
                    break;
                //get the same drinks
                case 3:
                    AddNumDrinks(drinkList);
                    break;
                //find total and avg on given drink
                case 4:
                    CalculateAvgAndTotal(drinkList);
                    break;
                //save and exit
                case 5:
                    //todo: save---------------------------
                    isExit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option! try again!");
                    break;

            }

            Console.WriteLine("\n\n");
        }
    }

    //for option 1:
    public static void ShowAllDrinks(List<DrinkModel> list){
        if(list.Count == 0) Console.WriteLine("No drinks yet, you must be thirsty!?");
        else{
            foreach (DrinkModel drink in list){
                Console.WriteLine(drink);
            }
        }
    }

    //for option 2:
    public static void AddTypeDrinks(List<DrinkModel> list){

        string name = Validations.GetValidName();
        double price = Validations.GetValidPrice();
        
        //valid response, proceed to adding
        DrinkModel newDrink = new DrinkModel{Name = name, Price = price, TimesConsumed = 1};
        list.Add(newDrink);
    }

    //for option 3:
    public static void AddNumDrinks(List<DrinkModel> list){
        //prompt, show the current drink list
        Console.WriteLine("Your good old drinks are: ");
        foreach(DrinkModel drink in list){
            Console.Write(drink.Name + ", ");
        }

        //prompt, get input
        Console.Write("\nWhat are you drinking today? :");
        string name = Validations.GetValidName();
        double price;

        foreach(DrinkModel drink in list){
            if(drink.Name.Equals(name)){
                //proceed to add price, timesConsumed
                price = Validations.GetValidPrice();
                drink.Price += price;
                drink.TimesConsumed += 1;
                return;
            }
        }
        Console.WriteLine("Your drink is not found, try again!");
    }

    //for option 4:
    public static void CalculateAvgAndTotal(List<DrinkModel> list){
        //prompt, show the current drink list
        Console.WriteLine("Your drinks option: ");
        foreach(DrinkModel drink in list){
            Console.Write(drink.Name + ", ");
        }


        //prompt, get input
        Console.WriteLine("\nWhich drink you'd like to find avg. and total?: ");
        string name = Validations.GetValidName();

        foreach(DrinkModel drink in list){
            if(drink.Name.Equals(name)){
                double avg = drink.Price / drink.TimesConsumed;
                Console.WriteLine($"You spent a total of ${drink.Price} on {name} for {drink.TimesConsumed} times, with an average of ${avg:F2} per time!");
            }
        }
        Console.WriteLine("Your drink is not found, try again!");
    }
}
