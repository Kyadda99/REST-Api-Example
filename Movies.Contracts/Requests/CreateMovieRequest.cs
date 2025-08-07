using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Requests
{
    public class CreateMovieRequest
    {
        public required string Title { get; init; }
        public required int YearOfRelese { get; init; }
        public required IEnumerable<string> Generes { get; init; } = Enumerable.Empty<string>();


    }
}
