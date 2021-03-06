using Microsoft.AspNet.Routing;

namespace CommandRouting.Router.ValueParsers
{
    public interface IValueParser
    {
        void ParseValues(RouteData routeData, object requestModel);
    }
}