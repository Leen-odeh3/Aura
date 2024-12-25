using Aura.Application.Abstracts;
using Aura.Application.Abstracts.UserServices;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Follow;
using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;
using Aura.Domain.Exceptions;
using AutoMapper;

namespace Aura.Application.Services;
public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public FollowService(IFollowRepository followRepository, IUserRepository userRepository, IAuthenticatedUserService authenticatedUserService)
    {
        _followRepository = followRepository;
        _userRepository = userRepository;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<FollowResponseDto> FollowUserAsync(FollowRequestDto followRequestDto)
    {
        var authenticatedUserId = _authenticatedUserService.GetAuthenticatedUserId();

        if (authenticatedUserId == followRequestDto.FollowedId)
        {
            throw new BadRequestException("You cannot follow yourself.");
        }

        var isAlreadyFollowing = await _followRepository.IsFollowingAsync(authenticatedUserId, followRequestDto.FollowedId);

        if (isAlreadyFollowing)
        {
            throw new BadRequestException("You are already following this user.");
        }

        await _followRepository.FollowUserAsync(authenticatedUserId, followRequestDto.FollowedId);

        return new FollowResponseDto
        {
            FollowerId = authenticatedUserId,
            FollowedId = followRequestDto.FollowedId,
            DateFollowed = DateTime.UtcNow
        };
    }
}