using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tableId { get; set; }
        public string tableName { get; set; }
        public int noOfSeats { get; set; } = 0;
    }
}
