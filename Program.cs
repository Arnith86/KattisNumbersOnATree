using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KattisNumbersOnATree
{
    internal class Program
    {

        /// <summary>
        /// 
        /// </summary>
        static void Main(string[] args)
        {
			Console.SetIn(new StreamReader(/*"numbertree-02.in"*/"numbers_on_tree_input.txt"));

            int creationID = 0;
            string input = null;
            
            while ((input = Console.ReadLine()) != null)
            {
				string[] extractedValues = input.Split(' ');
                
                int height = 0;
                string path = string.Empty;

				if (extractedValues.Length > 1)
                {
					height = int.Parse(extractedValues[0]);
					path = extractedValues[1];
				}
                
               Console.WriteLine(GetNodeValue(height, path));
			}
        
            
            


            int GetNodeValue(int height, string path)
            {
                /// <summary>
                /// We know that the tree is a perfect binary tree, which means that each node has 0 or 2 child nodes,
                /// and each "row" is always full of nodes. Resulting in each new row having double the number of nodes to 
                /// To calculate the total number of nodes the formula 2^(height+1) -1 is used. 
                /// 
                /// We can therefore use bitwise shift to find the total number of nodes.
                /// </summary>
               
                // The number of times the 1 shifts to the left
                int shiftValue = height + 1;

                /// <summary>
                /// 1 = 0001    : simply the value 1
                /// <<          : the direction that the shift will move.
                /// shiftValue  : the number of shifts. 
                /// </summary>
                int maxValue = (1 << shiftValue) - 1;

				/// <summary>
                /// Another way of finding the number of nodes is with the bellow for loop.
                /// 
				///  for (int i = 0; i < height + 1; i++)
				///    maxValue += (int)Math.Pow(2, i); 
				/// </summary>

				// Set root node value
				int prevNodeValue = 1;
                
                // Calculate the values along the path to the desired node. 
                /// <summary>
                /// This gives the result in a numerical order 1 -> 2 -> 3 -> 4, and so on. 
                /// The resulting value has to be converted to the reversed value.
                /// </summary> 
				for (int i = 0; i < path.Length; i++)
                {
                    int nextNodeNumber = path[i] == 'L' ? (prevNodeValue * 2) : (prevNodeValue * 2 + 1);
                    prevNodeValue = nextNodeNumber;
                }

                // Reverses the value.
                int result = maxValue - (prevNodeValue -1);

                return result;
            }
        }
    }
}
