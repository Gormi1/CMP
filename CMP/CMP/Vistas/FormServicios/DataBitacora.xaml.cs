using CMP.Modelo;
using CMP.VistaModelo.Formularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas.FormServicios
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DataBitacora : ContentPage
	{
		public DataBitacora (MServicios parametro)
		{
			InitializeComponent ();
			BindingContext = new VMDataBitacora(Navigation, parametro);
		}
	}
}