using System.Collections.Generic;

namespace SparkPost
{
    public class GetMetricsResponse: Response
    {
        public IList<IDictionary<string, object>> Results { get; set; }

        public GetMetricsResponse()
        {
            Results = new List<IDictionary<string, object>>();
        }

        public GetMetricsResponse(Response source)
        {
            this.SetFrom(source);
        }
    }
}
