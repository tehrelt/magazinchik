namespace magazinchik.DTOs;

public class SneakersPhotoDto
{
    public ulong Id { get; set; }
    public string SneakerName { get; set; }
    public string PhotoBytes { get; set; }
}

public class SneakersPhotoInputDto
{
    public string PhotoString { get; set; }
}