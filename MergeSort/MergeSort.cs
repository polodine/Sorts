using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp3
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
                //System.Diagnostics.Stopwatch sw = new Stopwatch();
                //sw.Start();
                MergeSort(ref intArray, lengthArray);
                //sw.Stop();
                for (int i = 0; i < lengthArray; i++)
                    Console.Write($"{intArray[i]} ");
                //TODO: Console.WriteLine($"\nElapsed time: {sw.ElapsedMilliseconds / 100.0} seconds");
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
            for (int merges = 2; merges < length*2; merges *= 2) // Divides array into parts with length - merges, that will be merge
            {
                Stack<int> myStack = new Stack<int>(); // new Stack for sorted couples
                for (int couples = 0; couples < length - merges/2; couples += merges) // Handler couples that counted leftSide of each merges
                {
                    int leftSideOfFirstOne = couples;
                    int leftSideOfSecondOne = couples + merges / 2; 
                    int rightSideOfSecondOne = couples + merges < length ? couples + merges : length;

                    for (int leftCounter = leftSideOfFirstOne, rightCounter = leftSideOfSecondOne; ;) // Completes sort for each couple (leftCounter, rightCounter - counters for each part of couple)
                    {
                        if (a[leftCounter] <= a[rightCounter])
                        {
                            myStack.Push(a[leftCounter]);
                            leftCounter++;
                        }
                        else
                        {
                            myStack.Push(a[rightCounter]);
                            rightCounter++;
                        }
                        if (leftCounter == leftSideOfSecondOne) // If left part of couple has already counted
                        {
                            while (rightCounter != rightSideOfSecondOne) // If right part of couple is consisting elements 
                            {
                                myStack.Push(a[rightCounter]);
                                rightCounter++;
                            }
                            break;
                        }
                        else if (rightCounter == rightSideOfSecondOne)
                        {
                            while (leftCounter != leftSideOfSecondOne)
                            {
                                myStack.Push(a[leftCounter]);
                                leftCounter++;
                            }
                            break;
                        }
                    }
                }

                for (int stackLength = myStack.Count; stackLength > 0; stackLength--) // Array with sorted couples with length = merges
                    a[stackLength-1] = myStack.Pop();
            }
        }
       
    }
}