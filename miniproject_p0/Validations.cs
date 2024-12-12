namespace miniproject_p0;

public class Validations{
    //helper method: using tryParse for validation for number
    public static int ValidateNumber(string str){
        if(int.TryParse(str, out int num)){
            return num;
        }
        else return 5;
    }

    //helper method: validate for letter
    public static string ValidateLetter(string str){
        //condition: not null, no blank space, white space, no number
        if(string.IsNullOrWhiteSpace(str) || !str.All(char.IsLetter)){
            //failed condition
            Console.WriteLine("Drink name cannot be blank and/or numbers!");
            return "";
        }
        return str;
    }

    //helper method: validate for double
    public static double ValidaterNonNegativeFloat(string str){
        //condition: not null, must be non-negative numbers
        if(string.IsNullOrWhiteSpace(str) || !double.TryParse(str, out double num) || num < 0) {
            //failed condition
            Console.WriteLine("Drink price must be numbers and/or negative");
            return -1.00;
        }
        return num;
    }

    //get valid drink name
    public static string GetValidName(){
        string str = "";
        do{
            //prompt user for drink's name
            Console.Write("Enter name of the Drink: ");
            //read input, send to letter validation
            str = ValidateLetter(Console.ReadLine()!);            
        }while(str.Equals(""));

        //valid name, return
        return str;
    }

    //get valid price
    public static double GetValidPrice(){
    double price;
    do{
        // Prompt user for the drink's price
        Console.Write("Enter price: ");
        // Read input and send to double validation
        price = ValidaterNonNegativeFloat(Console.ReadLine()!);

    } while (price <= 0); // Loop continues until a valid positive price is provided

    // Valid price, return
    return price;
}

    
}