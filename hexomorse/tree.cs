namespace hexomorse
{
    public enum NodeDirection
    {
        LEFT,
        RIGHT
    }

    public class TreeNode
    {
        public TreeNode(string value, TreeNode left = null, TreeNode right = null)
        {
            this.Value = value;
            this.Left = left;
            this.Right = right;
        }
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }
        public string Value { get; set; }
    }
}