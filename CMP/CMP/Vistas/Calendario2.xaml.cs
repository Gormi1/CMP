using CMP.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calendario2 : ContentPage
	{
		public Calendario2 ()
		{
			InitializeComponent ();
            BindingContext = new VMCalendario(Navigation);
        }
	}
}