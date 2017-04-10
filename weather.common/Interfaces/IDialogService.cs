using System;
using System.Threading.Tasks;

namespace Weather.Common
{
    public interface IDialogService
    {
        Task ShowErrorAsync(string error, string title);
    }
}

