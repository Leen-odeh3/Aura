﻿namespace Aura.Domain.DTOs.User;
public class UsersWithPaginationResponseDto
{
    public IEnumerable<UserResponseDto> users { get; set; } = new List<UserResponseDto>();
    public int numOfPages { get; set; }
    public int currentPage { get; set; }
}