using Acr.UserDialogs;
using FreshMvvm;
using PropertyChanged;
using System.Threading.Tasks;

namespace WeatherApp.PageModels
{
    //Added attribute to notify the proerty change
    [AddINotifyPropertyChangedInterface]
    public class BasePageModel : FreshBasePageModel
    {
        public bool isBusy;
        public BasePageModel()
        {

        }

        //Created virtual method to override and modify at page level
        public virtual async Task<bool> ShowAlert(string title, string message, string ok = "", string cancel = "")
        {
            return await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Title = title,
                Message = message,
                OkText = ok,
                CancelText = cancel
            });
        }
    }
}
