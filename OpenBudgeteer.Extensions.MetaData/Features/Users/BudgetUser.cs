using OpenBudgeteer.Core.Data.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenBudgeteer.Extensions.MetaData.Features.Users;

public class BudgetUser : IEntity
{
    [Key, Column("UserId")]
    public Guid Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public string? Email { get; set; }
}
