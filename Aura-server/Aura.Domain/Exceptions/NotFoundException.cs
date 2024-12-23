namespace Aura.Domain.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string massege) : base(massege) { }
}