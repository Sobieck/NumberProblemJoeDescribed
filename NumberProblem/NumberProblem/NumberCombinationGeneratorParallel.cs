using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumberProblem
{
    public class NumberCombinationGeneratorParallel
    {
        private ConcurrentDictionary<int, List<List<int>>> CachedResults { get; set; }

        public NumberCombinationGeneratorParallel()
        {
            CachedResults = new ConcurrentDictionary<int, List<List<int>>>();
        }

        public List<List<int>> Generate(int valuePassedIn)
        {
            if (CachedResults.TryGetValue(valuePassedIn, out List<List<int>> cachedResult))
            {
                return cachedResult;
            }

            var uniqueResults = new ConcurrentDictionary<string, List<int>> ();

            AddToDictionary(uniqueResults, new List<int> { valuePassedIn });

            Parallel.For(1, valuePassedIn, i =>
            {
               var topNumber = valuePassedIn - i;
               var bottomNumber = i;

               if (bottomNumber > topNumber)
               {
                   return;
               }

               var listOfListFromTop = Generate(topNumber);
               var listOfListFromBottom = Generate(bottomNumber);

               foreach (var topItem in listOfListFromTop)
               {
                   foreach (var bottomItem in listOfListFromBottom)
                   {
                       var newBottomListCopy = new List<int>(bottomItem);

                       newBottomListCopy.AddRange(topItem);

                       AddToDictionary(uniqueResults, newBottomListCopy);
                   }
               }
            });
            
            var result = new List<List<int>>();

            foreach (var item in uniqueResults)
            {
                result.Add(item.Value);
            }

            CachedResults.TryAdd(valuePassedIn, result);

            return result;
        }

        private void AddToDictionary(ConcurrentDictionary<string, List<int>> resultList, List<int> list)
        {
            list.Sort();

            var key = String.Join(String.Empty, list.ConvertAll(x => x.ToString()));

            if (!resultList.ContainsKey(key))
            {
                resultList.GetOrAdd(key, list);
            }
        }

    }
}
