using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class DiningSpace
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int diningSpaceId { get; set; }
        public string diningSpaceName { get; set; }

    }
}
