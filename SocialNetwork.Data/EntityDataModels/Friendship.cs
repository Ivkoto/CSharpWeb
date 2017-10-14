namespace SocialNetwork.Data.EntityDataModels
{
    public class Friendship
    {
        public int FromFriendId { get; set; }

        public int ToFriendId { get; set; }

        public User FromFriend { get; set; }

        public User ToFriend { get; set; }
    }
}