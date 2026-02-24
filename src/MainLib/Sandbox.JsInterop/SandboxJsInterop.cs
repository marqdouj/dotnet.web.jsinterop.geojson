using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Sandbox.JsInterop
{
    public class SandboxJsInterop(IJSRuntime jsRuntime) : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Sandbox.JsInterop/sandbox-jsinterop.js").AsTask());

        public async ValueTask<string> TestGeoJSON(GeoJsonObject feature)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>(GetJsInteropMethod(), feature);
        }

        public async ValueTask<string> TestGeoJSONString(GeoJsonObject feature)
        {
            var module = await moduleTask.Value;
            var json = feature.ToJson();
            return await module.InvokeAsync<string>(GetJsInteropMethod(), json);
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (moduleTask.IsValueCreated)
                {
                    var module = await moduleTask.Value;
                    await module.DisposeAsync();
                }
            }
            catch (JSDisconnectedException)
            {
            }
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
        {
            return name.ToJsonName();
        }
    }

    internal static class Extensions
    {
        /// <summary>
        /// first char must be lowercase
        /// </summary>
        internal static string ToJsonName(this string name)
        {
            var firstChar = name[0].ToString().ToLower();
            var remainder = name.Substring(1);
            return $"{firstChar}{remainder}";
        }
    }
}
