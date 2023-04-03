using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using HaGe.Core.Entities.Abstract;
using HaGe.Core.Entities.Base;

namespace HaGe.Core.Entities;

public class User : BaseEntity, IEntityBase<Guid> {
    #region Attributes

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public DateTime LastLogin { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public Guid? CountryId { get; set; }

    public int? Gender { get; set; }
    public Guid? RoleId { get; set; }

    public bool AccountActivated { get; set; } = false;
    public string? ProfilePhoto { get; set; }
    public int? Status { get; set; }

    #endregion

    #region Not Mapped Attributes

    [NotMapped]
    public String? Name => $"{FirstName} {LastName}";

    [NotMapped]
    public Role? Role { get; set; }

    #endregion

    #region Constructor

    public User(string firstName, string lastName, string email, string password) {
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Username = email;
        Password = password;
        Status = 0;
        LastLogin = SqlDateTime.MinValue.Value;
    }

    public User (){}

    #endregion
}