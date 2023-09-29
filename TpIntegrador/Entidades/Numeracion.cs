using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    

    public class Numeracion
    {
        //Enumerado con el tipo de sistema.
        public enum ESistema
        {
            Decimal,
            Binario
        }

        private ESistema sistema;
        private double valorNumerico;

        public Numeracion(double valorNumerico, ESistema sistema): this(valorNumerico.ToString(),sistema)
        {
           
        }

        //Sobrecargo el constructor

        public Numeracion(string valorNumerico, ESistema sistema)
        {
            //Inicializo todos los atributos
            InicializarValores(valorNumerico, sistema);
        }

        //GETTERS
        public ESistema Sistema
        {
            get
            {
                return this.sistema;
            }
            
            set
            {
                this.sistema = value;
            }

        }
        public string ValorNumerico
        {
            get
            {
                string valor;

                if (valorNumerico == double.MinValue) //valido si es infinito para las operaaciones de minValue
                {
                    valor = "Error";
                }
                else if(double.IsInfinity(valorNumerico))
                {
                    valor = "No se puede dividir por cero";
                }
                else if(Sistema == ESistema.Binario)
                {
                    valor = DecimalABinario(valorNumerico);
                }
                else
                {
                    valor = valorNumerico.ToString();
                }

                return valor;

            }

           
        }

  


        /// <summary>
        /// Este metodo se encarga de inicializar los valores de los atributos que contiene la clase
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="sistema"></param>
        private void InicializarValores(string valor, ESistema sistema)
        {
            this.sistema = sistema;
            

            if (this.sistema == ESistema.Binario && EsBinario(valor))
            {
                
                //Convierto binario a decimal
                this.valorNumerico = BinarioADecimal(valor);
                
                
            }
            else if (this.sistema == ESistema.Decimal && double.TryParse(valor, out double numero))
            {
                //Parseo string a decimal
                this.valorNumerico = numero;
            }
            else
            {
                this.valorNumerico = double.MinValue; //En caso de no ser de ningun sistema el dato ingresado, se asigna el minValue
            }






        }


        //VALIDADORES
        /// <summary>
        /// Valida si el valor ingresado es un numero binario
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>True en caso de ser binario y false en caso contrario</returns>
        private static bool EsBinario(string valor)
        {
            bool esBinario = true;

            if (!string.IsNullOrEmpty(valor))
            {
                foreach (char caracter in valor)
                {
                    if (caracter != '0' && caracter != '1')
                    {
                        esBinario = false;
                        break;
                    }
                }
            }

            return esBinario;
        }


        /// <summary>
        /// Convierte un binario a decimal
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Retornara el numero decimal en caso de no poder convertirse retornara 0</returns>
        private static double BinarioADecimal(string valor)
        {
            double numeroDecimal = 0;

            if (EsBinario(valor))
            {
                int cantidadCaracteres = valor.Length - 1;
                int digito; //sera el caracter 'char' luego ser parseado
                char caracter;

                for (int i = cantidadCaracteres; i >= 0; i--)
                {
                    //Ingreso al ultimo caracter
                    caracter = valor[i];
                    int.TryParse(caracter.ToString(), out digito);

                    //A medida que el I decrece aumenta la potencia, esta pensado de derecha a izquierda
                    numeroDecimal += digito * Math.Pow(2, (cantidadCaracteres - i));
                }
            }
            else
            {
                numeroDecimal = 0;
            }

            return numeroDecimal;
        }
        /// <summary>
        /// Convierte un decimal a binario
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Retorna un string</returns>
        private static string DecimalABinario(double valor)
        {
            return DecimalABinario(valor.ToString());
        }

        /// <summary>
        /// Recibe un string de tipo decimal y lo convierte a un binario
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Retornara el valor en caso contrario 'Numero Invalido'</returns>
        private static string DecimalABinario(string valor)
        {

            string numeroBinario = "";
            //valido que sea numerico y que sea mayor a 0 ya que binarios negativos no se puede
            if (int.TryParse(valor, out int numeroDecimal) && numeroDecimal > 0)
            {
                int divisor = 2;
                double resto;
                int dividendo = numeroDecimal;

                while (true)
                {

                    if (dividendo >= divisor)
                    {
                        resto = dividendo % divisor;
                        dividendo = dividendo / divisor;

                        numeroBinario += resto.ToString();
                        
                    }
                    else
                    {
                        numeroBinario += dividendo.ToString();
                        break;
                    }

                }
                //LE INDICO QUE MINIMO DEBE CONTENER 4 CHARS padLeft, SI TIENE MENOS LES VA AGREGAR 0 A LA IZQUIERDA
                numeroBinario = InvertirCadena(numeroBinario).PadLeft(4, '0');
            }
            else
            {
                numeroBinario = "Numero Invalido";
            }


            return numeroBinario;
        }
        /// <summary>
        /// Invierte una cadena
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Retorna un string</returns>
        private static string InvertirCadena(string valor)
        {
            StringBuilder cadenaInvertida = new StringBuilder();

            int largoCadena = valor.Length - 1;

            for (int i = largoCadena; i >= 0; i--)
            {
                cadenaInvertida.Append(valor[i]);
            }

            return cadenaInvertida.ToString();

        }

        /// <summary>
        /// Convierte un numero en el sistema ingresado
        /// </summary>
        /// <param name="sistema"></param>
        /// <returns>Un numero del tipo string</returns>
        public string ConvertirA(ESistema sistema)
        {
            if(sistema == ESistema.Binario && this.Sistema != sistema)
            {
                return DecimalABinario(this.ValorNumerico); 
            }
            else if(Sistema == ESistema.Decimal && this.Sistema != sistema)
            {
                return BinarioADecimal(this.ValorNumerico).ToString();
            }
            else
            {
                //return null; //NO RETORNO NULL PORQUE NO ME PARECE OPTIMO, PREFIERO RETORNAR UN MENSAJE
                return "Error en la conversion";
            }
        }

        //------------------------- SOBRECARGAR DE OPERADORES ASIGNACION  == o != ---------------------------------------//

        /// <summary>
        /// Dos numeraciones seran iguales si pertenecen al mismo Sistema
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static bool operator ==(Numeracion n1, Numeracion n2)
        {
            return n1.Sistema == n2.Sistema;

        }



        //Valida si la numeracion coincide con el sistema
        public static bool operator ==(ESistema sistema, Numeracion numeracion)
        {
            return numeracion.Sistema == sistema;
        }

        public static bool operator !=(Numeracion n1, Numeracion n2)
        {
            return !(n1 == n2);
        }

        public static bool operator !=(ESistema sistema, Numeracion numeracion)
        {
            return !(sistema == numeracion);
        }


        
        //------------------------- SOBRECARGAR DE OPERADORES ARITMETICOS  +, -, *, / ---------------------------------------//


        public static Numeracion operator +(Numeracion n1,Numeracion n2)
        {
            double resultado;

            if (n1 == n2)
            {
                resultado = n1.valorNumerico + n2.valorNumerico;
            }
            else
            {
                resultado = double.MinValue;
            }

            //retorno una nueva instancia
            return new Numeracion(resultado, n1.Sistema);
        }

        public static Numeracion operator -(Numeracion n1, Numeracion n2)
        {
            double resultado;

            if (n1 == n2)
            {
                resultado = n1.valorNumerico - n2.valorNumerico;
            }
            else
            {
                resultado = double.MinValue;
            }

            //retorno una nueva instancia
            return new Numeracion(resultado, n1.Sistema);
        }

        public static Numeracion operator *(Numeracion n1, Numeracion n2)
        {
            double resultado;

            if (n1 == n2)
            {
                resultado = n1.valorNumerico * n2.valorNumerico;
            }
            else
            {
                resultado = double.MinValue;
            }

            //retorno una nueva instancia
            return new Numeracion(resultado, n1.Sistema);
        }

       
        public static Numeracion operator /(Numeracion n1, Numeracion n2)
        {
            double resultado;

            if (n1 == n2)
            {
                resultado = n1.valorNumerico / n2.valorNumerico;

            }
            else
            {
                resultado = double.MinValue;
            }

            return new Numeracion(resultado, n1.Sistema);
        }


    }
}
