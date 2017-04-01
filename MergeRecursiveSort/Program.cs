using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace MergeInverseSort
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Write not sorted int32 array");
            try
            {
                string[] stringArray = Console.ReadLine().Trim().Split(new char[] { ' ', '\n', '\t' });
                int lengthArray = stringArray.Length;
                int[] intArray = new int[lengthArray];
                for (int i = 0; i < lengthArray; i++)
                {
                    intArray[i] = Int32.Parse(stringArray[i]);
                }
                MergeSort(ref intArray, lengthArray);
                for (int i = 0; i < lengthArray; i++)
                    Console.Write($"{intArray[i]} ");
                Console.ReadKey();
            }
            catch (OverflowException)
            {
                Console.WriteLine("Too big or too small writed number");
                Console.ReadKey();
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong writed format");
                Console.ReadKey();
            }
        }

        static void MergeSort(ref int[] a, int length)
        {
            InverseMergeSort(ref a, 0, length);
        }
        static void InverseMergeSort(ref int[] a, int leftSide, int rightSide)
        {
            
            if (rightSide - leftSide == 1) // Raise on function with 1 element
            {
                return;
            } // And begin to sort merge
            InverseMergeSort(ref a, leftSide, (leftSide + rightSide) / 2); // Two functions for every half array
            InverseMergeSort(ref a, (leftSide + rightSide) / 2, rightSide);
            Stack<int> myStack = new Stack<int>();

            for (int leftSideCounter = 0, rightSideCounter = 0, mid = (leftSide + rightSide) / 2; ;) {
                if (a[leftSide + leftSideCounter] > a[mid + rightSideCounter])
                {
                    myStack.Push(a[mid + rightSideCounter]);
                    rightSideCounter++;
                }
                else
                {
                    myStack.Push(a[leftSide + leftSideCounter]);
                    leftSideCounter++;
                }
                if (leftSide + leftSideCounter == mid) // If left part of couple has already counted
                {
                    while (mid + rightSideCounter != rightSide) // If right part of couple is consisting elements 
                    {
                        myStack.Push(a[mid + rightSideCounter]);
                        rightSideCounter++;
                    }
                    break;
                }
                else if (mid + rightSideCounter == rightSide)
                {
                    while (leftSide + leftSideCounter != mid)
                    {
                        myStack.Push(a[leftSide + leftSideCounter]);
                        leftSideCounter++;
                    }
                    break;
                }
            }
            for (int stackLength = myStack.Count, i = 1; stackLength > 0; stackLength--, i++) // Array with sorted couples 
                a[rightSide - i] = myStack.Pop();
        }

    }
}