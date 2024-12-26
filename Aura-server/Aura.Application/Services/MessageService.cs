using Aura.Application.Abstracts.UserServices;
using Aura.Application.Abstracts;
using Aura.Domain.Contracts;
using Aura.Domain.Exceptions;
using AutoMapper;
using Aura.Domain.DTOs.Message;
using Aura.Domain.Entities;

namespace Aura.Application.Services;
public class MessageService : IMessageService
{
    private readonly IMessageRepository privateMessageRepository;
    private readonly IUserRepository userRepository;
    private readonly IAuthenticatedUserService authenticatedUserService;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public MessageService(
        IMessageRepository privateMessageRepository,
        IUnitOfWork unitOfWork,
        IAuthenticatedUserService authenticatedUserService,
        IMapper mapper,
        IUserRepository userRepository)
    {
        this.privateMessageRepository = privateMessageRepository;
        this.unitOfWork = unitOfWork;
        this.authenticatedUserService = authenticatedUserService;
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<MessageResponseDto> StorePrivateMessage(int destinationUserId, string textMessage)
    {
        var sourceUserId = authenticatedUserService.GetAuthenticatedUserId();
        var destinationUser = await userRepository.GetUserById(destinationUserId);
        if (destinationUser == null)
        {
            throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
        }
        var message = new Message()
        {
            ReceiverId = destinationUserId,
            SenderId = sourceUserId,
            CreationDate = DateTime.Now,
            TextBody = textMessage
        };
        await privateMessageRepository.AddAsync(message);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<MessageResponseDto>(message);
    }

    public async Task<MessagesWithPaginationResponseDto> GetPrivateMessages(
        DateTime? pageDate,
        int pageSize,
        int firstUserId,
        int secoundUserId)
    {
        var firstUser = await userRepository.GetUserById(firstUserId);
        var secoundUser = await userRepository.GetUserById(secoundUserId);
        if (firstUser == null || secoundUser == null)
        {
            throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
        }
        var authenticatedUserId = authenticatedUserService.GetAuthenticatedUserId();
        if (authenticatedUserId != firstUserId)
        {
            throw new UnauthorizedException();
        }
        await privateMessageRepository.GetRecentChatsForUser(authenticatedUserId);
        if (pageDate == null)
        {
            pageDate = DateTime.Now;
        }
        var queryResult = await privateMessageRepository.GetPrivateMessagesForPrivateChat((DateTime)pageDate, pageSize, firstUserId, secoundUserId);
        var result = new MessagesWithPaginationResponseDto
        {
            Messages = mapper.Map<IEnumerable<MessageResponseDto>>(queryResult.Item1),
            IsThereMore = queryResult.Item2
        };
        return result;
    }

    public async Task<IEnumerable<ChatWithLastMessageResponseDto>> GetRecentChatsForUser(int userId)
    {
        var authenticatedUserId = authenticatedUserService.GetAuthenticatedUserId();
        if (authenticatedUserId != userId)
        {
            throw new UnauthorizedException();
        }
        var queryResult = await privateMessageRepository.GetRecentChatsForUser(userId);

        return mapper.Map<IEnumerable<ChatWithLastMessageResponseDto>>(queryResult);
    }
}
