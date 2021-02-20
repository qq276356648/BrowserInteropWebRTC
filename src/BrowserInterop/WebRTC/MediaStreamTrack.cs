using System;
using System.Threading.Tasks;
using BrowserInterop.Extensions;
using Microsoft.JSInterop;

namespace BrowserInterop.WebRTC
{
    public class MediaStreamTrack : JsObjectWrapperBase
    {
        internal static readonly object SerializationSpec = new
        {
            id = true,
            isolated = true,
            kind = true,
            label = true,
            muted = true
        };
        
        public event EventHandler OnEnded;
        public event EventHandler OnIsolationChange;
        public event EventHandler OnMute;
        public event EventHandler OnUnmute;
        
        public static async ValueTask<MediaStreamTrack> Create(IJSRuntime jsRuntime)
        {
            var window = await jsRuntime.GetWindowPropertyRef("window").ConfigureAwait(false);
            var jsObj = await jsRuntime.InvokeInstanceMethodGetRef(window, "newMediaStreamTrack")
                .ConfigureAwait(false);
            var ret = await jsRuntime.GetInstanceContent<MediaStreamTrack>(jsObj, SerializationSpec)
                .ConfigureAwait(false);
            ret.SetJsRuntime(jsRuntime, jsObj);
            await ret.ConfigCallbacks().ConfigureAwait(false);
            return ret;
        }
        
        public string Id { get; set; }
        
        public bool Isolated { get; set; }
        
        public string Kind { get; set; }
        
        public string Label { get; set; }
        
        public bool Muted { get; set; }
        
        public string ReadyState { get; set; }

        public async ValueTask<MediaStreamTrack> Clone()
        {
            var objRef = await JsRuntime.InvokeInstanceMethodGetRef(JsObjectRef, "clone")
                .ConfigureAwait(false);
            var obj = await JsRuntime.GetInstanceContent<MediaStreamTrack>(objRef, SerializationSpec)
                .ConfigureAwait(false);
            obj.SetJsRuntime(JsRuntime, objRef);
            return obj;
        }

        public async ValueTask Stop()
        {
            await JsRuntime.InvokeInstanceMethod(JsObjectRef, "stop").ConfigureAwait(false);
        }

        public async ValueTask<MediaTrackSettings> GetSettings()
        {
            return await JsRuntime.InvokeInstanceMethod<MediaTrackSettings>(JsObjectRef, "getSettings")
                .ConfigureAwait(false);
        }

        public async ValueTask<MediaTrackCapabilities> GetCapabilities()
        {
            return await JsRuntime.InvokeInstanceMethod<MediaTrackCapabilities>(JsObjectRef, "getCapabilities")
                .ConfigureAwait(false);
        }

        private async ValueTask ConfigCallbacks()
        {
            await JsRuntime.AddEventListener(JsObjectRef, "", "onended",
                CallBackInteropWrapper.Create(OnEndedCallback, false)).ConfigureAwait(false);
            await JsRuntime.AddEventListener(JsObjectRef, "", "onisolationchange",
                CallBackInteropWrapper.Create(OnIsolationChangeCallback, false)).ConfigureAwait(false);
            await JsRuntime.AddEventListener(JsObjectRef, "", "onmute",
                CallBackInteropWrapper.Create(OnMuteCallback, false)).ConfigureAwait(false);
            await JsRuntime.AddEventListener(JsObjectRef, "", "onunmute",
                CallBackInteropWrapper.Create(OnUnmuteCallback, false)).ConfigureAwait(false);
        }

        private async ValueTask OnEndedCallback()
        {
            ReadyState = "ended";
            OnEnded?.Invoke(this, EventArgs.Empty);
        }

        private async ValueTask OnIsolationChangeCallback()
        {
            Isolated = await JsRuntime.GetInstanceProperty<bool>(JsObjectRef, "isolated")
                .ConfigureAwait(false);
            OnIsolationChange?.Invoke(this, EventArgs.Empty);
        }

        private async ValueTask OnMuteCallback()
        {
            Muted = true;
            OnMute?.Invoke(this, EventArgs.Empty);
        }

        private async ValueTask OnUnmuteCallback()
        {
            Muted = false;
            OnUnmute?.Invoke(this, EventArgs.Empty);
        }
    }
}