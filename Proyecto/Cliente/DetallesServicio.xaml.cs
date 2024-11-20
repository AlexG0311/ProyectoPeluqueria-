namespace Proyecto;

using Microsoft.Maui.ApplicationModel.Communication;
using Proyecto.Model;
using System.Collections.ObjectModel;


public partial class DetallesServicio : ContentPage
{
  
    public DetallesServicio()
    {
		InitializeComponent();

      


    }


    private async void OnNavigateButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Reserva());
    }






}