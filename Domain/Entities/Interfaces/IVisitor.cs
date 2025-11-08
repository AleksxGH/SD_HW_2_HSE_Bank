namespace Domain.Entities.Interfaces
{
    public interface IVisitor
    {
        public void Visit(IBankAccount bankAccount);
        public void Visit(ICategory category);
        public void Visit(IOperation operation);
    }
}
