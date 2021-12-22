using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TesteBalancaAlfa
{
    public partial class frmHexaDecimal : Form
    {
        public delegate void AlterarCheckBox();
        private AlterarCheckBox _closeCheckBox;
        private Form1 parentForm;
        //Numero máximo de linhas do log.
        private const int _MaxLines = 256;
        private List<string> strList = new List<string>();

        public frmHexaDecimal()
        {
            InitializeComponent();
            this.BringToFront();
            
        }
        public frmHexaDecimal(Form1 frm) : this()
        {
            try
            {
                parentForm = frm;
                parentForm.onReceiveData += Frm_onReceiveData;
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }

        }

        private void Frm_onReceiveData(string text)
        {
            try
            {
                 AdicionaTexto(text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void AdicionaTexto(string texto)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke((MethodInvoker)(() => AdicionaTexto(texto)));
            }
            else
            {
                strList.Insert(0, texto);
                TrimLog(strList);
                richTextBox1.Lines = strList.ToArray();

               // richTextBox1.AppendText(string.Format("{0} ", texto));
               // richTextBox1.ScrollToCaret();
            }
        }

     


        private void frmHexaDecimal_Load(object sender, EventArgs e)
        {

        }

        private void frmHexaDecimal_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.onReceiveData -= Frm_onReceiveData;
        }



        private void TrimLog<T>(List<T> _logList)
        {
            try
            {
                if (_logList.Count <= _MaxLines) return;
                _logList.RemoveRange(_MaxLines, _logList.Count - _MaxLines);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
