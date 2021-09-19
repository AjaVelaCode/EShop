using System.Collections.Generic;

public class Computers
{
    public List<ComputerData> computersList { get; set; }
}

public class ComputerData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    public string Procesor { get; set; }
    public string Ram { get; set; }
}


