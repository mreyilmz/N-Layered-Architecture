using Business.Abstracts;
using Business.Validations;
using DataAccess.Abstracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes;

public class OrderDetailManager :IOrderDetailService
{
    public readonly IOrderDetailRepository _orderDetailRepository;
    public readonly OrderDetailValidations _orderDetailValidations;

    public OrderDetailManager(IOrderDetailRepository orderDetailRepository, OrderDetailValidations orderDetailValidations)
    {
        _orderDetailRepository = orderDetailRepository;
        _orderDetailValidations = orderDetailValidations;
    }
    public OrderDetail Add(OrderDetail orderDetail)
    {
        return _orderDetailRepository.Add(orderDetail);
    }

    public async Task<OrderDetail> AddAsync(OrderDetail orderDetail)
    {
        return await _orderDetailRepository.AddAsync(orderDetail);
    }

    public void DeleteById(Guid id)
    {
        var orderDetail = _orderDetailRepository.Get(od => od.Id == id);
        _orderDetailValidations.OrderDetailMustNotBeEmpty(orderDetail).Wait();
        _orderDetailRepository.Delete(orderDetail);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var orderDetail = _orderDetailRepository.Get(od => od.Id == id);
        await _orderDetailValidations.OrderDetailMustNotBeEmpty(orderDetail);
        await _orderDetailRepository.DeleteAsync(orderDetail);
    }

    public IList<OrderDetail> GetAll()
    {
        return _orderDetailRepository.GetAll().ToList();
    }

    public async Task<IList<OrderDetail>> GetAllAsync()
    {
        var result = await _orderDetailRepository.GetAllAsync();
        return result.ToList();
    }

    public OrderDetail? GetById(Guid id)
    {
        return _orderDetailRepository.Get(od => od.Id == id);
    }

    public async Task<OrderDetail?> GetByIdAsync(Guid id)
    {
        return await _orderDetailRepository.GetAsync(od => od.Id == id);
    }

    public OrderDetail Update(OrderDetail orderDetail)
    {
        return _orderDetailRepository.Update(orderDetail);
    }

    public async Task<OrderDetail> UpdateAsync(OrderDetail orderDetail)
    {
        return await _orderDetailRepository.UpdateAsync(orderDetail);
    }
}
