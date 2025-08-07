using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies.Application.Models
{
    public partial class Movie
    {
        public required Guid Id { get; init; }
        public required string Title { get; set; }
        public string Slug => GenerateSlug();
        public required int YearOfRelese { get; set; }
        public required List<string> Genres { get; init; } = new();

        private string GenerateSlug()
        {
            var slug = ToSlugRegex().Replace(Title, "")
                            .Replace(" ", "-")
                            .ToLower();
            return $"{slug}-{YearOfRelese}";
        }

        [GeneratedRegex(@"[^a-zA-Z0-9\s]+", RegexOptions.NonBacktracking,7)]
        private static partial Regex ToSlugRegex();
    }
}
