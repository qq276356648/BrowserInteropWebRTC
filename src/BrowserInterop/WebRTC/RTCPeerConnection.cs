using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Buffers;
using System.Runtime.InteropServices;
using BrowserInterop.Extensions;

namespace BrowserInterop.WebRTC
{
    public class RTCPeerConnection : JsObjectWrapperBase
    {
        internal static readonly object SerializationSpec = new
        {
            canTrickleIceCandidates = true,
            connectionState = true,
            currentLocalDescription = true,
            currentRemoteDescription = true,
            iceConnectionState = true,
            iceGatheringState = true,
            idpErrorInfo = true,
            idpLoginUrl = true,
            localDescription = true,
        };
        
        private DotNetObjectReference<RTCPeerConnection> myRef;

        public static async ValueTask<RTCPeerConnection> Create(IJSRuntime jsRuntime, object config)
        {
            var window = await jsRuntime.GetWindowPropertyRef("window").ConfigureAwait(false);

            var jsRef = await jsRuntime.InvokeInstanceMethodGetRef(window, "newRTCPeerConnection", config)
                .ConfigureAwait(false);

            return await jsRuntime.GetInstanceContent<RTCPeerConnection>(jsRef, SerializationSpec)
                .ConfigureAwait(false);
        }
        
        public bool? CanTrickleIceCandidates { get; set; }
        
        public string ConnectionState { get; set; }
        
        public string IceConnectionState { get; set; }

        public string IceGatheringState { get; set; }
        
        public string SignalingState { get; set; }
        
        public string IdpErrorInfo { get; set; }
        
        public string IdpLoginUrl { get; set; }

        public async ValueTask AddIceCandidate(RTCIceCandidateInit candidate)
        {
            await JsRuntime.InvokeInstanceMethod(JsObjectRef, "addIceCandidate", candidate)
                .ConfigureAwait(false);
        }
    }
}