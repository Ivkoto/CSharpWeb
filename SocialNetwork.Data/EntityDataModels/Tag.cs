namespace SocialNetwork.Data.EntityDataModels
{
    using SocialNetwork.Data.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [TagValidator]
        [MaxLength(20)]
        public string TagTitle { get; set; }

        public ICollection<AlbumTag> Albums { get; set; }
    }
}