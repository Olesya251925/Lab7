using System;
using System.Collections;
using System.Collections.Generic;

namespace Seven
{
    public class BinaryTree<T> : IEnumerable<T>
    {
        private Node root;

        public void Add(T value)
        {
            if (root == null)
            {
                root = new Node(value);
            }
            else
            {
                root.Add(value);
            }
        }

        // Реализация интерфейса IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return new InOrderEnumerator(root); // Создаем и возвращаем итератор для прямого обхода дерева
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Node
        {
            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(T value)
            {
                Value = value;
            }

            public void Add(T value)
            {
                if (Comparer<T>.Default.Compare(value, Value) < 0)
                {
                    if (Left == null)
                    {
                        Left = new Node(value);
                    }
                    else
                    {
                        Left.Add(value);
                    }
                }
                else
                {
                    if (Right == null)
                    {
                        Right = new Node(value);
                    }
                    else
                    {
                        Right.Add(value);
                    }
                }
            }
        }

        private class InOrderEnumerator : IEnumerator<T>
        {
            private Node root;
            private Stack<Node> stack;
            private Node current;

            public InOrderEnumerator(Node root)
            {
                this.root = root;
                stack = new Stack<Node>();
                current = null;
                Reset();
            }

            public T Current
            {
                get { return current.Value; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (stack.Count == 0)
                {
                    current = null;
                    return false;
                }

                current = stack.Pop(); // Извлекаем узел из стека
                Node node = current.Right; // Получаем правого потомка текущего узла
                while (node != null)
                {
                    stack.Push(node); // Добавляем в стек правого потомка и все его левые потомки
                    node = node.Left;
                }

                return true;
            }

            public void Reset()
            {
                stack.Clear(); // Очищаем стек
                Node node = root; // Получаем корневой узел
                while (node != null)
                {
                    stack.Push(node); // Добавляем в стек текущий узел и все его левые потомки
                    node = node.Left;
                }
            }

            public void Dispose()
            {
            }
        }

        // Операторы ++ и --
        public static BinaryTree<T> operator ++(BinaryTree<T> tree)
        {
            tree.MoveNext(); // Используем метод MoveNext() для перемещения к следующему узлу в прямом обходе
            return tree;
        }

        public static BinaryTree<T> operator --(BinaryTree<T> tree)
        {
            tree.MovePrevious(); // Используем метод MovePrevious() для перемещения к предыдущему узлу в прямом обходе
            return tree;
        }

        // Методы Next(), Previous() и Current()
        public bool MoveNext()
        {
            var enumerator = GetEnumerator(); // Получаем итератор для прямого обхода дерева
            enumerator.MoveNext(); // Перемещаемся к следующему элементу
            return true; // Возвращаем true для указания успешного перемещения
        }

        public bool MovePrevious()
        {
            var enumerator = GetEnumerator(); // Получаем итератор для прямого обхода дерева
            if (enumerator.MoveNext()) // Перемещаемся к следующему элементу и проверяем успешность перемещения
            {
                enumerator.MoveNext(); // Перемещаемся к следующему элементу
                return true; // Возвращаем true для указания успешного перемещения
            }
            return false; // Возвращаем false, если перемещение не удалось или итератор достиг конца
        }

        public T Current()
        {
            var enumerator = GetEnumerator(); // Получаем итератор для прямого обхода дерева
            enumerator.MoveNext(); // Перемещаемся к следующему элементу
            return enumerator.Current; // Возвращаем текущий элемент
        }

        // Внешний итератор для центрального обхода
        public IEnumerable<T> CentralTraversal()
        {
            var sortedList = new List<T>();
            CentralTraversal(root, sortedList);
            return sortedList;
        }

        private void CentralTraversal(Node node, List<T> sortedList)
        {
            if (node != null)
            {
                CentralTraversal(node.Left, sortedList);
                sortedList.Add(node.Value);
                CentralTraversal(node.Right, sortedList);
            }
        }
    }
}


