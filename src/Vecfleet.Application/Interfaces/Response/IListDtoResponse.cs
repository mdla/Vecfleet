using Vecfleet.Application.Interfaces.Common;

namespace Vecfleet.Application.Interfaces.Response;

public interface IListDtoResponse<T>: IResponse, IData<List<T>>
{
    
}