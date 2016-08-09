using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Meowtrix.WPF.Extend
{
    public static class VisualTreeHelperEx
    {
        public static IEnumerable<DependencyObject> DFS(this DependencyObject root)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(root);
            yield return root;
            for (int i = 0; i < childrenCount; i++)
                foreach (var c in DFS(VisualTreeHelper.GetChild(root, i)))
                    yield return c;
        }

        public static IEnumerable<DependencyObject> BFS(this DependencyObject root)
        {
            Queue<DependencyObject> queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int childrenCount = VisualTreeHelper.GetChildrenCount(current);
                yield return current;
                for (int i = 0; i < childrenCount; i++)
                    queue.Enqueue(VisualTreeHelper.GetChild(current, i));
            }
        }
    }
}
