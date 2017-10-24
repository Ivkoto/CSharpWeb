namespace SocialNetwork.Data.EntityDataModels
{
    using SocialNetwork.Data.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [PasswordValidator]
        public string Password { get; set; }

        [Required]
        [EmailValidator]
        public string Email { get; set; }

        [MaxLength(1024)]
        public byte[] ProfilePicture { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }

        public DateTime? LastTymeLoggedIn { get; set; }

        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120!")]
        public int? Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Friendship> RelatedFrom { get; set; } = new List<Friendship>();

        public ICollection<Friendship> RelatedTo { get; set; } = new List<Friendship>();

        public ICollection<Album> Albums { get; set; } = new List<Album>();

        public ICollection<UserSharedAlbums> SharedAlbums { get; set; }
    }
}