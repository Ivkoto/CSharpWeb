namespace SocialNetwork.Data.EntityDataModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<AlbumPicture> Pictures { get; set; } = new List<AlbumPicture>();

        public int UserId { get; set; }

        public User User { get; set; }
    }
}