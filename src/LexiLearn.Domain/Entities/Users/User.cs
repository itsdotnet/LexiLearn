using LexiLearn.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace LexiLearn.Domain.Entity.User;

public class User : Auditable
{
    [MaxLength(25), MinLength(2)]
    public string FirstName { get; set; }

    [MaxLength(25), MinLength(2)]
    public string LastName { get; set; }

    [MaxLength(25), MinLength(3)]
    public string UserName { get; set; }

    public string Email { get; set; }

    public bool IsEmailVerifed { get; set; }

    public string Password { get; set; }

    public long Score { get; set; }
}