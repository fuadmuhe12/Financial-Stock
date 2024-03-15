using api.Data;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces;

public interface ICommentRepostitory
{
    Task<Comment?> GetByIdAsync(int id);
    Task<List<Comment>> GetAllAsync();

    Task<List<Comment>?> GetAllStockCommmentAsync(int StockId);
    Task DeleteCommentAsync(int id);

    Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto commentDto);

    Task<Comment> CreateCommentAsync( CreateCommentDto commentDto, int stockId,string userId);
    Task DeleteAllCommentAsync();
}
