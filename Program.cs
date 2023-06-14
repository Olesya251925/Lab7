using System;

namespace Seven7
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            tree.Add(5);
            tree.Add(3);
            tree.Add(8);
            tree.Add(1);
            tree.Add(4);

Console.WriteLine("Прямой обход дерева:");
List<int> treeValues = new List<int>();
foreach (int value in tree)
{
    treeValues.Add(value);
}
string result = string.Join(", ", treeValues);
Console.WriteLine(result);

// Обратный обход дерева
Console.WriteLine("Обратный обход дерева:");
treeValues.Clear();
foreach (int value in tree.Reverse())
{
    treeValues.Add(value);
}
result = string.Join(", ", treeValues);
Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}

