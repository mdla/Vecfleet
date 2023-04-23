using Vecfleet.Application.Interfaces.Common;

namespace Vecfleet.Application.Interfaces.Response;

public interface IResponse
{
    public Result Result { get; set; }
}