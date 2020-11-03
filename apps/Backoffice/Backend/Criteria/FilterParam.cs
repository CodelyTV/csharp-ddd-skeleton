using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CodelyTv.Apps.Backoffice.Backend.Criteria
{
    public class FiltersParam
    {
        [FromQuery(Name = "filters")]
        public List<Dictionary<string, string>> Filters { get; set; }

        [FromQuery(Name = "order_by")]
        public string OrderBy { get; set; }

        public string Order { get; set; }

        public int? Limit { get; set; }

        public int? Offset { get; set; }
    }
}
