public class Day4 
{
    public void Run() 
    {

    }
} 


public class Card
{


    private readonly string[] winningNumbers;
    private readonly string[] cardNumbers;


    public Card(string[] winningNumbers, string[] cardNumbers)
    {
        this.winningNumbers = winningNumbers;
        this.cardNumbers = cardNumbers;
    }
}