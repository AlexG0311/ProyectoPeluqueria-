using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proyecto.Model
{
  public  class Carrito : INotifyPropertyChanged
    {
        private int _cantidad;

        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Imagen { get; set; }

        public int Cantidad
        {
            get => _cantidad;
            set
            {
                if (_cantidad != value)
                {
                    _cantidad = value;
                    OnPropertyChanged();  // Notificar cambios
                    OnPropertyChanged(nameof(PrecioTotal));  // Notificar cuando cambie el total del precio
                }
            }
        }

        // Calcular el total de precio por producto
        public double PrecioTotal => Precio * Cantidad;

        // Comando para aumentar la cantidad
        public ICommand IncreaseCommand => new Command(() =>
        {
            Cantidad++;
        });

        // Comando para disminuir la cantidad
        public ICommand DecreaseCommand => new Command(() =>
        {
            if (Cantidad > 1) Cantidad--;
        });

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
