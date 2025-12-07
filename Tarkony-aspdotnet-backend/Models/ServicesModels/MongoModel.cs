using System.ComponentModel.DataAnnotations;

namespace MongoExample.Models;

public class MongoDBSettings
{
    [Required]
    public string ConnectionURI { get; set; } = null!;

    [Required]
    public string DatabaseName { get; set; } = null!;

    [Required]
    public string CollectionName { get; set; } = null!;
}
