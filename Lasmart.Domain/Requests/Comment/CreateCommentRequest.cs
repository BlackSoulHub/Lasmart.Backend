namespace Lastmart.Domain.Requests.Comment
{
    public class CreateCommentRequest
    {
        public int DotId { get; set; }
        public string Text { get; set; }
        public string BackgroundColor { get; set; }
    }
}