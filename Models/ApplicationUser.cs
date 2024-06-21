using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OptiWorksAPI.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? City { get; set; }
    public string? PostCode { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }

  }
}