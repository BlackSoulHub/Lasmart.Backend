using System.Text.Json.Serialization;

namespace Lastmart.Domain.DBModels
{
    /// <summary>
    /// Модель комментария.
    /// </summary>
    public class CommentModel
    {
        /// <summary>
        /// Id комментария.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Текст комментария.
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Цвет фона комментария.
        /// </summary>
        public string BackgroundColor { get; set; }
        
        /// <summary>
        /// Связь с точкой.
        /// </summary>
        [JsonIgnore]
        public DotModel Dot { get; set; }
    }
}