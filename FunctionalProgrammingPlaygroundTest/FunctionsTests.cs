using FluentAssertions;
using FunctionalProgrammingPlayground;

namespace FunctionalProgrammingPlaygroundTest
{
    public class FunctionsTests
    {
        [Fact]
        public void ForkHelperTest()
        {
            var input = "aaabbbcccddd";
            var output = input.Fork(x => x.Sum(),
                x => x.Count(y => y == 'a'),
                x => x.Count(y => y == 'b'),
                x => x.Count(y => y == 'c'));
            output.Should().Be(9);
        }

        [Fact]
        public void AggregateTest()
        {
            var (totalMissing, totalOvarall) = Story.Stories.Aggregate(
                    (0,0),
                    (acc, curr) => (
                        acc.Item1 + curr.NumberOfMissingEpisodes,
                        acc.Item2 + curr.NumberOfEpisodes
                    )
                );
            var percentageMissing = Math.Round(totalMissing / (decimal)totalOvarall * 100, 2);
            percentageMissing.Should().Be(103.57M);
        }

        [Fact]
        public void BindTest()
        {
            var storys = Story.Stories.ToList();
            storys.Add(new Story
            {
                Title = "Das ist ein Test was hier alles stehen kann ohne Fehler",
                Code = 'T',
                NumberOfEpisodes = 1,
                NumberOfMissingEpisodes= 25,
                Rating = 10
            });
            var storyCode = 'T';
            var newTitle = storyCode.ToMaybe()
                .Bind(x => storys.Where(y => y.Code == x).First().Title)
                .Bind(x => x.Replace("a", "4"))
                .Bind(x => x.Replace("e", "3"))
                .Bind(x => x.Replace("i", "1"))
                .Bind(x => x.Replace("o", "0"));

            string title = newTitle;
            title.Should().Be("D4s 1st 31n T3st w4s h13r 4ll3s st3h3n k4nn 0hn3 F3hl3r");
            newTitle.Should().BeOfType<Some<string>>();


        }

        [Fact]
        public void BindNothingTest()
        {
            var storys = Story.Stories.ToList();
            storys.Add(new Story
            {
                Title = "Das ist ein Test was hier alles stehen kann ohne Fehler",
                Code = 'X',
                NumberOfEpisodes = 1,
                NumberOfMissingEpisodes = 25,
                Rating = 10
            });
            var storyCode = 'T';
            var newTitle = storyCode.ToMaybe()
                .Bind(x => storys.Where(y => y.Code == x).First().Title)
                .Bind(x => x.Replace("a", "4"))
                .Bind(x => x.Replace("e", "3"))
                .Bind(x => x.Replace("i", "1"))
                .Bind(x => x.Replace("o", "0"));

            newTitle.Value.Should().BeNull();
            newTitle.Should().BeOfType<Nothing<string>>();
        }

        [Fact]
        public void DoConversion()
        {
            var value = 13;
            value.ToMaybe()
                .Bind(x => Convert.ToDouble(x))
                .Bind(x => x * 1.5d);
                
        }
    }
}