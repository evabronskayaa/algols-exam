using System.Collections.Generic;

namespace SortingAlgorithms
{
    public class TreeNode
    {
        private readonly int _data;
        
        private TreeNode _left; //left branch
        
        private TreeNode _right; //right branch
        
        public TreeNode(int data) 
        {
            _data = data;
        }
        
        public void Insert(TreeNode node) 
        {
            if (node._data < _data) 
            {
                if (_left == null) _left = node;
                else _left.Insert(node);
            }
            else 
            {
                if (_right == null) _right = node;
                else _right.Insert(node);
            }
        }

        // transform tree to array
        public int[] Transform(List<int> elements = null)
        { 
            if (elements == null) elements = new List<int>();
            if (_left != null) _left.Transform(elements);

            elements.Add(_data);
            if (_right != null) _right.Transform(elements);

            return elements.ToArray();
        }
    }

    public static class TreeSorter
    {
        public static int[] TreeSort(this int[] array)
        {
            var treeNode = new TreeNode(array[0]);
            for (var i = 1; i < array.Length; i++) 
                treeNode.Insert(new TreeNode(array[i]));
            

            return treeNode.Transform();
        }
    }
}