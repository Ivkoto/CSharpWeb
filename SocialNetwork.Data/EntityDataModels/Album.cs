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

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<AlbumPicture> Pictures { get; set; } = new List<AlbumPicture>();

        public ICollection<AlbumTag> Tags { get; set; } = new List<AlbumTag>();

        public ICollection<UserSharedAlbums> UsersWithRoles { get; set; } = new List<UserSharedAlbums>();
    }
}