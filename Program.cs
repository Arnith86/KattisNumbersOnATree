namespace KattisNumbersOnATree
{
    internal class Program
    {
        static void Main(string[] args)
        {
			Console.SetIn(new StreamReader("numbertree-01.in"));

            int treeValues = 1;
            string input = null;
            
            while ((input = Console.ReadLine()) != null)
            {
                string[] extractedValues = input.Split(' ');

                int height = int.Parse(extractedValues[0]);
                string path = extractedValues[1];

                Console.WriteLine(height);
				Console.WriteLine(path);

                int currentHeight = 0;

                Node startNode = new Node();

                BuildTree(startNode, 0, height+1);
			}
        
            
            void BuildTree(Node currNode, int height, int maxHeight)
            {
                int nextHeight = height+1;
               
				if ( nextHeight < maxHeight)
                {
                    currNode.RightNode = new Node();
                    currNode.LeftNode = new Node();
					
                    BuildTree(currNode.RightNode, nextHeight, maxHeight);
                    BuildTree(currNode.LeftNode, nextHeight, maxHeight);
				}
            }
            
        }

        // Building blocks for the tree                
        public class Node
        {
            public Node RightNode { get; set; } = null;
            public Node LeftNode { get; set; } = null;
            public int Value { get; set; } = 0;
		} 
        
    }
}
