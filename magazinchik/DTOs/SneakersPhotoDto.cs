namespace magazinchik.DTOs;

public class SneakersPhotoDto
{
    public ulong Id { get; set; }
    public string SneakerName { get; set; }
    public string PhotoUrl { get; set; }
}

public class SneakersPhotosDto
{
    public ulong SneakerId { get; set; }
    public ulong[] PhotosIds { get; set; }
    public int Count { get; set; }
}