namespace Model;

public class Result {
    public object Value { get; set; }
    public bool Success { get; set; }
    
    public string[] Messages { get; set; }
}