using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distance.BLL.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Distance.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UniversityDetail : ContentPage
	{
        private University selectedUniversity;

        public UniversityDetail ()
		{
			InitializeComponent ();
		}

        public UniversityDetail(University _selectedUniversity)
        {
            InitializeComponent();
            this.selectedUniversity = _selectedUniversity;
            t.Text = selectedUniversity.arabicName;
            universityImage.Source = selectedUniversity.url;
            descrption.Text = selectedUniversity.discription;
        }
    }
}