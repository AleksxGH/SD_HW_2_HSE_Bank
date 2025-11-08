namespace Domain.Entities.Interfaces
{
    public interface IVisitable
    {
        public void Accept(IVisitor visitor);

    }
}
