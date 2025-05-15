using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KattisNumbersOnATree.Program;

namespace KattisNumbersOnATree
{
	/// <summary>
	/// This is a working but naive solution to the kattis problem Numbers On a Tree. Which builds the trees 
	/// and uses reverse BFS traversal to set the values to the nodes. It does however not scale to larger versions 
	/// of the tree, making it naive.
	/// </summary>
	internal class NaviSolution
	{
		int creationID = 0;
		string input = null;

		public NaviSolution() 
		{
			Console.SetIn(new StreamReader(/*"numbertree-03.in"*/"numbers_on_tree_input.txt"));

			

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

				int currentHeight = 0;

				Node startNode = new Node(0);

				BuildTree(startNode, 0, height + 1);
				SetNodeValues(startNode);
				Console.WriteLine(GetNodeValue(path, startNode));
			}
		}
		// Builds the tree up to the desired height
		// nodesCreated is a global variable.
		void BuildTree(Node currNode, int height, int maxHeight)
		{
			int nextHeight = height + 1;

			if (nextHeight < maxHeight)
			{
				currNode.RightNode = new Node(++creationID);
				currNode.LeftNode = new Node(++creationID);

				BuildTree(currNode.RightNode, nextHeight, maxHeight);
				BuildTree(currNode.LeftNode, nextHeight, maxHeight);
			}
		}

		// Sets values for the tree nodes, using bfs traversal.
		void SetNodeValues(Node startNode)
		{
			Node currNode = startNode;

			// Keeps track of which order to visit the nodes 
			Queue<Node> nextToVisit = new Queue<Node>();

			// Keeps track of which order to set the values to the nodes. 
			// First visited, last to receive its value.
			Stack<Node> valueSetOrde = new Stack<Node>();

			nextToVisit.Enqueue(startNode);

			while (nextToVisit.Count > 0)
			{
				currNode = nextToVisit.Dequeue();
				valueSetOrde.Push(currNode);

				// If next nodes are null, then this node is a leaf,
				if (currNode.RightNode == null || currNode.LeftNode == null) continue;

				// Adds the new branches to the bfs queue. The order in which they are visited
				// is reversed, to allow the value setting to be conducted right to left,
				// instead of left to right. 
				nextToVisit.Enqueue(currNode.LeftNode);
				nextToVisit.Enqueue(currNode.RightNode);
			}

			int treeValues = 1;

			while (valueSetOrde.Count > 0)
			{
				valueSetOrde.Pop().Value = treeValues++;
			}
		}

		int GetNodeValue(string path, Node startNode)
		{
			Node currNode = startNode;

			for (int i = 0; i < path.Length; i++)
			{
				if (path[i] == 'R') currNode = currNode.RightNode;
				else currNode = currNode.LeftNode;
			}

			return currNode.Value;
		}

	}
	
	// Building blocks for the tree                
	public class Node
	{
		public Node RightNode { get; set; } = null;
		public Node LeftNode { get; set; } = null;
		public int Value { get; set; } = 0;
		public int CreationID { get; set; }

		public Node(int creationID)
		{
			CreationID = creationID;
		}
	}
}
