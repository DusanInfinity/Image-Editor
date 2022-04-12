using System.Collections.Generic;

namespace MMSP1.Models
{
    public static class LinkedListExtensions
    {
        public static void AddElementAtEndAndRemoveFirstIfBufferOverflow<T>(this LinkedList<T> list, T element, int size)
        {
            list.AddLast(element);

            if (list.Count > size)
                list.RemoveFirst();
        }
    }
}
