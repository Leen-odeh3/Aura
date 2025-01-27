﻿using Aura.Application.Abstracts.UserServices;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.User;
using Aura.Domain.Exceptions;
using AutoMapper;

namespace Aura.Application.Services.UserServices;
public class UserRetrievalService : IUserRetrievalService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UserRetrievalService(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<UsersWithPaginationResponseDto> GetUsers(
        int pageNumber,
        int pageSize,
        string searchText = null)
    {
        if (searchText != null)
        {
            searchText = searchText.Trim().ToLower();
        }
        var result = await userRepository.GetUsers(pageNumber, pageSize, searchText);

        var response = new UsersWithPaginationResponseDto
        {
            users = mapper.Map<IEnumerable<UserResponseDto>>(result.Item1),
            numOfPages = result.Item2,
            currentPage = pageNumber
        };
        return response;
    }

    public async Task<UserResponseDto> GetUserById(int userId)
    {
        var user = await userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
        }
        return mapper.Map<UserResponseDto>(user);
    }
}
