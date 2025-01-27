﻿using Aura.Application.Abstracts;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Comment;
using Aura.Domain.Entities;

namespace Aura.Application.Services;
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(int postId)
    {
        return await _commentRepository.GetCommentsByPostIdAsync(postId);
    }

    public async Task<Comment> AddCommentAsync(int postId, PostComment comment, int userId)
    {
        return await _commentRepository.AddCommentAsync(postId, comment, userId);
    }


    public async Task<Comment> RemoveCommentAsync(int commentId)
    {
        return await _commentRepository.RemoveCommentAsync(commentId);
    }
}
