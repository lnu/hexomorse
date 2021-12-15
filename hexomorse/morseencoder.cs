namespace hexomorse
{
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using System.Text;

    public class MorseEncoder : IEncoder
    {
        private const string NODE_DELIMITER = "_";
        private readonly TreeNode _rootNode;
        private readonly Dictionary<string, TreeNode> nodes = new System.Collections.Generic.Dictionary<string, TreeNode>();

        public MorseEncoder()
        {
            //load morse file
            string[] morseLines = File.ReadAllLines("morse.txt");
            foreach (var l in morseLines)
            {
                var splitted = l.Split("->").Select(p => p.Trim()).ToArray();
                // check if it exists firts
                var tn = nodes.GetValueOrDefault(splitted[0], new TreeNode(splitted[0]));
                if (!nodes.TryAdd(splitted[0], tn)) nodes[splitted[0]] = tn;
                if (splitted[0] == NODE_DELIMITER)
                {
                    // root node
                    _rootNode = tn;
                }

                //build left and right
                var leafNodes = splitted[1].Split(",").Select(p => p.Trim()).ToArray();
                tn.Left = BuildNode(leafNodes[0]);
                tn.Right = BuildNode(leafNodes[1]);
            }
            if (_rootNode == null)
            {
                throw new Exception("Error while parsing input file");
            }
        }

        private TreeNode? BuildNode(string leaf)
        {
            if (leaf == NODE_DELIMITER)
            {
                return null;
            }
            var tn = nodes.GetValueOrDefault(leaf, new TreeNode(leaf));
            if (!nodes.TryAdd(leaf, tn)) nodes[leaf] = tn;
            return tn;
        }

        private Queue<NodeDirection>? recursiveInorder(char c, Queue<NodeDirection> path, TreeNode node)
        {
            if (node.Value.Equals(c.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return path;
            }

            if (node.Left != null)
            {
                Queue<NodeDirection> tmpQueue = new Queue<NodeDirection>(path);
                tmpQueue.Enqueue(NodeDirection.LEFT);
                var res = recursiveInorder(c, tmpQueue, node.Left);
                if (res != null)
                {
                    return res;
                }
            }
            if (node.Right != null)
            {
                Queue<NodeDirection> tmpQueue = new Queue<NodeDirection>(path);
                tmpQueue.Enqueue(NodeDirection.RIGHT);
                var res = recursiveInorder(c, tmpQueue, node.Right);
                if (res != null)
                {
                    return res;
                }
            }
            return null;
        }

        private string FindNode(TreeNode node, Queue<NodeDirection> directions)
        {
            if (directions.Count == 0)
            {
                return string.Empty;
            }
            TreeNode n = node;
            while (directions.Count > 0)
            {
                var d = directions.Dequeue();
                switch (d)
                {
                    case NodeDirection.LEFT:
                        n = n.Left;
                        break;
                    case NodeDirection.RIGHT:
                        n = n.Right;
                        break;
                }
            }
            return n.Value;
        }

        public string Encode(string input)
        {
            //validate input
            if (input.Any(p => !(char.IsLetterOrDigit(p) || char.IsWhiteSpace(p))))
            {
                throw new Exception("only ascii character supported");
            }

            var result = new StringBuilder();
            foreach (var c in input)
            {
                if (c == ' ')
                {
                    result.Append("   ");
                    continue;
                }
                Queue<NodeDirection> directions = new Queue<NodeDirection>();
                var res = recursiveInorder(c, directions, _rootNode);
                while (res.Count > 0)//
                {
                    var nd = res.Dequeue();
                    switch (nd)
                    {
                        case NodeDirection.LEFT:
                            result.Append(".");
                            break;
                        case NodeDirection.RIGHT:
                            result.Append("-");
                            break;
                    }
                }
                result.Append(" ");
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        public string Decode(string input)
        {
            var result = new StringBuilder();
            var directions = new Queue<NodeDirection>();
            foreach (var g in input.Replace("    ", " # ").Split(' '))
            {
                foreach (var c in g)
                {
                    switch (c)
                    {
                        case '#':
                            result.Append(" ");
                            break;
                        case '.':
                            directions.Enqueue(NodeDirection.LEFT);
                            break;
                        case '-':
                            directions.Enqueue(NodeDirection.RIGHT);
                            break;
                        default:
                            throw new Exception("Wrong morse charcacter found, only . or - allowed");
                    }
                }
                result.Append(FindNode(_rootNode, directions));
            }
            return result.ToString();
        }
    }
}
