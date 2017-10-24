namespace SocialNetwork.Data.EntityDataModels
{
    public class UserSharedAlbums
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int SharedAlbumId { get; set; }

        public Album SharedAlbum { get; set; }
    }
}