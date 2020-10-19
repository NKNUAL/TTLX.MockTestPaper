using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Controller.Model
{
    public class UploadLocalRecord
    {
        public string UserId { get; set; }
        public int SpecialtyId { get; set; }
        public List<EditPaperRecord> Papers { get; set; }
    }
}
