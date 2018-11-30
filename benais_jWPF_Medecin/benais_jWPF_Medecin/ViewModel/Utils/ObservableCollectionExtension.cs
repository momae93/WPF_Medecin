using System;
using System.Collections.ObjectModel;

namespace benais_jWPF_Medecin.ViewModel.Utils
{
    public static class ObservableCollectionExtension
    {
        /// <summary>
        /// Remove all extension for observable collections
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="condition"></param>
        public static void RemoveAll<T>(this ObservableCollection<T> collection, Func<T, bool> condition)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
                if (condition(collection[i]))
                    collection.RemoveAt(i);
        }
    }
}
