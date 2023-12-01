namespace magazinchik.DTOs;

public enum SizeCodes 
{
    EU, US
}

public class SneakerSizeDto
{
    public ulong Id { get; set; }
    public string SizeCode { get; set; }
    public double Size { get; set; }
    public double SizeInCm { get; set; }
}

public class SneakerSizeInputDto
{
    public double EuSize { get; set; }
    public double UsSize { get; set; }
    public double CmSize { get; set; }
}