using Business.Tools.Exceptions;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;
using Entities.DTOs;
using Entities.Models;


namespace Business.Validations;

public class OrderValidations:BaseValidation
{
    private readonly IProductTransactionRepository _productTransactionRepository;
    public OrderValidations(IProductTransactionRepository productTransactionRepository)
    {
        _productTransactionRepository = productTransactionRepository;
    }

    public async Task CheckProductListCount(AddOrderDto addOrderDto)
    {
        if(addOrderDto.ProductTransactions.Where(t => t.Quantity == 0).Any())
        {
            throw new ValidationException("Product count can't be zero.");
        }
        await Task.CompletedTask;
    }

    public async Task CheckTransactionCount(AddOrderDto addOrderDto)
    {
        if(addOrderDto.ProductTransactions.Count() == 0)
        {
            throw new ValidationException("Product list can not be empty");
        }
        await Task.CompletedTask;
    }

    public async Task OrderMustNotBeEmpty(Order? order)
    {
        if (order == null)
        {
            throw new ValidationException("Order must not be empty.", 500);
        }
        await Task.CompletedTask;
    }

    public async Task CheckStock(AddOrderDto addOrderDto)
    {
        var checkCounts = addOrderDto.ProductTransactions.Select(t =>
        _productTransactionRepository.GetAll(pt => pt.ProductId == t.ProductId)
        .Sum(transaction => transaction.Quantity) - t.Quantity)
            .Where(q => q < 0).Any();
        if (checkCounts)
        {
            throw new ValidationException("We have not any product stock");
        }
    }
}

public class AddOrderValidations : OrderValidations
{
    public AddOrderValidations(IProductTransactionRepository productTransactionRepository) : base(productTransactionRepository)
    {
    }

}

public class DeleteOrderValidations : BaseValidation
{
    protected readonly IOrderRepository _orderRepository;
    public DeleteOrderValidations(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task OrderNotFound(Guid id)
    {
        var dbOrder = await _orderRepository.GetAsync(u => u.Id == id);
        if (dbOrder == null)
        {
            throw new ValidationException("Order not found.", 400);
        }
        await Task.CompletedTask;
    }
}
