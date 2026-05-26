namespace DriverSearchApp.Models;
public class Driver
{
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public override string ToString() => $"Id: {Id}, X: {X}, Y: {Y}";
}
