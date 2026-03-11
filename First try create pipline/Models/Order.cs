using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public double Amount { get; set; }

        public override string ToString()
        {
            return $"Id => {Id} \nCustomer Name => {Customer} \nAmount of order => {Amount}\n\n";
        }

    }
}
