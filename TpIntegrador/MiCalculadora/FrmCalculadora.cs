using Entidades;

namespace MiCalculadora
{
    /*PREGUNTAR
     -La calculadora realiza calculos con numeros binarios o solo decimal?

     -Cuando nos pide inicializar el componente porque tiene la opcion de recibir un binario
    si no tenemos forma de identificarlo ya que con el metodo es binario no seria suficiente.?

     -El sistema es solo para luego mostrar el resultado o es para seleccionar el tipo de numero
    que va ingresar?

    -Y se puede hacer algun metodo extra del que esta en el tp o tiene que ser todo tal cual las consignas.

     
     */
    public partial class FrmCalculadora : Form
    {
        Numeracion operadorUno;
        Numeracion operadorDos;
        Numeracion resultado;
        Operacion calculadora;
        Numeracion.ESistema sistema;


        public FrmCalculadora()
        {
            InitializeComponent();
        }

        private void FrmCalculadora_Load(object sender, EventArgs e)
        {
            List<string> operadores = new List<string>() { "+", "-", "*", "/" };

            //Le agrego la lista de operadores
            this.cmbTipoOperacion.DataSource = operadores;
            //con selectIndex le indico en que posicion debe empezar
            this.cmbTipoOperacion.SelectedIndex = 0;

            //Selecciono de forma predeterminada un radioButton
            this.rdbDecimal.Checked = true;

            //BORRO EL TEXTO RESULTADO PARA QUE NO SE VEA EL 0
            this.lblResultado.Text = "";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.txbPrimerOperando.Clear();
            this.txbSegundoOperando.Clear();
            this.resultado = null;
            this.operadorUno = null;
            this.operadorDos = null;
            this.lblResultado.Text = "";
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Estas seguro ? ", "Cerrar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Instancia la clase numeracion cada vez que se presiona una tecla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbPrimerOperando_TextChanged(object sender, EventArgs e)
        {
            operadorUno = new Numeracion(this.txbPrimerOperando.Text, Numeracion.ESistema.Decimal);

        }

        private void txbSegundoOperando_TextChanged(object sender, EventArgs e)
        {
            operadorDos = new Numeracion(this.txbSegundoOperando.Text, Numeracion.ESistema.Decimal);
        }

        /// <summary>
        /// Asigno el sistema que se visualizara el resultado al ser seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbDecimal_CheckedChanged(object sender, EventArgs e)
        {
            sistema = Numeracion.ESistema.Decimal;

            SetResultado();
        }

        private void rdbBinario_CheckedChanged(object sender, EventArgs e)
        {
            sistema = Numeracion.ESistema.Binario;

            SetResultado();
        }

        //PERMITO QUE SOLO SE INGRESEN NUMEROS
        private void txbPrimerOperando_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                //SI NO ES NI UN NUMERO O UN CONTROL ESPECIAL EJEMPLO BORRAR, NO PERMITO QUE ESCRIBA
                e.Handled = true; //true es para cancelar 
            }
        }

        private void txbSegundoOperando_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                //SI NO ES NI UN NUMERO O UN CONTROL ESPECIAL EJEMPLO BORRAR, NO PERMITO QUE ESCRIBA
                e.Handled = true; //true es para cancelar 
            }
        }

        //REVISAR ESTE APARTADO
        private void SetResultado()
        {
            if (resultado is not null)
            {
                if (sistema != Numeracion.ESistema.Decimal && operadorDos.ValorNumerico != "0")
                {
                    this.lblResultado.Text = resultado.ConvertirA(this.sistema);
                }
                else
                {
                    this.lblResultado.Text = resultado.ValorNumerico;
                }
            }


        }



        private void btnOperar_Click(object sender, EventArgs e)
        {
            if (operadorUno is not null && operadorDos is not null)
            {
                calculadora = new Operacion(operadorUno, operadorDos);

                //Resultado es del tipo numeracion
                resultado = calculadora.Operar(this.cmbTipoOperacion.Text[0]);

                //AL INDICAR LA POSICION DE UN STIRNG LO ESTARIA LEYENDO COMO UN CHAR
                this.lblResultado.Text = resultado.ValorNumerico;

                SetResultado();

            }

        }

        
    }
}