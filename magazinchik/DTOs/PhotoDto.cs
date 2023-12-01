namespace magazinchik.DTOs;

public class PhotoDto
{
    public ulong Id { get; set; }
    public string Url { get; set; }
}

public class PhotoInputDto
{
    public string Url { get; set; }
}