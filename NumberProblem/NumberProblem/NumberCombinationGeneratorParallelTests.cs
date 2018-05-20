using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;
using System.Collections.Generic;
using System.Linq;

namespace NumberProblem
{
    [TestClass]
    public class NumberCombinationGeneratorParallelTests
    {
        private NumberCombinationGeneratorParallel numberGenerator;
        private List<List<int>> expected;

        public NumberCombinationGeneratorParallelTests()
        {
            numberGenerator = new NumberCombinationGeneratorParallel();
        }

        [TestInitialize]
        public void BeforeEach()
        {
            expected = new List<List<int>>();
        }

        [TestMethod]
        public void AllListsReturnedShouldSumToValuePassedIn()
        {
            var valuePassedIn = RandomValue.Int(40) + 1;

            var result = numberGenerator.Generate(valuePassedIn);

            foreach (var numberList in result)
            {
                numberList.Sum().Should().Be(valuePassedIn);
            }
        }

        [TestMethod]
        public void OneShouldCountainOne()
        {
            var result = numberGenerator.Generate(1);

            expected.Add(new List<int> { 1 });

            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void TwoShouldCountainTwoAndOneAndOne()
        {
            var result = numberGenerator.Generate(2);

            expected.Add(new List<int> { 2 });
            expected.Add(new List<int> { 1, 1 });

            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void TwoShouldBeTwoLong()
        {
            var result = numberGenerator.Generate(2);

            result.Should().HaveCount(2);
        }

        [TestMethod]
        public void ThreeShouldBeRight()
        {
            var result = numberGenerator.Generate(3);

            expected.Add(new List<int> { 3 });
            expected.Add(new List<int> { 2, 1 });
            expected.Add(new List<int> { 1, 1, 1 });

            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void FourShouldBeRight()
        {
            var result = numberGenerator.Generate(4);

            expected.Add(new List<int> { 4 });
            expected.Add(new List<int> { 3, 1 });
            expected.Add(new List<int> { 2, 1, 1 });
            expected.Add(new List<int> { 2, 2 });
            expected.Add(new List<int> { 1, 1, 1, 1 });

            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ShouldResultInTheSameResultAsNormalGenerator()
        {
            var valuePassedIn = RandomValue.Int(25) + 1;

            var expected = new NumberCombinationGenerator().Generate(valuePassedIn);

            var result = numberGenerator.Generate(valuePassedIn);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
