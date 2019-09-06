using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string client { get; set; }
        public int invoice_amount { get; set; }

    }
}
