using Marqdouj.DotNet.Web.JsInterop.Extensions;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Sandbox.JsInterop
{
    public class SandboxJsInterop(IJSRuntime jsRuntime) : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = jsRuntime.CreateModuleTask("./_content/Sandbox.JsInterop/sandbox-jsinterop.js");

        public async ValueTask<string> TestGeoJSON(GeoJsonObject feature)
        {
            return await moduleTask.InvokeAsyncTC<string>(GetJsInteropMethod(), feature);
        }

        public async ValueTask<string> TestGeoJSONString(GeoJsonObject feature)
        {
            var json = feature.ToJson();
            return await moduleTask.InvokeAsyncTC<string>(GetJsInteropMethod(), json);
        }

        public async ValueTask DisposeAsync() => await moduleTask.DisposeAsync();

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
