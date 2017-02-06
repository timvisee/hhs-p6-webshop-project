using System.Collections.Generic;

namespace SparkPost
{
    public class GetMetricsResourceResponse: Response
    {
        public IList<string> Results { get; set; }

        public GetMetricsResourceResponse()
        {
            Results = new List<string>();
        }        

        public GetMetricsResourceResponse(Response source)
        {
            this.SetFrom(source);
        }
    }
}
