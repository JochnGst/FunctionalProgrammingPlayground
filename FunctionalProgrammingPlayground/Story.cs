using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingPlayground
{
    public class Story
    {
        public char Code { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        public int NumberOfEpisodes { get; set; }
        public int NumberOfMissingEpisodes { get; set; }

        public static IEnumerable<Story> Stories = new[]
        {
            new Story
            {
                Code = 'A',
                Title = "A new Way",
                Rating = 5,
                NumberOfEpisodes = 7,
                NumberOfMissingEpisodes = 8
            },
            new Story
            {
                Code = 'B',
                Title = "A new Way",
                Rating = 6,
                NumberOfEpisodes = 4,
                NumberOfMissingEpisodes = 3
            },
            new Story
            {
                Code = 'D',
                Title = "A new Way",
                Rating = 3,
                NumberOfEpisodes = 6,
                NumberOfMissingEpisodes = 8
            },
            new Story
            {
                Code = 'C',
                Title = "A new Way",
                Rating = 8,
                NumberOfEpisodes = 2,
                NumberOfMissingEpisodes = 6
            },
            new Story
            {
                Code = 'D',
                Title = "A new Way",
                Rating = 6,
                NumberOfEpisodes = 9,
                NumberOfMissingEpisodes = 4
            },
        };
    }
}
