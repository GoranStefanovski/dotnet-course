using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // string[] names = [ "John", "Jane" ];
            // string[] names = new string[2];

            // List<string> namesList = new List<string>();

            // namesList.Add("John");
            // namesList.Add("Jane");
            // namesList.Add("Goran");
            // Console.WriteLine(namesList[0]);
            // Console.WriteLine(namesList[1]);
            // Console.WriteLine(namesList[2]);
            // Console.WriteLine(namesList.Count);

            // List<string> myGroceryList = ["Bread", "Milk", "Eggs"];

            // Console.WriteLine(myGroceryList[0]);
            // Console.WriteLine(myGroceryList[1]);
            // Console.WriteLine(myGroceryList[2]);
            // Console.WriteLine(myGroceryList.Count);

            // IEnumerable<string> myGroceryListEnumerable = myGroceryList;

            // List<string> myGroceryList2 = myGroceryListEnumerable.ToList();

            // Console.WriteLine(myGroceryList2[0]);

            // string[,] myMultiDimensionalArray = {
            //     {"Bread", "Milk", "Eggs"},
            //     {"Jajnca", "Ovaj", "Neshto"},
            //     {"Kava", "Kafe", "Kakao"}
            // };

            // Console.WriteLine(myMultiDimensionalArray[2, 2]);
        

            // int[,,] myMultiDimensionalArray = {
            //     {
            //         {1, 2, 3},
            //         {4, 5, 6},
            //         {7, 8, 9}
            //     },
            //     {
            //         {10, 11, 12},
            //         {13, 14, 15},
            //         {16, 17, 18}
            //     },
            //     {
            //         {19, 20, 21},
            //         {22, 23, 24},
            //         {25, 26, 27}
            //     }
            // };

            // System.Console.WriteLine(myMultiDimensionalArray[2, 2, 2]);
        
            // Dictionary<string, int> groceryItemPrices = new Dictionary<string, int>();

            // groceryItemPrices["Bread"] = 123;

            // Console.WriteLine(groceryItemPrices["Bread"]);
 
            // Operators and Conditionals
            // int[] intsToCompress = new int[] {10, 15, 20, 25, 30, 35, 40, 45, 50};
            // var ourValue = GetSum(intsToCompress);
            // Console.WriteLine(ourValue + 1);

            // Console.Write("Hello: ");

            // Console.WriteLine("Goran Stefanovski");

            // Simple Addition Program
            // Console.WriteLine("=== Addition Calculator ===");
            
            // Console.Write("Enter first number: ");
            // string input1 = Console.ReadLine();
            
            // Console.Write("Enter second number: ");
            // string input2 = Console.ReadLine();
            
            // // Convert strings to numbers
            // if (int.TryParse(input1, out int num1) && int.TryParse(input2, out int num2))
            // {
            //     int result = num1 + num2;
            //     Console.WriteLine($"{num1} + {num2} = {result}");
            // }
            // else
            // {
            //     Console.WriteLine("Please enter valid numbers!");
            // }


             // // Divide Two Numbers
             // Console.WriteLine("=== Division Calculator ===");

             // Console.Write("Enter first number: ");
             // string input1 = Console.ReadLine();

             // Console.Write("Enter second number: ");
             // string input2 = Console.ReadLine();
             
             // if (int.TryParse(input1, out int num1) && int.TryParse(input2, out int num2))
             // {
             //     int result = num1 / num2;
             //     Console.WriteLine($"{num1} / {num2} = {result}");
             // }
             // else
             // {
             //     Console.WriteLine("Please enter valid numbers!");
             // }

             // Specified Operations Results
            //  Console.WriteLine("=== Specified Operations Results ===");
             
            //  // Operation 1: -1 + 4 * 6
            //  int result1 = -1 + 4 * 6;
            //  Console.WriteLine(result1); // Expected: 23
             
            //  // Operation 2: (35 + 5) % 7
            //  int result2 = (35 + 5) % 7;
            //  Console.WriteLine(result2); // Expected: 5
             
            //  // Operation 3: 14 + -4 * 6 / 11
            //  int result3 = 14 + -4 * 6 / 11;
            //  Console.WriteLine(result3); // Expected: 12
             
            //  // Operation 4: 2 + 15 / 6 * 1 - 7 % 2
            //  int result4 = 2 + 15 / 6 * 1 - 7 % 2;
            //  Console.WriteLine(result4); // Expected: 3

            // Number Swapping Program
            // Console.WriteLine("\n=== Number Swapping Program ===");
            
            // Console.Write("Input the First Number: ");
            // string firstInput = Console.ReadLine();
            
            // Console.Write("Input the Second Number: ");
            // string secondInput = Console.ReadLine();
            
            // if (int.TryParse(firstInput, out int firstNumber) && int.TryParse(secondInput, out int secondNumber))
            // {
            //     Console.WriteLine($"Before Swapping:");
            //     Console.WriteLine($"First Number: {firstNumber}");
            //     Console.WriteLine($"Second Number: {secondNumber}");
                
            //     // Swap the numbers using a temporary variable
            //     int temp = firstNumber;
            //     firstNumber = secondNumber;
            //     secondNumber = temp;
                
            //     Console.WriteLine($"After Swapping:");
            //     Console.WriteLine($"First Number: {firstNumber}");
            //     Console.WriteLine($"Second Number: {secondNumber}");
            // }
            // else
            // {
            //     Console.WriteLine("Please enter valid numbers!");
            // }
        }

        static private int GetSum(int[] intsToCompress)
        {
            int totalValue = 0;
            foreach (int i in intsToCompress)
            {
                totalValue += i;
            }
            return totalValue;
        }
    }
}