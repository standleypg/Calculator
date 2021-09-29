using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    class Program
    {
        static string[] arr;
        int startIndex = 0;

        static void Main(string[] args)
        {
            System.Console.Write("String input: ");
            System.Console.WriteLine(Calculate(Console.ReadLine()).ToString("#.##"));

            Console.ReadLine();
        }

        public static double Calculate(string str)
        {
            arr = str.Split(" ");
            Program program = new Program();
            return program.Handler();
        }

        public double Handler()
        {
            Stack<double> stack = new Stack<double>();
            string currentOperator = "+";
            double totalResult = 0;
            while (startIndex < arr.Length)
            {
                // System.Console.WriteLine($"Element at index[{startIndex}] is : {arr[startIndex]}");
                //check if the current index value is number
                if (double.TryParse(arr[startIndex], out _))
                {
                    //is number?  then we assign it to current number
                    double currentNumber = double.Parse(arr[startIndex]);
                    StackOperation(stack, currentNumber, currentOperator);
                }
                else if (arr[startIndex].Equals("("))
                {
                    startIndex++;

                    double curNum = Handler();
                    StackOperation(stack, curNum, currentOperator);
                }
                else if (arr[startIndex].Equals(")"))
                {
                    break;
                }
                //else, if not number, then it's operator
                else
                {
                    currentOperator = arr[startIndex];
                }

                startIndex++;
            }
            //while the stack is not empty, we total up the number inside the stack
            while (!stack.Count.Equals(0))
            {
                totalResult += stack.Pop();
                // System.Console.WriteLine($"Total {totalResult}");
            }
            return totalResult;
        }

        public void StackOperation(Stack<double> stack, double currentNumber, string currentOperator)
        {
            switch (currentOperator)
            {
                case "-":
                    //if the current operator is minus sign, we multiply the current number with -1 to get he negative value
                    currentNumber *= -1;
                    break;
                case "+":
                    //do nothing
                    break;
                case "*":
                    //multiply between most recent stack and current number
                    currentNumber *= stack.Pop();
                    break;
                case "/":
                    //divide between most recent stack and current number
                    currentNumber = stack.Pop() / currentNumber;
                    break;
            }
            // push current number into the stack
            // System.Console.WriteLine($"pushing {currentNumber} to the stack");
            stack.Push(currentNumber);
        }
    }
}
