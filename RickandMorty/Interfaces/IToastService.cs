using System;
namespace RickandMorty.Interfaces
{
    public interface IToastService
    {
        void ShowToast(string message, bool isLongToast = false);
    }
}
