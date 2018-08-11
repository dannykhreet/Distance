using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Distance.BLL.ViewModel
{
    public class ViewModelBase : BindableBase
    {
      

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public ViewModelBase()
        {


        }
    }

}
