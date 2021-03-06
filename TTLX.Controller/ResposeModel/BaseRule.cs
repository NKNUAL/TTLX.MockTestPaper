﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Controller.ResposeModel
{
    public class BaseRule
    {
        public int SpecialtyId { get; set; }
        public int DanxuanCount { get; set; }
        public int DuoxuanCount { get; set; }
        public int PanduanCount { get; set; }
        public List<CourseBaseRule> CourseRules { get; set; }
    }

    public class CourseBaseRule
    {
        public string CourseNo { get; set; }
        public int DanxuanCount { get; set; }
        public int DuoxuanCount { get; set; }
        public int PanduanCount { get; set; }
    }
}
