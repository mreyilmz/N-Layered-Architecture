using Business.Abstracts;
using Business.Validations;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Security;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstracts;
using Entities.DTOs;
using Entities.Models;


namespace Business.Concretes;

public class OrderManager : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderValidations _orderValidations;
    private readonly IProductTransactionService _productTransactionService;
    private readonly IOrderDetailService _orderDetailService;

    public OrderManager(
        IOrderRepository orderRepository, 
        OrderValidations orderValidations, 
        IProductTransactionService productTransactionService,
        IOrderDetailService orderDetailService
        )
    {
        _orderRepository = orderRepository;
        _orderValidations = orderValidations;
        _productTransactionService = productTransactionService;
        _orderDetailService = orderDetailService;
    }

    [ValidationAspect(typeof(AddOrderValidations))]
    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IOrderService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Order added.")]
    [SecurityAspect]
    public Order Add(AddOrderDto addOrderDto)
    {

        var addedOrder = _orderRepository.Add(new()
        {
            UserId = addOrderDto.UserId,
            CreatedDate = DateTime.UtcNow
        });

        addOrderDto.ProductTransactions.ToList().ForEach(productTransaction =>
        {
            productTransaction.Quantity = productTransaction.Quantity > 0 ? -1 * productTransaction.Quantity : productTransaction.Quantity;
            var addedProductTransaction = _productTransactionService.Add(productTransaction);
            _orderDetailService.Add(new()
            {
                OrderId = addedOrder.Id,
                ProductId = productTransaction.ProductId,
                ProductTransactionId = addedProductTransaction.Id
            });
        });
        return addedOrder;
    }

    [ValidationAspect(typeof(AddOrderValidations))]
    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IOrderService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Order added.")]
    [SecurityAspect]
    public async Task<Order> AddAsync(AddOrderDto addOrderDto)
    {
        await _orderValidations.CheckProductListCount(addOrderDto);
        await _orderValidations.CheckTransactionCount(addOrderDto);
        await _orderValidations.CheckStock(addOrderDto);

        var addedOrder = await _orderRepository.AddAsync(new()
        {
            UserId = addOrderDto.UserId,
            CreatedDate = DateTime.UtcNow
        });

        addOrderDto.ProductTransactions.ToList().ForEach(productTransaction =>
        {
            productTransaction.Quantity = productTransaction.Quantity > 0 ? -1 * productTransaction.Quantity : productTransaction.Quantity;
            var addedProductTransaction = _productTransactionService.Add(productTransaction);
            _orderDetailService.Add(new()
            {
                OrderId = addedOrder.Id,
                ProductId = productTransaction.ProductId,
                ProductTransactionId = addedProductTransaction.Id
            });
        });
        return addedOrder;
    }

    [CacheRemoveAspect("Business.Abstracts.IOrderService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteOrderValidations))]
    [DebugWriteSuccessAspect(Message = "Order deleted.")]
    [SecurityAspect]
    public void DeleteById(Guid id)
    {
        var order = _orderRepository.Get(o => o.Id == id);
        _orderValidations.OrderMustNotBeEmpty(order).Wait();
        _orderRepository.Delete(order);
    }

    [CacheRemoveAspect("Business.Abstracts.IOrderService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteOrderValidations))]
    [DebugWriteSuccessAspect(Message = "Order deleted.")]
    [SecurityAspect]
    public async Task DeleteByIdAsync(Guid id)
    {
        var order = _orderRepository.Get(o => o.Id == id);
        await _orderRepository.DeleteAsync(order);
    }

    [CacheAspect(1)]
    [DebugWriteSuccessAspect(Message = "Order listing completed.")]
    [SecurityAspect]
    public IList<Order> GetAll()
    {
        return _orderRepository.GetAll().ToList();
    }

    [CacheAspect(1)]
    [DebugWriteSuccessAspect(Message = "Order listing completed.")]
    [SecurityAspect]
    public async Task<IList<Order>> GetAllAsync()
    {
        var result = await _orderRepository.GetAllAsync();
        return result.ToList();
    }

    [SecurityAspect]
    public Order? GetById(Guid id)
    {
        return _orderRepository.Get(o => o.Id == id);
    }

    [SecurityAspect]
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _orderRepository.GetAsync(o => o.Id == id);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IOrderService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Order updated.")]
    [SecurityAspect]
    public Order Update(Order order)
    {
        return _orderRepository.Update(order);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IOrderService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Order updated.")]
    [SecurityAspect]
    public async Task<Order> UpdateAsync(Order order)
    {
        return await _orderRepository.UpdateAsync(order);
    }
}
