using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dnc.Common.Razor.Wrapper
{
    public class DncWrapperComponent:ComponentBase
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }
        public bool Loading { get; private set; }

        private CancellationTokenSource cancellationTokenSource;
        public async Task<T> AwaitTask<T>(Task<T> task)
        {
            SetLoading(true);

            try
            {
                cancellationTokenSource = new CancellationTokenSource();
                return await task.WaitAsync(cancellationTokenSource.Token);
            }
            catch (TaskCanceledException e)
            {
                e.Data.Add("CanceledTask", "true");
                throw;
            }
            finally
            {
                SetLoading(false);
                cancellationTokenSource = null;
            }
        }
        public void SetLoading(bool loadoing)
        {
            Loading = loadoing;
            StateHasChanged();
        }
    }
}
