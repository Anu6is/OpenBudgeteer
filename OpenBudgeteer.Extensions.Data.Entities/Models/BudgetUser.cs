using OpenBudgeteer.Core.Data.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenBudgeteer.Extensions.Data.Entities.Models;

public class BudgetUser : IEntity
{
    [Key, Column("UserId")]
    public Guid Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public string? Email { get; set; }
}
