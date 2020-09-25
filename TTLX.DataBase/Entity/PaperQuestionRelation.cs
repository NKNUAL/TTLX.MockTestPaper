using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.DataBase.Entity
{
    [Table("PaperQuestionRelation")]
    public class PaperQuestionRelation
    {
        [Key]
        public string RGuid { get; set; }
        public string PGuid { get; set; }
        public string QGuid { get; set; }
    }
}
