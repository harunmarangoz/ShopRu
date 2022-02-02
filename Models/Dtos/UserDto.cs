using Models.Enums;

namespace Models.Dtos;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public RoleEnum Role { get; set; }  
}