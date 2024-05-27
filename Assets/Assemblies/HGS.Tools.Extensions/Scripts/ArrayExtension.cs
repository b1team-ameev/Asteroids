using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HGS {

    public static class ArrayExtension {
        
        private static Dictionary<int, System.Random> arRandoms = new Dictionary<int, System.Random>();

        public static T GetRandomValue<T>(this IEnumerable<T> collection) {

            if (collection == null || collection.Count() == 0) {

                return default;

            }

            int hash = collection.GetHashCode();

            if (!arRandoms.ContainsKey(hash)) {

                arRandoms[hash] = new System.Random();

            }

            System.Random random = arRandoms[hash];

            T result = collection.ElementAt(random.Next(0, collection.Count()));

            return result;

        }

        public static void Shuffle<T> (this T[] array) {

            int n = array.Length;
            while (n > 1)  {

                int k = Random.Range(0, n);

                T temp = array[n - 1];
                array[n - 1] = array[k];
                array[k] = temp;

                n--;
            }

        }

        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> collection) where T: class {

            if (collection == null) {

                return Enumerable.Empty<T>();

            }
            
            return collection.Where(item => item != null);

        }

    }

}