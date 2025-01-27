﻿using Aura.Application.Abstracts.UserServices;
using Aura.Application.Helper;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;
using Aura.Domain.Exceptions;
using AutoMapper;

namespace Aura.Application.Services.UserServices;
public class UserAccountService : IUserAccountService
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IAuthenticatedUserService authenticatedUserService;

    public UserAccountService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticatedUserService authenticatedUserService)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.authenticatedUserService = authenticatedUserService;
    }

    public async Task<List<UserResponseDto>> GetFollowersAsync(int userId)
    {
        var followers = await userRepository.GetFollowersAsync(userId);

        var followersDto = followers.Select(f => mapper.Map<UserResponseDto>(f)).ToList();
        return followersDto;
    }
    public async Task<UserResponseDto> RegisterUserAsync(UserRequestDto userRequestDto)
    {
        if (userRepository.CheckIfUsernameExists(userRequestDto.Username))
        {
            throw new NotFoundException(UserExceptionMessages.UsernameAlreadyExsist);
        }
        PasswordHashing.HashPassword(userRequestDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = new User()
        {
            Username = userRequestDto.Username.ToLower(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };
        await userRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<UserResponseDto>(user);
    }

    public async Task<object> LoginUserAsync(UserRequestDto userRequestDto)
    {
        var user = await userRepository.GetUserByUsername(userRequestDto.Username.ToLower());

        if (user == null)
        {
            throw new NotFoundException(UserExceptionMessages.NotFoundUserByUsername);
        }

        if (!PasswordHashing.VerifyPassword(userRequestDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new BadRequestException(UserExceptionMessages.IncorrectPassword);
        }

        var token = TokenGenerator.Generate(user);
        return new
        {
            Token = token,
            UserId = user.Id
        };
    }

    public async Task<UserResponseDto> GetUserByJwtTokenAsync()
    {
        var userId = authenticatedUserService.GetAuthenticatedUserId();
        var user = await userRepository.GetUserById(userId);
        return mapper.Map<UserResponseDto>(user);
    }

    public async Task ChangePasswordAsync(int userId, ChangePasswordRequestDto changePasswordDto)
    {
        var authenticatedUserId = authenticatedUserService.GetAuthenticatedUserId();
        if (authenticatedUserId != userId)
        {
            throw new UnauthorizedException();
        }
        var user = await userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
        }
        if (!PasswordHashing.VerifyPassword(changePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
        {
            throw new BadRequestException(UserExceptionMessages.IncorrectPassword);
        }
        PasswordHashing.HashPassword(changePasswordDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await unitOfWork.SaveChangesAsync();
    }

    public async Task ChangeUserAboutAsync(int userId, string newAbout)
    {
        var authenticatedUserId = authenticatedUserService.GetAuthenticatedUserId();
        if (authenticatedUserId != userId)
        {
            throw new UnauthorizedException();
        }

        var user = await userRepository.GetUserById(userId);
        if (user is null)
        {
            throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
        }

        user.About = newAbout;  

        await unitOfWork.SaveChangesAsync();
    }

}