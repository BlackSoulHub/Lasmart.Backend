using System.Collections.Generic;

namespace Lastmart.Domain.DBModels
{
    /// <summary>
    /// Модель точки.
    /// </summary>
    public class DotModel
    {
        /// <summary>
        /// Id точки.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Позиция X. 
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// Позиция Y.
        /// </summary>
        public int Y { get; set; }
        
        /// <summary>
        /// Радиус точки.
        /// </summary>
        public int Radius { get; set; }
        
        /// <summary>
        /// Цвет точки.
        /// </summary>
        public string Color { get; set; }
        
        /// <summary>
        /// Связь с таблицей Comments
        /// </summary>
        public IEnumerable<CommentModel> Comments { get; set; }
    }
}