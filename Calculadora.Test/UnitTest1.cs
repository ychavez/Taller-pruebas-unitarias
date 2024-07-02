using System.Drawing;

namespace Calculadora.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            Matematicas matematicas = new();
            var numero1 = 1;
            var numero2 = 4;
            int resultado;

            //Act
            resultado = matematicas.Sumar(numero1, numero2);


            //Assert
            Assert.Equal(5, resultado);
        }


        [Theory]
        [InlineData(1,1,2)]
        [InlineData(5, 7, 12)]
        [InlineData(6, 7, 13)]
        public void Test2(int numero1Val, int numero2Val, int resultadoVal)
        {
            //Arrange
            Matematicas matematicas = new();
            var numero1 = numero1Val;
            var numero2 = numero2Val;
            int resultado ;

            //Act
            resultado = matematicas.Sumar(numero1, numero2);


            //Assert
            Assert.Equal(resultado, resultadoVal);
        }
    }
}