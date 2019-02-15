namespace Forum.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Answers")]
    public class PostAnswer
    {
        public PostAnswer()
        {
            this.AnsweredOn = DateTime.Now;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        [Column("AnsweredOn")]
        public DateTime AnsweredOn { get; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}