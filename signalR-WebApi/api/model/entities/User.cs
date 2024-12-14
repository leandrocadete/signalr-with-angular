

using Model.Services;

namespace Model;
public class User {

    public string Name { get; set; }
    public string Email { get; set; }
    public string Pwd { get; set; }
    public string Group { get; set;}
    public int Id { get; set; }

    public bool Validate()
    {
       return Pwd.Length > 6 && Pwd.Length < 25;
    }

    public bool ValidateToken(ILogin login) {
        var result = login.Login(this);
        if(!result.Success) return false;

        if (result.Value is null) return false;

        return true;
    }
}