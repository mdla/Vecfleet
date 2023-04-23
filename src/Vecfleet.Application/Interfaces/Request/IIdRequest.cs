namespace Vecfleet.Application.Interfaces.Request;

public interface IIdRequest<T>
{
    public T Id { get; set; }
}