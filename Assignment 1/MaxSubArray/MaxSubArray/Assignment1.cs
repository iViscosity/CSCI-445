using System;
using System.Diagnostics;
using System.Linq;

namespace MaxSubArray
{
	public class Assignment1
	{
		public static void Main(string[] args)
		{
			// Just to keep track.
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			int[] arr = new int[50000];
			Random r = new Random(Environment.TickCount);

			for (int i = 0; i < arr.Length; i++)
				arr[i] = r.Next(-50000, 50000);

			(int start, int end, int[] subarray) = MaxSubArray(arr);

			stopwatch.Stop();

			// Output the subarray first (as it can be VERY long)
			for (int i = 0; i < subarray.Length; i++)
				Console.Write($"{subarray[i]} ");

			// Output where it was found
			Console.Write($"\n\nFound max subarray from {start} to {end} ({end - start} elements) (sum: {subarray.Sum()})");

			// Diagnostics
			Console.WriteLine($"\n\nTook {stopwatch.ElapsedMilliseconds}ms");

			Console.ReadKey(true); // To wait for key to close
		}

		// Finds the max sub array using Kadane's Algorithm.
		public static (int, int, int[]) MaxSubArray(int[] arr)
		{
			// These are to keep track of the current and global maximum sum. If the current maximum supercedes the global maximum, it starts over
			int maxSum = 0;
			int curSum = 0;

			// These are to keep track of the *positions* of the current and global maximums. 
			int maxStart = 0;
			int curStart = 0;
			int maxEnd = 0;
			int curEnd;

			// For convenience
			int size = arr.Length;

			for (int i = 0; i < size; i++)
			{
				// Add the current value to the 
				curSum += arr[i];
				curEnd = i;

				// Unless every value in the array is negative, there will always be a positive maximum.
				if (curSum < 0)
				{
					curSum = 0;
					curStart = curEnd + 1;
				}

				// If the current max is greater than the global max, replace it and the starting points.
				if (maxSum < curSum)
				{
					maxSum = curSum;
					maxStart = curStart;
					maxEnd = curEnd;
				}
			}

			// If every value is negative, the maximum is just the least negative number
			if (maxEnd == 0)
			{
				int max = arr.Max();
				int idx = arr.ToList().IndexOf(max);
				return (idx, idx, new int[] { arr.Max() });
			}

			// Return a tuple containing the the start and end indicies and the subarray
			return (maxStart, maxEnd, new ArraySegment<int>(arr, maxStart, maxEnd - maxStart + 1).ToArray());
		}
	}
}
