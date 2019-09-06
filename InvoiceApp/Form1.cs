using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace InvoiceApp
{
    public partial class Form1 : Form
    {
        private int rowIndex = 0;       
        public static int addInvoice;
        public static int selectedId;
        public static string editClient;
        public static string editDate;
        public static int editInvoice;

        private int summaryInvoice = 0;

        DataTable datatable;

        public Form1()
        {
            InitializeComponent();
            this.dateDataGridViewTextBoxColumn.DefaultCellStyle.Format = "MM/dd/yyyy";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Invoice App";                    
           
            datatable = new DataTable();
            datatable = this.invoiceDataSet.Invoices;
            this.invoicesTableAdapter.Fill(this.invoiceDataSet.Invoices);
            dataGridView1.DataSource = datatable;

        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if(ClientNameInput.Text.Length < 1)
            {
                MessageBox.Show("You must enter a Client Name.",
                    "Invalid Name", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ClientNameInput.Focus();
            }
            else if (InvoiceInput.Text.Length < 1)
            {
                MessageBox.Show("You must enter an Invoice value.",
                    "Invalid Name", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                InvoiceInput.Focus();
            }
            else if (!int.TryParse(InvoiceInput.Text.ToString(), out addInvoice))
            {               
                MessageBox.Show("You must enter an Invoice value is not a valid.",
                    "Invalid Name", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                InvoiceInput.Focus();
            }

            else
            {
                using (var context = new MyDbContext())
                {
                    var newInvoice = new Invoice
                    {
                        date = dateTimePicker1.Value,
                        client = ClientNameInput.Text,
                        invoice_amount = Int32.Parse(InvoiceInput.Text)
                    };

                    context.Invoice.Add(newInvoice);
                    context.SaveChanges();
                    this.invoicesTableAdapter.Fill(this.invoiceDataSet.Invoices);
                }
            }
        }

        private void DataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {         
                if (e.Button == MouseButtons.Right)
                {
                    this.dataGridView1.Rows[e.RowIndex].Selected = true;
                    this.rowIndex = e.RowIndex;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
                    this.contextMenuStrip1.Show(this.dataGridView1, e.Location);

                    selectedId = Convert.ToInt32(this.dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                    editDate = (this.dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value).ToString();
                    editClient = (this.dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value).ToString();                
                    editInvoice = Convert.ToInt32(this.dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value);
                    contextMenuStrip1.Show(Cursor.Position);
                }          
        }

        private void contextDeleteMenu_Click(object sender, EventArgs e)
        {
            using (var context = new MyDbContext())
            {
                if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
                {
                    Invoice newInvoice = new Invoice { Id = selectedId };
                    context.Invoice.Attach(newInvoice);
                    context.Invoice.Remove(newInvoice);
                    context.SaveChanges();
                    this.dataGridView1.Rows.RemoveAt(this.rowIndex);

                }
            }
        }

        private void contextEditMenu_Click(object sender, EventArgs e)
        {

            Form2 dialog = new Form2();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.invoicesTableAdapter.Fill(this.invoiceDataSet.Invoices);              
            }
        }


        private void summaryInvoiceCount(object sender, DataGridViewCellPaintingEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    summaryInvoice += Convert.ToInt32(this.dataGridView1[3, i].Value);
                }
                summaryText.Text = "Summary: " + summaryInvoice.ToString();
                summaryInvoice = 0;

            }
        }

        private void searchTextBox_Changed(object sender, EventArgs e)
        {
            DataView DV = new DataView(datatable);
            DV.RowFilter = string.Format("client LIKE '%{0}%'", searchTextBox.Text);
            dataGridView1.DataSource = DV;
        }
    }
}
