using System.Collections.Generic;

namespace Lastmart.Domain.Models.Dto
{
    public class DotDto
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
        public string Color { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}