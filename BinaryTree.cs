using System;
using System.Collections;
using System.Collections.Generic;

namespace Seven7
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

        public IEnumerator<T> GetEnumerator()
        {
            return new BinaryTreeEnumerator(this);
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

        private class BinaryTreeEnumerator : IEnumerator<T>
        {
            private BinaryTree<T> tree;
            private Stack<Node> stack;
            private Node current;

            public BinaryTreeEnumerator(BinaryTree<T> tree)
            {
                this.tree = tree;
                stack = new Stack<Node>();
                current = null;
                Reset();
            }

            public T Current
            {
                get { return current.Value; } // Возвращает текущее значение
            }

            object IEnumerator.Current
            {
                get { return Current; } // Возвращает текущий объект
            }

            public bool MoveNext()
            {
                if (stack.Count == 0)
                {
                    current = null;
                    return false;
                }

                current = stack.Pop(); // Получаем текущий узел
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
                Node node = tree.root; // Получаем корневой узел
                while (node != null)
                {
                    stack.Push(node); // Добавляем в стек текущий узел и все его левые потомки
                    node = node.Left;
                }
            }

            public void Dispose()
            {
                // В данном случае нет неуправляемых ресурсов, требующих очистки.
                // Поэтому метод Dispose() остается пустым.
            }

        }

        // Операторы ++ и --
        public static BinaryTree<T> operator ++(BinaryTree<T> tree)
        {
            tree.MoveNext();
            return tree;
        }

        public static BinaryTree<T> operator --(BinaryTree<T> tree)
        {
            tree.MovePrevious();
            return tree;
        }

        // Методы Next(), Previous() и Current()
        public bool MoveNext()
        {
            var enumerator = GetEnumerator(); // Создание экземпляра итератора
            enumerator.MoveNext(); // Перемещение к следующему элементу
            return true; // Возвращаем true для указания успешного перемещения
        }


        public bool MovePrevious()
        {
            var enumerator = GetEnumerator(); // Создание экземпляра итератора
            if (enumerator.MoveNext()) // Перемещение к следующему элементу и проверка успешности перемещения
            {
                enumerator.MoveNext(); // Перемещение к следующему элементу
                return true; // Возвращаем true для указания успешного перемещения
            }
            return false; // Возвращаем false, если перемещение не удалось или итератор достиг конца
        }


        public T Current()
        {
            var enumerator = GetEnumerator(); // Создание экземпляра итератора
            enumerator.MoveNext(); // Перемещение к следующему элементу
            return enumerator.Current; // Возвращение текущего элемента
        }

    }
}
