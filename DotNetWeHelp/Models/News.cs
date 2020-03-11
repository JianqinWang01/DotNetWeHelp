using System;
using System.ComponentModel.DataAnnotations;
namespace DotNetWeHelp.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public String ImagePath { get; set; }
        [DataType(DataType.Text)]
        public String Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public News()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
        }

    }
}
