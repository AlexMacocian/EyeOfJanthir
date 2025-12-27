namespace EyeOfJanthir.Options;

public sealed class PortainerOptions
{
    public required string Url { get; set; }
    public required string ApiKey { get; set; }
    public TimeSpan Frequency { get; set; }
}
