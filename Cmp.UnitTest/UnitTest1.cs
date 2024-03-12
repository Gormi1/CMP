using CMP.VistaModelo.FormServicios;
using NUnit.Framework;
using Xamarin.Forms;
using System.Threading.Tasks;
using CMP.UnitTest;

namespace Cmp.UnitTest
{
    [TestFixture]
    public class VMAddServiciosTests
    {
        // Mock para INavigation (puedes utilizar un marco de prueba de doble falso o alguna implementaci�n de prueba)

        [Test]
        public async Task AgregarServicio_CuandoCamposLlenos_DeberiaInsertarServicioYVaciarCampos()
        {
            // Arrange
            var vm = new VMAddServicios(new MockNavigation());
            vm.NumeroEconomico = "123";
            vm.InventarioText = "InventarioPrueba";
            vm.TipoServicio = "TipoPrueba";

            // Act
            await vm.AgregarServicio();

            // Assert
            // Verificar que los campos se han vaciado correctamente
            Assert.That(vm.NumeroEconomico, Is.EqualTo(""));
            Assert.That(vm.InventarioText, Is.EqualTo(""));
            Assert.That(vm.TipoServicio, Is.EqualTo(""));

            // Verificar que la propiedad ResultadoFecha est� actualizada
            Assert.That(vm.ResultadoFecha, Is.Not.Null.Or.Empty);

            // Puedes agregar m�s afirmaciones seg�n sea necesario para verificar el estado del ViewModel despu�s de la operaci�n.
        }


        // Puedes agregar m�s pruebas seg�n sea necesario para cubrir otros casos y m�todos.
    }

   

}