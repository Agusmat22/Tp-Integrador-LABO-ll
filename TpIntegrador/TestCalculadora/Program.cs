using Entidades;

namespace TestCalculadora
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Numeracion operador1 = new Numeracion(100, ESistema.Decimal);
            //Numeracion operador2 = new Numeracion(50, ESistema.Decimal);

            Numeracion operador3 = new Numeracion(-10, ESistema.Binario);
            Numeracion operador4 = new Numeracion(-10, ESistema.Binario);

            //Operacion calculadora = new Operacion(operador1, operador2);

            Operacion calculadora2 = new Operacion(operador4, operador3);

            //Numeracion resultado = calculadora.Operar('*');
            Numeracion resultado2 = calculadora2.Operar('a');

            //Console.WriteLine(resultado.ValorNumerico);
            Console.WriteLine(resultado2.ConvertirA(ESistema.Binario));
            Console.WriteLine(operador3.ValorNumerico);
        }
    }
}