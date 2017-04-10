using System;
using Weather.Common;
using System.Threading.Tasks;
using Android.App;
using Android.Content;

namespace Weather
{
    public class DialogService : IDialogService
    {
        private const string OkMessage = "Ok";
        
        private class DialogInstance : Java.Lang.Object, IDialogInterfaceOnDismissListener
        {
            private TaskCompletionSource<int> taskCompletionSource;

            public DialogInstance()
            {
                taskCompletionSource = new TaskCompletionSource<int>();
            }

            public void OnDismiss(IDialogInterface dialog)
            {
                taskCompletionSource.SetResult(0);
            }

            public Task DismissingTask
            {
                get
                {
                    return taskCompletionSource.Task;
                }
            }
        }
        
        public async Task ShowErrorAsync(string error, string title)
        {
            using (var dialogInstance = new DialogInstance())
            {
                new AlertDialog.Builder(MainActivity.CurrentActivity)
                    .SetTitle(title)
                    .SetMessage(error)
                    .SetPositiveButton(OkMessage, (IDialogInterfaceOnClickListener)null)
                    .SetOnDismissListener(dialogInstance)
                    .Show();

                await dialogInstance.DismissingTask;
            }
        }
    }
}

