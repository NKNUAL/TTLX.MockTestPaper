﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Controller
{
    public class CacheController
    {
        #region single
        private CacheController(string specialtyId)
        {
            _specialtyId = specialtyId;
        }
        private static Dictionary<string, CacheController> _instance = new Dictionary<string, CacheController>();
        public static CacheController Instance(string specialtyId)
        {
            if (_instance.ContainsKey(specialtyId))
            {
                if (_instance[specialtyId] == null)
                {
                    _instance[specialtyId] = new CacheController(specialtyId);
                }
            }
            else
            {
                _instance.Add(specialtyId, new CacheController(specialtyId));
            }
            return _instance[specialtyId];
        }
        #endregion

        private string _specialtyId;

        /// <summary>
        /// 科目缓存
        /// </summary>
        private Dictionary<string, string> _dicCourse = new Dictionary<string, string>();

        /// <summary>
        /// 知识点缓存
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> _dicKnow =
            new Dictionary<string, Dictionary<string, string>>();


        public Dictionary<string, string> GetCourse()
        {
            return _dicCourse.Count == 0 ? null : _dicCourse;
        }

        public void SetCourse(Dictionary<string, string> dicCourse)
        {
            this._dicCourse = dicCourse;
        }

        public Dictionary<string, string> GetKnows(string courseNo)
        {
            if (_dicKnow.ContainsKey(courseNo))
            {
                return _dicKnow[courseNo];
            }
            return null;
        }

        public void SetKnow(string courseNo, Dictionary<string, string> dicKnows)
        {
            if (!_dicKnow.ContainsKey(courseNo))
            {
                _dicKnow.Add(courseNo, dicKnows);
            }
        }
    }
}
