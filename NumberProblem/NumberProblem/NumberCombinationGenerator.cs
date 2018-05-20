using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NumberProblem
{
    public class NumberCombinationGenerator
    {
        private ConcurrentDictionary<int, List<List<int>>> CachedResults { get; set; }

        public NumberCombinationGenerator()
        {
            CachedResults = new ConcurrentDictionary<int, List<List<int>>>();
        }

        public List<List<int>> Generate(int valuePassedIn)
        {
            if (CachedResults.TryGetValue(valuePassedIn, out List<List<int>> cachedResult))
            {
                return cachedResult;
            }

            var uniqueResults = new Dictionary<string, List<int>> ();

            AddToDictionary(uniqueResults, new List<int> { valuePassedIn });

            for (var i = 1; i < valuePassedIn; i++)
            {
                var topNumber = valuePassedIn - i;
                var bottomNumber = i;

                if(bottomNumber > topNumber)
                {
                    break;
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
            }
            
            var result = new List<List<int>>();

            foreach (var item in uniqueResults)
            {
                result.Add(item.Value);
            }

            CachedResults.TryAdd(valuePassedIn, result);

            return result;
        }

        private void AddToDictionary(Dictionary<string, List<int>> resultList, List<int> list)
        {
            list.Sort();

            var key = String.Join(String.Empty, list.ConvertAll(x => x.ToString()));

            if (!resultList.ContainsKey(key))
            {
                resultList.Add(key, list);
            }
        }

    }
}
