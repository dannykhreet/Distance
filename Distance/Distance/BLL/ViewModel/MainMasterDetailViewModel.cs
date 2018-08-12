using Distance.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Distance.BLL.ViewModel
{
    public class MainMasterDetailViewModel : ViewModelBase
    {

        public bool _canClose = true;
        public Command ExitCommand { get; set; }

        public MainMasterDetailViewModel()
        {
            ExitCommand = new Command(async () => await ShowExitDialog());
        }


        public async Task ShowExitDialog()
        {
            String massage = "هل تريد بالتأكيد اغلاق البرنامج ؟";
            var answer = await App.Current.MainPage.DisplayAlert("تنبيه", massage, "موافق", "غير موافق");
            if (answer)
            {
                DependencyService.Get<IExit>().CloseApp();
                _canClose = false;
            }
        }
    }

}
