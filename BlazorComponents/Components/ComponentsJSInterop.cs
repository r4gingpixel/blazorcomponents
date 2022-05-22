using Microsoft.JSInterop;

namespace Components
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class ComponentsJSInterop : IAsyncDisposable
    {
        private Lazy<Task<IJSObjectReference>>? moduleTask;        
        private bool _runtimeSet = false;

        public ComponentsJSInterop(IJSRuntime jsRuntime)
        {
            if (_runtimeSet == false)
            {

                moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Components/JsInterop.js").AsTask());
                _runtimeSet = true;
            }            
        }

        public async ValueTask<string> Prompt(string message)
        {
            //init(jsRuntime);
            
            if (moduleTask == null)
            {
                return "error";
            }

            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("showPrompt", message);
        }

        public async ValueTask<bool> CopyToClipboard(string text)
        {
            //init(jsRuntime);
            if (moduleTask == null)
            {
                return false;
            }

            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("CopyText", text);
            
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask!=null && moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}