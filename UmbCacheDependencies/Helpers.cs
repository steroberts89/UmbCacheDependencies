using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbCacheDependencies
{
    static class Helpers
    {
        /// <summary>
        /// Simple method to convert a csv of id's to a list of ints
        /// </summary>
        /// <param name="csv">e.g. 1234,1106,1058</param>
        /// <returns></returns>
        internal static List<int> ConvertCsvToIntArray(string csv)
        {
            var items = csv.Split(',');
            var ids = new List<int>();
            foreach (var idStr in items)
            {
                int id;
                bool worked = int.TryParse(idStr, out id);
                if (worked)
                {
                    ids.Add(id);
                }
            }
            return ids;

        }
    }
}