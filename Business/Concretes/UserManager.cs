using System;
using Business.Abstracts;
using Business.Validations;
using DataAccess.Abstracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Security;
using Core.Aspects.Autofac.Transaction;

namespace Business.Concretes;

public class UserManager : IUserService
{
    public readonly IUserRepository _userRepository;
    private readonly UserValidations _userValidations;
    public UserManager(IUserRepository userRepository,UserValidations userValidations)
    {
        _userRepository = userRepository;
        _userValidations = userValidations;
    }

    [TransactionScopeAspect]
    [ValidationAspect(typeof(AddUserValidations))]
    [CacheRemoveAspect("Business.Abstracts.IUserService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "User added.")]
    [SecurityAspect]
    public User Add(User user)
    {
        return _userRepository.Add(user);
    }

    [TransactionScopeAspect]
    [ValidationAspect(typeof(AddUserValidations))]
    [CacheRemoveAspect("Business.Abstracts.IUserService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "User added.")]
    [SecurityAspect]
    public async Task<User> AddAsync(User user)
    {
        return await _userRepository.AddAsync(user);
    }

    [CacheRemoveAspect("Business.Abstracts.IUserService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteValidations))]
    [DebugWriteSuccessAspect(Message = "User deleted.")]
    [SecurityAspect]
    public void DeleteById(Guid id)
    {
        var user=_userRepository.Get(u=>u.Id==id);
        _userValidations.UserMustNotBeEmpty(user).Wait();
        _userRepository.Delete(user);
    }

    [CacheRemoveAspect("Business.Abstracts.IUserService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteValidations))]
    [DebugWriteSuccessAspect(Message = "User deleted.")]
    [SecurityAspect]
    public async Task DeleteByIdAsync(Guid id)
    {
        var user = _userRepository.Get(u => u.Id == id);
        await _userValidations.UserMustNotBeEmpty(user);
        await _userRepository.DeleteAsync(user);
    }

    [CacheAspect(1)]
    [DebugWriteSuccessAspect(Message = "Kullanıcı listeleme tamamlandı")]
    [SecurityAspect]
    public IList<User> GetAll()
    {
        return _userRepository.GetAll().ToList();
    }

    [CacheAspect(1)]
    [DebugWriteSuccessAspect(Message = "Kullanıcı listeleme tamamlandı")]
    [SecurityAspect]
    public async Task<IList<User>> GetAllAsync()
    {
        var result= await _userRepository.GetAllAsync();
        return result.ToList();
    }

    [SecurityAspect]
    public IList<User> GetAllByBirthDate(short birthDate)
    {
        return _userRepository.GetAll(u=>u.BirthYear==birthDate).ToList();
    }

    [SecurityAspect]
    public IList<User> GetAllByBirthDateGratherThan(short birthDate)
    {
        return _userRepository.GetAll(u => u.BirthYear > birthDate).ToList();
    }

    [SecurityAspect]
    public IList<User> GetAllByBirthDateLessThan(short birthDate)
    {
        return _userRepository.GetAll(u => u.BirthYear < birthDate).ToList();

    }

    [SecurityAspect]
    public IList<User> GetAllByFirstName(string firstName)
    {
        return _userRepository.GetAll(u => u.FirstName == firstName).ToList();

    }

    [SecurityAspect]
    public IList<User> GetAllByFirstNameContains(string firstName)
    {
        return _userRepository.GetAll(u => u.FirstName.Contains(firstName)).ToList();
    }

    [SecurityAspect]
    public IList<User> GetAllByLastName(string lastName)
    {
        return _userRepository.GetAll(u => u.LastName == lastName).ToList();
    }

    [SecurityAspect]
    public User? GetById(Guid id)
    {
        return _userRepository.Get(u=>u.Id==id);
    }

    [SecurityAspect]
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetAsync(u => u.Id == id);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IUserService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "User updated.")]
    [SecurityAspect]
    public User Update(User user)
    {
        return _userRepository.Update(user);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IUserService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "User updated.")]
    [SecurityAspect]
    public async Task<User> UpdateAsync(User user)
    {
       return await _userRepository.UpdateAsync(user);
    }

    [SecurityAspect]
    public User? GetByUserNameWithClaims(string userName)
    {
       return _userRepository.Get(u => u.UserName == userName, include: user => user.Include(u => u.UserClaims).ThenInclude(uc => uc.Claim));
    }

    [SecurityAspect]
    public async Task<User?> GetByUserNameWithClaimsAsync(string userName)
    {
        return await _userRepository.GetAsync(u => u.UserName == userName, include: user => user.Include(u => u.UserClaims).ThenInclude(uc => uc.Claim));
    }
}
