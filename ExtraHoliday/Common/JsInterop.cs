using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ExtraHoliday.Common;
internal class JsInterop(IJSRuntime jsRuntime) : IAsyncDisposable {//Cannot invoke JavaScript outside of a WebView context if it isn't scoped in maui
    readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./common.js").AsTask());

    public async ValueTask ScrollToElement(ElementReference element) => await (await moduleTask.Value).InvokeVoidAsync("scrollToElement", element);
    public ValueTask HistoryBack() => jsRuntime.InvokeVoidAsync("window.history.back");

    public async ValueTask DisposeAsync() {
        if (moduleTask.IsValueCreated)
            await (await moduleTask.Value).DisposeAsync();
    }
}