using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
