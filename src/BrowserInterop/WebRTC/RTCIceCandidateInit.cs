namespace BrowserInterop.WebRTC
{
    public class RTCIceCandidateInit
    {
        public string Candidate { get; set; }
        public int SdpMLineIndex { get; set; }
        public string SdpMid { get; set; }
        public string UsernameFragment { get; set; }
    }
}