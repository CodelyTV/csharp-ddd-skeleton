using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CodelyTv.Apps.Backoffice.Backend.Criteria
{
    public class FiltersParam
    {
        [FromQuery(Name = "filters")]
        public List<Dictionary<string, string>> Filters { get; set; } = new List<Dictionary<string, string>>();

        [FromQuery(Name = "order_by")]
        public string OrderBy { get; set; } = string.Empty;

        public string Order { get; set; } = string.Empty;

        public int? Limit { get; set; }

        public int? Offset { get; set; }
    }
}
