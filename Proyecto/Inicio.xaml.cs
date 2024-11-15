

namespace Proyecto;

public partial class Inicio : FlyoutPage
{
	public Inicio()
	{
		InitializeComponent();

        Flyout = new Maestro();
        Detail = new NavigationPage(new Detalle());
        App.FlyoutPage = this;
    }
}