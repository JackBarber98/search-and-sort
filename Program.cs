//Created by Jack Barber (17633953) / 2018

using System;
using System.IO;
using System.Diagnostics;

namespace CMP1124MAssessment
{
    class Program
    {
        static void Main(string[] args)
        {
            //Launches the main menu when the program loads
            Interface.SelectOperation();
        }
    }

    class Interface
    {
        //Handles menu options - maximum and minimum are the smallest and largest values in a numbered menu
        public static int MenuOptionHandler(int minimum, int maximum)
        {
            try
            {
                /*Attempt to read user input as an integer, but if the input is out of range or is not an integer, 
                 * request a new input from the user */
                int option = int.Parse(Console.ReadLine());

                while (option < minimum || option > maximum)
                {
                    Console.WriteLine("That is not a valid option. Please try again. ");
                    option = int.Parse(Console.ReadLine());
                }

                //Return the user's choice as an integer
                return option;
            }
            catch
            {
                Console.WriteLine("That is not a valid option. Please try again. ");
            }

            return 0;
        }

        //Handles user inputs that are data-based (i.e. values to search for)
        public static float DataInputHandler()
        {
            try
            {
                /*Attempt to return the user's input as a floating point value. 
                 * If it is not a floating point number, request a new input from the user */
                return float.Parse(Console.ReadLine());
            } catch
            {
                Console.WriteLine("That is not a valid input. Please try again. ");
            }

            return 0;
        }

        //Displays a main menu to the user, requesting a function to be specified
        public static void SelectOperation()
        {
            Console.WriteLine("Select a menu option: ");
            Console.WriteLine("1. Sort a single dataset");
            Console.WriteLine("2. Sort a merged dataset");
            Console.WriteLine("3. Exit application");
            Console.WriteLine();

            //The user's menu option is stored as an integer
            int option = MenuOptionHandler(1, 3);

            switch (option)
            {
                //If the user selected option 1, move on to the 'select dataset' menu
                case 1:
                    SelectDataset();
                    break;
                //If option 2 was selected, move on to the 'select merged dataset' menu
                case 2:
                    SelectMergedDataset();
                    break;
                //If option 3 was selected, exit the program with exit code 1
                case 3:
                    Environment.Exit(1);
                    break;
            }
        }

        //Displays a list of possible shares data and asks the user to select one to sort
        public static void SelectDataset()
        {
            Console.WriteLine("Select a type of share data to sort: ");
            Console.WriteLine("1. Stock high");
            Console.WriteLine("2. Stock low");
            Console.WriteLine("3. Stock opening");
            Console.WriteLine("4. Stock closing");
            Console.WriteLine("5. Stock change");
            Console.WriteLine();

            //Assigns the user's menu choice to a variable
            int option = MenuOptionHandler(1, 5);

            switch (option)
            {
                /*If the user selects option 1, the file prefix will be set to "high_" 
                 * and the program will advance to request a number of datapoints from the user */
                case 1:
                    SelectDatasetLength("high_");
                    break;
                /*If the user selects option 2, the file prefix will be set to "low_" 
                 * and the program will advance to request a number of datapoints from the user */
                case 2:
                    SelectDatasetLength("low_");
                    break;
                /*If the user selects option 3, the file prefix will be set to "open_" 
                 * and the program will advance to request a number of datapoints from the user */
                case 3:
                    SelectDatasetLength("open_");
                    break;
                /*If the user selects option 4, the file prefix will be set to "close_" 
                 * and the program will advance to request a number of datapoints from the user */
                case 4:
                    SelectDatasetLength("close_");
                    break;
                /*If the user selects option 5, the file prefix will be set to "change_" 
                 * and the program will advance to request a number of datapoints from the user */
                case 5:
                    SelectDatasetLength("change_");
                    break;
            }
        }

        /*Requests that the user makes a decision on the number of datapoints the program is going to analyse,
         * and obtains the corresponding array */
        public static void SelectDatasetLength(string prefix)
        {
            Console.WriteLine("Select a dataset length: ");
            Console.WriteLine("1. 128 datapoints");
            Console.WriteLine("2. 256 datapoints");
            Console.WriteLine("3. 1024 datapoints");
            Console.WriteLine();

            //Assigns the user's input to an integer and initialises a floating point array to store all relevent stock values in
            int option = MenuOptionHandler(1, 3);
            float[] stockArray;

            switch (option)
            {
                /*If the user selects option 1, an array containing 128 values of the type of share data the user specified will be returned and assigned to the 
                empty floating point array */
                case 1:
                    stockArray = GetArray(prefix+"128.txt");
                    break;
                /*If the user selects option 2, an array containing 256 values of the type of share data the user specified will be returned and assigned to the 
                empty floating point array */
                case 2:
                    stockArray = GetArray(prefix+"256.txt");
                    break;
                /*If the user selects option 3, an array containing 1024 values of the type of share data the user specified will be returned and assigned to the 
                empty floating point array */
                case 3:
                    stockArray = GetArray(prefix+"1024.txt");
                    break;
                default:
                    stockArray = GetArray(prefix+"128.txt");
                    break;
            }

            //Advance the program to the sorting algorithm selection menu
            SelectSortingAlgorithm(stockArray);
        }

        //Displays a menu of possible merged datasets to create, and then progresses to ask the user which sorting algorithm to use
        public static void SelectMergedDataset()
        {
            Console.WriteLine("Select a merged dataset: ");
            Console.WriteLine("1. Stock close dataset and stock high dataset (128 datapoints each)");
            Console.WriteLine("2. Stock close dataset and stock high dataset (256 datapoints each)");
            Console.WriteLine("3. Stock close dataset and stock high dataset (1024 datapoints each)");
            Console.WriteLine();

            //Assigns the user's choice to an integer
            int option = MenuOptionHandler(1, 3);
            float[] stockArray;

            switch (option)
            {
                //If the user selected option 1, return an array containing data from both the "close_128.txt" file and the "high_128.txt" file
                case 1:
                    stockArray = GetMergedArray("close_128.txt", "high_128.txt");
                    break;
                //If the user selected option 2, return an array containing data from both the "close_256.txt" file and the "high_256.txt" file
                case 2:
                    stockArray = GetMergedArray("close_256.txt", "high_256.txt");
                    break;
                //If the user selected option 3, return an array containing data from both the "close_1024.txt" file and the "high_1024.txt" file
                case 3:
                    stockArray = GetMergedArray("close_1024.txt", "high_1024.txt");
                    break;
                default:
                    stockArray = GetMergedArray("close_128.txt", "high_128.txt");
                    break;
            }

            //Progress to ask the user which sorting algorithm they want to use to sort the dataset
            SelectSortingAlgorithm(stockArray);
        }

        //Returns an array of values contained within a specified text file
        public static float[] GetArray(string fileName)
        {


            float[] stockArray = new float[File.ReadAllLines(fileName).Length];

            int count = 0;

            //Stores each line of the text file as a floating point value in an array
            foreach (string s in File.ReadAllLines(fileName))
            {
                stockArray[count] = float.Parse(s);
                count++;
            }

            return stockArray;
        }

        //Combines all of the data within two text files into one floating point array
        public static float[] GetMergedArray(string closeDataset, string highDataset)
        {

            //Creates a new floating point array with the sum of the lengths of the two datasets
            float[] stockArray = new float[File.ReadAllLines(closeDataset).Length + File.ReadAllLines(highDataset).Length];

            //Stores each value of the "closing" text file to the stock array, followed by all values in the "high" text file
            int count = 0;
            foreach (string s in File.ReadAllLines(closeDataset))
            {
                stockArray[count] = float.Parse(s);
                count++;
            }
            foreach (string s in File.ReadAllLines(highDataset))
            {
                stockArray[count] = float.Parse(s);
                count++;
            }

            //Return the merged array
            return stockArray;
        }

        //Displays a list of sorting algorithms to the user and asks them to choose one to sort the dataset with
        public static void SelectSortingAlgorithm(float[] stockArray)
        {
            //Creates a stopwatch object to allow the chosen algorithm to be timed during execution
            Stopwatch stopwatch = new Stopwatch();
            //Creates an integer variable for storing how many iterations were required to sort the dataset with a specific algorithm
            int iterations = 0;

            Console.WriteLine("Select a sorting algorithm: ");
            Console.WriteLine("1. Bubble sort");
            Console.WriteLine("2. Merge sort");
            Console.WriteLine("3. Quick sort");
            Console.WriteLine("4. Heap sort");
            Console.WriteLine("5. Insertion sort");
            Console.WriteLine();

            //Return the user's menu choice an assign it to a variable
            int option = MenuOptionHandler(1, 5);

            switch (option)
            {
                /*If the user selected option 1, run the bubble sort algorithm with the shares array. 
                Time how long it takes to execute, and record the number of iterations required to sort the array */
                case 1:
                    stopwatch.Start();
                    BubbleSort.Sort(stockArray);
                    stopwatch.Stop();
                    iterations = BubbleSort.iterations;
                    break;
                /*If the user selected option 2, run the merge sort algorithm with the shares array. 
                Time how long it takes to execute, and record the number of iterations required to sort the array */
                case 2:
                    stopwatch.Start();
                    MergeSortAlgorithm.MergeSort(stockArray, 0, stockArray.Length - 1);
                    stopwatch.Stop();
                    iterations = MergeSortAlgorithm.iterations;
                    break;
                /*If the user selected option 3, run the quick sort algorithm with the shares array. 
                Time how long it takes to execute, and record the number of iterations required to sort the array */
                case 3:
                    stopwatch.Start();
                    QuickSortAlgorithm.QuickSort(stockArray, 0, stockArray.Length - 1);
                    stopwatch.Stop();
                    iterations = QuickSortAlgorithm.iterations;
                    break;
                /*If the user selected option 4, run the heap sort algorithm with the shares array. 
                Time how long it takes to execute, and record the number of iterations required to sort the array */
                case 4:
                    stopwatch.Start();
                    HeapSortAlgorithm.HeapSort(stockArray);
                    stopwatch.Stop();
                    iterations = HeapSortAlgorithm.iterations;
                    break;
                /*If the user selected option 5, run the insertion sort algorithm with the shares array. 
                Time how long it takes to execute, and record the number of iterations required to sort the array */
                case 5:
                    stopwatch.Start();
                    InsertionSortAlgorithm.InsertionSort(stockArray, stockArray.Length - 1);
                    stopwatch.Stop();
                    iterations = InsertionSortAlgorithm.iterations;
                    break;
            }

            //If the user wants to display the data in descending order, reverse the array and print each value of this array seperated by a comma
            if (CheckIfDescendingOrder())
            {
                int count = 0;

                Console.WriteLine("Dataset in descending order: \n");
                foreach (float value in GetInDescendingOrder(stockArray))
                {
                    if (count != stockArray.Length - 1)
                    {
                        Console.Write(value + ", ");
                        count++;
                    }
                    //If the value being printed is the last value in the array, do not add a comma to the end of it
                    else
                    {
                        Console.WriteLine(value);
                    }
                    
                }
            }
            else
            {
                //If the user wishes to display data in ascending order, print all values of the sorted array, seperated by commas
                Console.WriteLine("Dataset in ascending order: \n");
                foreach (float value in stockArray)
                {
                    Console.Write(value + ", ");
                }
            }

            //Display information to the user about how long the algorithm took to execute as well as the number of iterations required
            Console.WriteLine();
            Console.WriteLine("\nTime elapsed: {0}", stopwatch.Elapsed);
            Console.WriteLine("Number of iterations: {0}", iterations);
            Console.WriteLine();

            /*If the user wants to search for a value within the sorted dataset, obtain a value to search 
            for as well as the searching algorithm to be used */
            if (ToSearch())
            {
                float toFind = SelectValueToSearch();
                SelectSearchingAlgorithm(stockArray, toFind);
            }

            //Return the user to the main menu
            SelectOperation();
        }

        //Asks the user if they want to view data in ascending or descending order, and return a boolean value depending on their input
        public static bool CheckIfDescendingOrder()
        {
            Console.WriteLine("Select the order you would like the data to be shown in: ");
            Console.WriteLine("1. Ascending");
            Console.WriteLine("2. Descending");
            Console.WriteLine();

            //Assign the user's input to an integer variable
            int option = MenuOptionHandler(1, 2);

            switch (option)
            {
                //If the user selected option 1, return false
                case 1:
                    return false;
                //If the suer selected option 2, return true
                case 2:
                    return true;
                default:
                    return false;
            }
        }

        //Reverses a sorted array so that it is in descending order
        public static float[] GetInDescendingOrder(float[] stockArray)
        {
            //All values that are equidistance from the centre of the array are swapped, leading to the creation of a reversed array
            for (int i = 0; i < stockArray.Length / 2; i ++)
            {
                GenericSortingAlgorithms.Swap(stockArray, i, stockArray.Length - i - 1);
            }

            //Return the reversed array
            return stockArray;
        }

        //Ask the user if they want to search the sorted dataset for a specific value and returns a boolean value based on their input
        public static bool ToSearch()
        {
            Console.WriteLine("Would you like to search the sorted data for a specific value? ");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            Console.WriteLine();

            //Assign the user's input to an integer variable
            int option = MenuOptionHandler(1, 2);

            switch (option)
            {
                //Return true if option 1 is selected
                case 1:
                    return true;
                //Return false if option 2 is selected
                case 2:
                    return false;
                default:
                    return false;
            }
        }
            
        //Asks the user to input a value to search for, and returns their input as a floating point value
        public static float SelectValueToSearch()
        {
            Console.WriteLine("Select a value to search for: ");
            return DataInputHandler();
        }

        //Asks the user to select a searching algorithm to use in order to find the value they inputted, and runs that algorithm
        public static void SelectSearchingAlgorithm(float[] stockArray, float toFind)
        {
            //Creates a stopwatch object to allow the chosen algorithm to be timed during execution
            Stopwatch stopwatch = new Stopwatch();
            //Creates an integer variable for storing how many iterations were required to search the dataset with a specific algorithm
            int iterations = 0;

            Console.WriteLine("Select a searching algorithm: ");
            Console.WriteLine("1. Binary search");
            Console.WriteLine("2. Binary search tree");
            Console.WriteLine("3. Interpolation search");

            //Assigns the user's input to an integer variable
            int option = MenuOptionHandler(1, 3);

            switch (option)
            {
                //If option 1 was selected, run a binary search on the array to find the value
                case 1:
                    stopwatch.Start();
                    BinarySearchAlgorithm.BinarySearch(stockArray, toFind, 0 , stockArray.Length - 1);
                    iterations = BinarySearchAlgorithm.iterations;
                    stopwatch.Stop();
                    break;
                //If option 2 was selected, run the binary search tree algorithm on the array to find the value
                case 2:
                    stopwatch.Start();
                    BST.InitialiseTree(stockArray, toFind);
                    iterations = BST.iterations;
                    stopwatch.Stop();
                    break;
                //If option 3 was selected, run the interpolation search algorithm on the array to find the value
                case 3:
                    stopwatch.Start();
                    InterpolationSearchAlgorithm.InterpolationSearch(stockArray, toFind, 0, stockArray.Length - 1);
                    iterations = InterpolationSearchAlgorithm.iterations;
                    stopwatch.Stop();
                    break;
            }

            Console.WriteLine("\nTime Elapsed: {0}", stopwatch.Elapsed);
            Console.WriteLine("Iterations Required: {0}", iterations);
            Console.WriteLine();
            //Return the user to the main menu
            SelectOperation();
        }
    }

    //Contains simple sorting algorithms and any essention sorting operations
    class GenericSortingAlgorithms
    {
        //Swaps two values within an array
        public static float[] Swap(float[] array, int x, int y)
        {
            float temp = array[x];
            array[x] = array[y];
            array[y] = temp;

            return array;
        }
    }

    //Contains the bubble sort algorithm
    class BubbleSort
    {
        public static int iterations = 0;

        //Sorts the stock data using the bubble sort algorithm
        public static float[] Sort(float[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    iterations++;
                    //If the left-most value is less than the right-most, swap the two in the array
                    if (array[j] > array[j + 1])
                    {
                        GenericSortingAlgorithms.Swap(array, j, j + 1);
                    }
                }
            }

            //Return the sorted array
            return array;
        }
    }

    //Contains the merge sort algorithm
    class MergeSortAlgorithm
    {
        public static int iterations = 0;

        public static float[] MergeSort(float[] array, int left, int right)
        {
            //If the length of the sub-array is not 0, mergesort the data between the two pointers
            if (left < right)
            {
                //Obtain the index of the midpoint of the sub-array 
                int mid = (int)Math.Floor((double)left + (right - left) / 2);

                //Perform a mergesort on the left and right halves of the sub-array
                MergeSort(array, left, mid);
                MergeSort(array, mid + 1, right);

                //Rebuild the arrays
                array = Merge(array, left, right, mid);
            }

            //Increase the valeu of the iterations counter and return the array
            iterations++;
            return array;
        }

        //Sorts data within sub-arrays, merigng them together
        public static float[] Merge(float[] array, int low, int high, int mid)
        {
            int left = low;
            int right = mid + 1;

            //Creates a temporary floating point array with length equal to the difference between the high and low indexes
            float[] tempArray = new float[(high - low) + 1];

            int indexOfTemp = 0;

            /*Sorts through pairs of elements in the array, and adds the smallest value in the pair to the temporary array,
             incrementing the associated pointer afterwards*/
            while (left <= mid && right <= high)
            {
                if (array[left] < array[right])
                {
                    tempArray[indexOfTemp] = array[left];
                    left++;
                }
                else
                {
                    tempArray[indexOfTemp] = array[right];
                    right++;
                }

                //The index data is being stored to in the temporary array is incremented evey time a value is added
                indexOfTemp++;
            }

            //Adds any remaining values that were already in the correct order, from the left half of the array, to the temporary array 
            while (left <= mid)
            {
                tempArray[indexOfTemp] = array[left];
                left++;
                indexOfTemp++;
            }

            //Adds any remaining values that were already in the correct order, from the right half of the array, to the temporary array
            while (right <= high)
            {
                tempArray[indexOfTemp] = array[right];
                right++;
                indexOfTemp++;
            }

            //Adds the data from the now-sorted temporary array to the main array
            for (int i = 0; i < tempArray.Length; i++) { array[low + i] = tempArray[i]; }

            return array;
        }
    }

    //Contains the quick sort algorithm
    class QuickSortAlgorithm
    {
        public static int iterations = 0;

        //This recursive function is responsible for sorting the dataset
        public static float[] QuickSort(float[] array, int low, int high)
        {
            if (low < high)
            {
                int partition = QuickSortPivot(array, low, high);
                QuickSort(array, low, partition - 1);
                QuickSort(array, partition + 1, high);
                iterations++;
            }

            //The array is returned at the end of the function
            return array;
        }

        public static int QuickSortPivot(float[] array, int low, int high)
        {
            //The pivot is always assumed to be the rightmost element of the array
            int startOfArray = low;
            float pivot = array[high];

            //Each element of a partition is compared to the adjacent value, and are swapped if they are out of order
            for (int i = low; i < high; i++)
            {
                if (array[i] < pivot)
                {
                    GenericSortingAlgorithms.Swap(array, i, startOfArray);
                    startOfArray++;
                }
            }

            //The highest value and the lowest value of the partition are swapped (the pivot is moved)
            GenericSortingAlgorithms.Swap(array, startOfArray, high);
            return startOfArray;
        }
    }

    //Contains the insertion sort algorithm
    class InsertionSortAlgorithm
    {
        public static int iterations = 0;

        public static float[] InsertionSort(float[] array, int high)
        {

            //The algorithm will only run whilst there are elements to sort (i.e. whilst the index of the highest unsorted value is greater than 0)
            if (high > 0)
            {

                //Perform an insertion sort using the value to the right of the current one
                InsertionSort(array, high - 1);

                float x = array[high];
                int i = high - 1;
                
                //While the value to the left of the current value is greater than the highest value, swap the values ????
                while (i >= 0 && array[i] > x)
                {
                    array[i + 1] = array[i];
                    i = i - 1;
                    iterations++;
                }

                array[i + 1] = x;
            }

            //Return the sorted array
            return array;
        }
    }

    //Contains the heap sort algorithm
    class HeapSortAlgorithm
    {
        private static int heapSize;
        public static int iterations = 0;

        public static float[] HeapSort(float[] array)
        {
            //Starts the algorithm by creating a heap from the unsorted dataset
            BuildHeap(array);

            //Swaps the greatest and second-greatest values in the array, and inserts them into the heap. The heap is then re-balanced
            for (int i = array.Length - 1; i >= 0; i--)
            {
                GenericSortingAlgorithms.Swap(array, 0, i);

                //The size of the heap is decremented before the heap is re-balanced
                heapSize--;
                Heapify(array, 0);
            }

            //The sorted array is returned
            return array;
        }

        private static void BuildHeap(float[] array)
        {
            heapSize = array.Length - 1;

            //A tree-based heap is created by heapifying the righthand size of the array 
            for (int i = (int) Math.Floor((double) heapSize / 2); i >= 0; i--)
            {
                Heapify(array, i);
            }
        }

        private static void Heapify(float[] array, int index)
        {
            //The index of the left value in the heap is assigned to be twice the value of the index of the array 
            int left = 2 * index;

            //The index of the right value in the heap is set to be one greater than the left value
            int right = 2 * index + 1;

            //The maximum possible index in the heap is the current index passed to the function from the array
            int max = index;

            iterations++;

            /*If the left index is less than the size of the heap, and the value at this index is less than the vaue at the 
            current index, the maximuim index becomes the left index */
            if (left <= heapSize && array[left] > array[index])
            {
                max = left;
            }

            /*If the right index is less than the size of the heap, and the value at this index is less than the vaue at the 
            current index, the maximuim index becomes the right index */
            if (right <= heapSize && array[right] > array[max])
            {
                max = right;
            }

            //If the maximum possible index is less than the current value of the index, swap the items at the two indexes in the heap, and rebalance the heap
            if (max != index)
            {
                GenericSortingAlgorithms.Swap(array, index, max);
                Heapify(array, max);
            }
        }
    }

    //Contains functions to find other instances of values in an array, and to find the closest value
    class GenericSearchingAlgorithms
    {
        public static int iterations = 0;

        //Responsible for finding other instances of a value in a sorted array
        public static void FindOtherInstances(float[] array, float toFind, int mid)
        {
            /*The first value of the array to be compared to the first instance the value being searched for is 
            * found, is set to be one less than the index the value was first found */
            int boundary = mid - 1;

            //While the value to the left of the location the value was first found is equal to that value, tell the user where this new instance of the value was found
            while (toFind == array[boundary])
            {
                Console.WriteLine("Value {0} found at index {1}. ", toFind, boundary);
                boundary--;
            }

            //The value of the boundary is now set to be one greater than the location the value was first found
            boundary = mid + 1;

            //While the value to the right of the location the value was first found is equal to that value, tell the user where this new instance of the value was found
            while (toFind == array[boundary])
            {
                Console.WriteLine("Value {0} found at index {1}. ", toFind, boundary);
                boundary++;
            }
        }


        //Obtains the closest value to the one being searched for, along with its index in the sorted array
        public static void GetClosestValue(float[] array, int mid, float toFind, int boundaryCode)
        {
            Console.WriteLine("\nValue {0} not found in dataset. ", toFind);

            /*The boundary code passed to the function is 0 if the value being searched for is less than the value at index 0 of the array.
             * If this is the case, the closest value must be the leftmost value of the array.
             * The boundary code is set to 1 if the value being searched for is greater than the value at the maximum index of the array. 
             * If this is the case, the largest value must be the rightmost value of the array */
            if (boundaryCode == 0)
            {
                Console.WriteLine("The closest value to {0} is {1} at index {2}", toFind, array[0], 0);
            } else if (boundaryCode == 1)
            {
                Console.WriteLine("The closest value to {0} is {1} at index {2}", toFind, array[array.Length - 1], array.Length - 1);
            }
            /*If the value is not found in the array, but is within range of the array, then the closest value must be the value at the 
            * last index found by the sorting algorithm before it determined the value was not present */
            else if (boundaryCode == -1)
            {
                Console.WriteLine("The closest value to {0} is {1} at index {2}", toFind, array[mid + 1], mid + 1);
            }
        }
    }

    class BinarySearchAlgorithm
    {
        public static int iterations = 0;

        //Contains the binary search algorithm
        public static void BinarySearch(float[] array, float toFind, int left, int right)
        {
            int boundaryCode = 0;

            //If the value is not out of range of the array, but it is not found in the array, find the closest value using boundary code -1
            if (left > right)
            {
                boundaryCode = -1;
                GenericSearchingAlgorithms.GetClosestValue(array, left - 1, toFind, boundaryCode);
                return;
            }

            //If the value being searched for is less than the leftmost value in the array, find the closest value using boundary code 0
            if (toFind < left)
            {
                boundaryCode = 0;
                GenericSearchingAlgorithms.GetClosestValue(array, left, toFind, boundaryCode);
                return;

            }
            //If the value being searched for is greater than the rightmost value in the array, find the closest value using boundary code 1
            else if (toFind > right)
            {
                boundaryCode = 1;
                GenericSearchingAlgorithms.GetClosestValue(array, right, toFind, boundaryCode);
                return;
            }

            //The midpoint between the current left and right indexes are found (initially set to 0, and the length of the array - 1)
            int mid = (int) Math.Floor((double)right + left / 2);

            //If the value is found at the midpoint, tell the user and find any other instances
            if (toFind == array[mid])
            {
                Console.WriteLine("\nValue {0} found at index {1}. ", toFind, mid);
                GenericSearchingAlgorithms.FindOtherInstances(array, toFind, mid);
            }
            //If the value being searched for is less than the midpoint, 'discard' the right side of the array and perform a binary search on the left side
            else if (toFind < array[mid]) { 
                BinarySearch(array, toFind, left, mid - 1); 
            }
            //If the value being searched for is greater than the midpoint, 'discard' the left side of the array and perform a binary search on the right side
            else { 
                BinarySearch(array, toFind, mid + 1, right); 
            }

            iterations++;
        }
    }

    class InterpolationSearchAlgorithm
    {
        public static int iterations = 0;

        //Contains the interpolation search algorithm
        public static void InterpolationSearch(float[] array, float toFind, int low, int high)
        {
            int boundaryCode = 0;

            int mid = 0;
            while (low <= high && toFind >= array[low] && toFind <= array[high])
            {
                //The midpoint of the array is found by estimating where the value being searched for should lie within the array, assuming equal distribution
                mid = (int)Math.Round(low + (((double)(high - low) / (array[high] - array[low])) * (toFind - array[low])));

                //If the value at the midpoint is equal to the value being searched for, tell the user and find other instances of the value
                if (array[mid] == toFind)
                {
                    Console.WriteLine("Value {0} found at index {1}", toFind, mid);
                    GenericSearchingAlgorithms.FindOtherInstances(array, toFind, mid);
                }

                //If the value at the midpoint is less than the value being searched for, change the low index so that the left side of the array is effectively discarded
                if (array[mid] < toFind) { 
                    low = mid + 1; 
                }
                //Otherwise, discard the right side of the array
                else { 
                    high = mid - 1; 
                }

                iterations++;
            }

            //If the value is not found in the array at all, tell the user and get the closest value in the array
            if (array[mid] != toFind)
            {
                Console.WriteLine("Value {0} not found in dataset. ", toFind);
                boundaryCode = -1;
                GenericSearchingAlgorithms.GetClosestValue(array, mid, toFind, boundaryCode);
            } else if (toFind > array[array.Length - 1])
            {
                boundaryCode = 1;
                GenericSearchingAlgorithms.GetClosestValue(array, mid, toFind, boundaryCode);
            } else if (toFind < array[0])
            {
                boundaryCode = 0;
                GenericSearchingAlgorithms.GetClosestValue(array, mid, toFind, boundaryCode);
            }
        }
    }

    class BSTNode
    {
        public int iterations = 0;

        //Each node of the binary search tree has an index and a value
        public float value;
        public int index;

        //Each node has a left and right node attached to it, which is null unless specified
        public BSTNode left;
        public BSTNode right;

        //Seaches for a value in the binary search tree recursively
        public int Search(float toFind)
        {
            iterations++;
            //If the value being searched for is equal to the value of the node, return the index of the node it was found in
            if (toFind == value) {
                FindOtherInstances(toFind);
                return index;
            }
            /*If the value being searched for is less than the value of the node, attempt to search the node attached to the 
            * left of it, else return an error code as the value cannot be present */
            else if (toFind < value)
            {
                if (left == null) { 
                    return -1; 
                }
                else { 
                    return left.Search(toFind); 
                }
            }
            //Attempt to search the node attached to the right of the current node, else return an error code as the value cannot be present
            else
            {
                if (right == null) { 
                    return -1; 
                }
                else { 
                    return right.Search(toFind); 
                }
            }
        }

        //Gets the closest value to the one requested within a binary search tree
        public void GetClosestValue(float toFind, float smallestDifference)
        {

            /*If the current node has no left or right branches, the end of the tree has been reached 
            and thus the closest value found is the value of the current node */
            if (right == null && left == null)
            {
                Console.WriteLine("End of tree reached. The closest value to {0} found was {1}. ", toFind, value);
            }

            /*If the current node has a node to the right of it, and the difference between the right node's value and the value 
            being searched for is less than the smallest difference found so far, a value closer to the requested one has been found */
            if (right != null && Math.Abs(toFind - right.value) <= smallestDifference)
            {
                smallestDifference = Math.Abs(toFind - right.value);

                //Attempt to find an even closer value by examining the right node
                right.GetClosestValue(toFind, smallestDifference);
            }
            /*If the current node has a node to the left of it, and the difference between the left node's value and the value 
            being searched for is less than the smallest difference found so far, a value closer to the requested one has been found */
            else if (left != null && Math.Abs(toFind - left.value) < smallestDifference)
            {
                smallestDifference = Math.Abs(toFind - left.value);

                //Attempt to find an even closer value by examining the left node
                left.GetClosestValue(toFind, smallestDifference);
            }
            else
            {
                //If no closer value was found to the left or right, inform the user that the closest value is the value of the current node
                Console.WriteLine("The closest value to {0} found was {1}. ", toFind, value);
                return;
            }
        }

        //Returns the index of all other nodes with the same value as the one being searched for
        public void FindOtherInstances(float toFind)
        {

            //If there exists a node to the left of the current node, and it's value is equal to the one being searched for, inform the user of its index
            if (left != null && left.value == toFind)
            {
                Console.WriteLine("Value {0} also found at node {1}", toFind, index);

                //Examine this left node to see if other instances of the value exists further down the tree
                left.FindOtherInstances(toFind);
            }

            //If there ists a node to the right of the current node, and it's value is equal to the one being searched for, inform the user of its index
            else if (right != null && right.value == toFind)
            {
                Console.WriteLine("Value {0} also found at node {1}", toFind, index);

                //Examine this right node to see if other instances of the calue exosts further down the tree
                right.FindOtherInstances(toFind);
            }
        }
    }

    class BST
    {
        public static int index = 0;
        public static int iterations = 0;

        public static void InitialiseTree(float[] array,float toFind)
        {
            //A root node is created
            BSTNode root = new BSTNode();
            root.index = 0;
            root.value = array[0];

            //Each value of the array is inserted into the BST
            for (int i = 1; i < array.Length; i++)
            {
                InsertData(root, array[i]);
            }

            //The user-specified value is then searched for within the BST
            SearchForValue(root, toFind);
        }

        public static BSTNode InsertData(BSTNode node, float data)
        {

            //If the node passed to the function is null, create a new node and assign its value to the data passed to the function
            if (node == null)
            {
                node = new BSTNode();
                node.value = data;
                node.index = index;
                index++;
            }
            //If the data is less than the value of the current node, attempt to insert it into the node attached to the left of the current node
            else if (data < node.value) { 
                node.left = InsertData(node.left, data); 
            }
            //Otherwise, attempt to insert the data into the node attached to the right of the current node
            else { 
                node.right = InsertData(node.right, data); 
            }
            
            return node;
        }

        //Initialises the search for the value within the BST
        public static void SearchForValue(BSTNode node, float toFind)
        {
            int found = node.Search(toFind);
            iterations = node.iterations;

            //If the value was not found in the tree, find the next closest value to it that is in the tree
            if (found == -1)
            {
                Console.WriteLine("The value {0} was not found in the tree. ", toFind);
                node.GetClosestValue(toFind, Math.Abs(toFind - node.value));
            }
        }
    }
}