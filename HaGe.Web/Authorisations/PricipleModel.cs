using System;

namespace HaGe.Web.Authorisations; 

public class PrincipleModel
{
    public PrincipleModel() {
        RoleId = Guid.Empty;
    }
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string RoleName { get; set; }
    public string Name { get; set; }
    public string LastLogin { get; set; }
    public string Version { get; set; }
    public Guid? RoleId { get; set; }
    public string Token { get; set; }
    public Guid AccountId { get; set; }
}