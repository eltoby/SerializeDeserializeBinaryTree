using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializeDeserializeBinaryTree
{
    public class Codec
    {

        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            if (root == null)
                return string.Empty;

            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            var result = new StringBuilder();

            while (queue.TryDequeue(out var node))
            {
                if (node == null)
                    result.Append("null");
                else
                { 
                    result.Append(node.val);
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                }

                result.Append(",");
            }

            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            var nodes = data.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var inputQueue = new Queue<string>(nodes);

            var parentsQueue = new Queue<TreeNode>();

            if (inputQueue.Count == 0)
                return null;

            var root = new TreeNode(int.Parse(inputQueue.Dequeue()));
            parentsQueue.Enqueue(root);

            while(inputQueue.TryDequeue(out var node))
            {
                var parent = parentsQueue.Dequeue();
                if (node != "null")
                { 
                    parent.left = new TreeNode(int.Parse(node));
                    parentsQueue.Enqueue(parent.left);
                }
                node = inputQueue.Dequeue();

                if (node != "null")
                {
                    parent.right = new TreeNode(int.Parse(node));
                    parentsQueue.Enqueue(parent.right);
                }
            }

            return root;
        }
    }
}
