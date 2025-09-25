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
            int[] intsToCompress = new int[] {10, 15, 20, 25, 30, 35, 40, 45, 50};
            var ourValue = GetSum(intsToCompress);
            Console.WriteLine(ourValue + 1);

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