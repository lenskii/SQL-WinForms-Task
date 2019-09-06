using System;
using System.Windows.Forms;

namespace InvoiceApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using (var context = new MyDbContext())
            {
                this.Text = "Database record editing";

                clientNameBox.Text = Form1.editClient;
                invoiceAmountBox.Text = Form1.editInvoice.ToString();
                dateTimePicker.Value = DateTime.Parse(Form1.editDate);             
            }
        }

        private void buttonOk_Click(object sender, MouseEventArgs e)
        {
            if (clientNameBox.Text.Length < 1)
            {
                MessageBox.Show("You must enter a First Name.",
                    "Invalid Name", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                clientNameBox.Focus();
            }            
            else
            {
                // It's ok. Close the dialog.
                using (var context = new MyDbContext())
                {
                    //Invoice invoice = context.Invoice.Find(Form1.selectedId);

                    Invoice oldInvoice = new Invoice { Id = Form1.selectedId };
                    context.Invoice.Attach(oldInvoice);

                    Invoice editedInvoice = new Invoice {
                        Id = Form1.selectedId,
                        client = clientNameBox.Text,
                        date = dateTimePicker.Value,
                        invoice_amount = Int32.Parse(invoiceAmountBox.Text)
                    };


                    
                    context.Entry(oldInvoice).CurrentValues.SetValues(editedInvoice);
                    context.SaveChanges();

                    //Invoice newInvoice = new Invoice { Id = Form1.selectedId };
                    //context.Entry(newInvoice).CurrentValues.SetValues(editedInvoice);
                    //context.Invoice.Attach(newInvoice);
                    //context.Invoice.Remove(newInvoice);
                    //context.SaveChanges();
                    //context.Invoice.Attach(editedInvoice);                    
                    //context.SaveChanges();

                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
