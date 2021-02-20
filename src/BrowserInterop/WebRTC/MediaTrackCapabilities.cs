namespace BrowserInterop.WebRTC
{
    public class DoubleRange
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
    }

    public class ULongRange
    {
        public ulong? Max { get; set; }
        public ulong? Min { get; set; }
    }
    
    public class MediaTrackCapabilities
    {
        public DoubleRange AspectRatio { get; set; }
        public bool[] AutoGainControl { get; set; }
        public string DeviceId { get; set; }
        public bool[] EchoCancellation { get; set; }
        public string[] FacingMode { get; set; }
        public DoubleRange FrameRate { get; set; }
        public string GroupId { get; set; }
        public ULongRange Height { get; set; }
        public DoubleRange Latency { get; set; }
        public bool[] NoiseSuppression { get; set; }
        public string[] ResizeMode { get; set; }
        public ULongRange SampleRate { get; set; }
        public ULongRange SampleSize { get; set; }
        public ULongRange Width { get; set; }
    }
}