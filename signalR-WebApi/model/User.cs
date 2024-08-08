

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
}