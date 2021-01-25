using System;
using System.Collections.Generic;
using System.Linq;

namespace HeapSort
{
	public class Program
	{
		// Helper for fancy output
		static int col = 0;

		static void Main(string[] args)
		{
		start:
			Console.Clear();
			Console.WriteLine("### HeapSort Algorithm ###\n");
			Console.WriteLine("Select an operation:\n\t" +
				"1. HeapSort (A = {5, 13, 2, 25, 7, 17, 20, 8, 4})\n\t" +
				"2. Max-Heap Insert (10, A = {15, 13, 9, 5, 12, 8, 7, 4, 0, 6, 2, 1})");

			int runMode = 0;
			ConsoleKeyInfo key;

			while (runMode == 0)
			{
				key = Console.ReadKey(true);

				if (key.KeyChar == '1')
					runMode = -1;
				else if (key.KeyChar == '2')
					runMode = 1;
				else
				{
					Console.SetCursorPosition(0, 5);
					Console.Write("Please press 1 or 2");
				}
			}

			Console.Clear();

			switch (runMode)
			{
				// Heap Sort (Min Heap)
				case -1:
					Console.WriteLine("### HeapSort ###\n");
					int[] A = { 5, 13, 2, 25, 7, 17, 20, 8, 4 };

					#region Output
					Console.Write("Initial State: ");

					foreach (int v in A)
						Console.Write(v + " ");

					Console.WriteLine("\n"); // Write two newlines
					#endregion

					Sort(A);

					#region Output
					Console.ResetColor();
					Console.Write("\nFinal State: ");

					foreach (int v in A)
						Console.Write(v + " ");

					Console.WriteLine();
					Console.ReadKey(true);
					#endregion

					break;
				// Max Heap Insertion
				case 1:
					Console.WriteLine("### Max-Heap Insertion ###");
					List<int> B = new List<int> { 15, 13, 9, 5, 12, 8, 7, 4, 0, 6, 2, 1 };

					#region Output
					Console.Write("Initial State: ");

					foreach (int v in B)
						Console.Write(v + " ");

					Console.WriteLine("\n");
					Console.WriteLine("Inserting 10...\n");
					#endregion

					InsertNode(B, 10);

					Console.ResetColor();

					#region Output
					Console.Write("\nFinal State: ");

					foreach (int v in B)
						Console.Write(v + " ");

					Console.WriteLine();
					Console.ReadKey(true);
					#endregion

					break;
				default:
					goto start;
			}
		}

		// Helper function for inserting
		public static void InsertNode(List<int> arr, int k)
		{
			foreach (int v in arr)
				Console.Write(v + " ");

			Console.Write("-> ");
			// Increase heap size
			int n = arr.Count + 1;

			// Add value to end of list
			arr.Add(k);

			#region Output
			for (int i = 0; i < n; i++)
			{
				if (i == n - 1)
					Console.ForegroundColor = ConsoleColor.Green;

				Console.Write(arr[i] + " ");
			}
			#endregion

			Console.ResetColor();
			Console.WriteLine();

			// Begin bottom up heapify
			HeapifyBottomUp(arr, n, n - 1);
		}

		public static void HeapifyBottomUp(List<int> arr, int n, int i)
		{
			List<int> copy = new List<int>(arr); //For tracking

			// Get parent node
			int parent = (i - 1) / 2;

			// If the current node > parent, swap and heapify parent
			if (arr[i] > arr[parent])
			{
				int temp = arr[parent];
				arr[parent] = arr[i];
				arr[i] = temp;

				HeapifyBottomUp(arr, n, parent);
			}

			#region Output
			if (!arr.SequenceEqual(copy))
			{
				col = 0;
				foreach (int v in arr)
				{
					if (col == i)
						Console.ForegroundColor = ConsoleColor.Red;
					else if (col == parent)
						Console.ForegroundColor = ConsoleColor.Green;
					else
						Console.ResetColor();

					Console.Write(v + " ");
					col++;
				}

				Console.Write("-> ");

				col = 0;
				foreach (int v in copy)
				{
					if (col == i)
						Console.ForegroundColor = ConsoleColor.Green;
					else if (col == parent)
						Console.ForegroundColor = ConsoleColor.Red;
					else
						Console.ResetColor();

					Console.Write(v + " ");
					col++;
				}

				Console.WriteLine();
			}
			#endregion
		}

		// Helper function for BottomUp Heapify
		public static void Sort(int[] arr)
		{
			int n = arr.Length;

			// Build heap
			for (int i = n / 2 - 1; i >= 0; i--)
				HeapifyTopDown(arr, n, i);

			for (int i = n - 1; i > 0; i--)
			{
				// Swap i with root
				int temp = arr[0];
				arr[0] = arr[i];
				arr[i] = temp;

				// Build heap on modified heap
				HeapifyTopDown(arr, i, 0);
			}
		}

		// Heapify an n-sized array at node i
		public static void HeapifyTopDown(int[] arr, int n, int i)
		{
			int[] copy = new int[arr.Length]; // For tracking
			Array.Copy(arr, copy, arr.Length);

			int max = i;
			int leftChild = 2 * i + 1;
			int rightChild = 2 * i + 2;

			// If left is larger than root
			if (leftChild < n && arr[leftChild] > arr[max])
				max = leftChild;

			// If right is larger than largest so far
			if (rightChild < n && arr[rightChild] > arr[max])
				max = rightChild;

			// If largest is not root
			if (max != i)
			{
				int temp = arr[i];
				arr[i] = arr[max];
				arr[max] = temp;

				// Build heap on modified heap
				HeapifyTopDown(arr, n, max);
			}

			// If we made a change to the array, output it here.
			#region Output
			if (!arr.SequenceEqual(copy))
			{
				col = 0;
				foreach (int v in arr)
				{
					if (col == i)
						Console.ForegroundColor = ConsoleColor.Red;
					else if (col == max)
						Console.ForegroundColor = ConsoleColor.Green;
					else
						Console.ResetColor();

					Console.Write(v + " ");
					col++;
				}

				Console.Write("-> ");

				col = 0;
				foreach (int v in copy)
				{
					if (col == i)
						Console.ForegroundColor = ConsoleColor.Green;
					else if (col == max)
						Console.ForegroundColor = ConsoleColor.Red;
					else
						Console.ResetColor();

					Console.Write(v + " ");
					col++;
				}

				Console.WriteLine();
			}
			#endregion
		}
	}
}
