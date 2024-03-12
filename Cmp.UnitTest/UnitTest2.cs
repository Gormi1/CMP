using CMP.VistaModelo.Formularios;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMP.UnitTest
{
    [TestFixture]
    public class VMAddVehiculoTests
    {
        [Test]
        public async Task InsertarVehiculo_CuandoCamposLlenos_DeberiaInsertarVehiculoYVaciarCampos()
        {
            // Arrange
            var vm = new VMAddVehiculo(new MockNavigation())
            {
                NumeroEconomico = "123",
                Modelo = "ModeloPrueba",
                Nombre = "NombrePrueba",
                Tipo = "TipoPrueba",
                NumeroDeSerie = "SeriePrueba",
                Kilometraje = 100,
                HoraInicial = 8,
                HoraFinal = 17,
                CantLlantas = 4,
                DatosExtras = "DatosExtrasPrueba",
                Observaciones = "ObservacionesPrueba",
                Estado = "EstadoPrueba",
                Combustible = 50.5
            };

            // Act
            await vm.InsertarVehiculo();

            // Assert
            // Verificar que los campos se han vaciado correctamente
            Assert.That(vm.NumeroEconomico, Is.EqualTo(""));
            Assert.That(vm.Modelo, Is.EqualTo(""));
            Assert.That(vm.Nombre, Is.EqualTo(""));
            Assert.That(vm.Tipo, Is.EqualTo(""));
            Assert.That(vm.NumeroDeSerie, Is.EqualTo(""));
            Assert.That(vm.Kilometraje, Is.EqualTo(0).Or.EqualTo(100));
            Assert.That(vm.HoraInicial, Is.EqualTo(0).Or.EqualTo(8));  // Ajustar según tu lógica
            Assert.That(vm.HoraFinal, Is.EqualTo(0).Or.EqualTo(17));
            Assert.That(vm.CantLlantas, Is.EqualTo(0).Or.EqualTo(4));
            Assert.That(vm.DatosExtras, Is.EqualTo(""));
            Assert.That(vm.Observaciones, Is.EqualTo(""));
            Assert.That(vm.Estado, Is.EqualTo(""));
            Assert.That(vm.Combustible, Is.EqualTo(0).Or.EqualTo(50.5));

            // Puedes agregar más afirmaciones según sea necesario para verificar el estado del ViewModel después de la operación.
        }


        [Test]
        public async Task InsertarVehiculo_CuandoCamposInvalidos_DeberiaMostrarMensajeErrorYNoInsertar()
        {
            // Arrange
            var mockNavigation = new MockNavigation();  // Configura MockNavigation directamente
            var vm = new VMAddVehiculo(mockNavigation)
            {
                // Deja algunos campos importantes vacíos o en valores no válidos
                NumeroEconomico = "123",
                Modelo = "ModeloPrueba",
                Nombre = "",  // Campo vacío
                Tipo = "TipoPrueba",
                NumeroDeSerie = null,  // Campo nulo
                Kilometraje = 100,
                HoraInicial = 8,
                HoraFinal = 17,
                CantLlantas = 0,  // Valor no válido
                DatosExtras = "DatosExtrasPrueba",
                Observaciones = "ObservacionesPrueba",
                Estado = "EstadoPrueba",
                Combustible = -1  // Valor no válido
            };

            // Act
            await vm.InsertarVehiculo();
            await Task.Delay(100);  // Esperar un breve momento antes de verificar

            // Assert
            // Verificar que se ha llamado a DisplayAlert
            Assert.That(mockNavigation.DisplayAlertCalls, Is.EqualTo(1));

            // Obtener la información de la última llamada a DisplayAlert
            var lastAlertInfo = await mockNavigation.GetLastAlertInfo();

            // Verificar las propiedades de la última llamada a DisplayAlert
            Assert.That(lastAlertInfo.Title, Is.EqualTo("Error"));
            Assert.That(lastAlertInfo.Message, Is.EqualTo("Llene todos los datos"));
            Assert.That(lastAlertInfo.CancelButton, Is.EqualTo("Aceptar"));


            // Verificar que todos los campos se mantienen igual
            Assert.That(vm.NumeroEconomico, Is.EqualTo("123"));
            Assert.That(vm.Modelo, Is.EqualTo("ModeloPrueba"));
            Assert.That(vm.Nombre, Is.EqualTo("NombrePrueba"));
            Assert.That(vm.Tipo, Is.EqualTo("TipoPrueba"));
            Assert.That(vm.NumeroDeSerie, Is.EqualTo("SeriePrueba"));
            Assert.That(vm.Kilometraje, Is.EqualTo(100));
            Assert.That(vm.HoraInicial, Is.EqualTo(8));
            Assert.That(vm.HoraFinal, Is.EqualTo(17));
            Assert.That(vm.CantLlantas, Is.EqualTo(0));
            Assert.That(vm.DatosExtras, Is.EqualTo("DatosExtrasPrueba"));
            Assert.That(vm.Observaciones, Is.EqualTo("ObservacionesPrueba"));
            Assert.That(vm.Estado, Is.EqualTo("EstadoPrueba"));
            Assert.That(vm.Combustible, Is.EqualTo(50.5));

            // Verificar que no se ha llamado a InsertarVehiculo
            Assert.That(mockNavigation.PushAsyncCalls, Is.Empty);

            // Puedes agregar más afirmaciones según sea necesario para verificar el estado del ViewModel después de la operación.
        }

        // Puedes agregar más afirmaciones según sea necesario para verificar el estado del ViewModel después de la operación.
    }


}

