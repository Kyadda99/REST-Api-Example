using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class MovieResponse
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Slug { get; init; }
        public required int YearOfRelese { get; init; }
        public required IEnumerable<string> Generes { get; init; } = Enumerable.Empty<string>();
    }
}
