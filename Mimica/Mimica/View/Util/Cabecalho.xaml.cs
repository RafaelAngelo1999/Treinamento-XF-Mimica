using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mimica.View.Util
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cabecalho : ContentView
	{
		public Cabecalho ()
		{
			InitializeComponent ();
            BindingContext = new ViewModel.CabecalhoViewModel();
		}

        private void SairEvento(object sender, EventArgs args)
        {
            var vierModel = (ViewModel.CabecalhoViewModel)this.BindingContext;
            if (vierModel.Sair.CanExecute(null))
            {
                vierModel.Sair.Execute(null);
            }
        }
    }
}