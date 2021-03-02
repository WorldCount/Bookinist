using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bookinist.Infrastructure.Extensions
{
    internal static class ObservableCollectionExtension
    {

        public static void AddClear<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            collection.Add(items);
        }

        public static void Add<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
                collection.Add(item);
        }
    }
}
