

namespace Model;
public class User {

    public string Name { get; set; }
    public string Email { get; set; }
    public string Pwd { get; set; }
    public int Id { get; set; }

    public int Validate(int n)
    {
        try {
            int r = Id / n;
            return r;
        } catch (Exception) {
            throw;
        }
    }
}