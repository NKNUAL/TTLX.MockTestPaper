using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.DataBase.Entity
{
    [Table("PaperRecord")]
    public class PaperRecord
    {
        [Key]
        public string PGuid { get; set; }
        public string PaperEditDate { get; set; }
        public string RuleNo { get; set; }
        public bool IsNormal { get; set; }
    }

}
