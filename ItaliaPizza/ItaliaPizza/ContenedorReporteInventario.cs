using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItaliaPizza
{
    public partial class ContenedorReporteInventario : Form
    {
        public ContenedorReporteInventario()
        {
            InitializeComponent();
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                ReporteInventario reporte = new ReporteInventario();
                reporte.Refresh();
                reporte.Load(Application.StartupPath + "\\ReporteInventario.rpt");
                reporte.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                reporte.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);
            }
            this.Close();
        }
    }
}
