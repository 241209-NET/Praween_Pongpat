namespace miniproject_p0;

//just a model of the drink, with getters/setters
public class DrinkModel{
    //try to work using properties!
    public string Name { get; set; } = "";
    public double Price { get; set; }
    public int TimesConsumed { get; set; }

    //override ToString
    public override string ToString()
    {
        return Name + " was chosen! with total spent of " + Price + " for " + TimesConsumed + " times!";
    }
}