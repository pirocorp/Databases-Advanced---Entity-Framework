namespace PetStore.Services.Implementations
{
    using Data;
    using Data.Models;

    public class OrderService : IOrderService
    {
        private readonly PetStoreDbContext _data;

        public OrderService(PetStoreDbContext data)
        {
            this._data = data;
        }

        public void CompleteOrder(int orderId)
        {
            var order = this._data.Orders.Find(orderId);

            order.Status = OrderStatus.Done;
            this._data.SaveChanges();
        }
    }
}
