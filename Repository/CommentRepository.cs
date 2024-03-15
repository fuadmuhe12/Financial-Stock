using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Data;
using api.Data.Migrations;
using api.Dtos;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mapping;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace api.Repository
{
    public class CommentRepository(FinanceContext context) : ICommentRepostitory
    {
        private readonly FinanceContext _context = context;

        public async Task<Comment> CreateCommentAsync(
            CreateCommentDto commentDto,
            int stockId,
            string userId
        )
        {
            var newComment = commentDto.ToComment(stockId, userId);
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();
            return newComment;
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _context.Comments.Where(commnet => commnet.Id == id).ExecuteDeleteAsync();
        }

        public async Task DeleteAllStockCommentAsync(int stockId)
        {
            var stocks = await _context.Stocks.FindAsync(stockId);
            if (stocks != null)
            {
                stocks.Comments = [];
                _context.Entry(stocks).CurrentValues.SetValues(stocks);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var val = await _context.Comments.Include(a =>a.AppUser).ToListAsync();
            Console.WriteLine(val);
            
          /*   foreach(Comment com in val){
                var user =   await _context.Users.FindAsync(com.UserId);
                com.AppUser =user;
            } */
            return val;
        }

        public async Task<List<Comment>?> GetAllStockCommmentAsync(int StockId)
        {
            var stock = await _context
                .Stocks.Include(stock => stock.Comments).ThenInclude(a => a.AppUser)
                .FirstOrDefaultAsync(stock => stock.Id == StockId);
            Console.WriteLine(stock);
            if (stock is null)
            {
                return null;
            }
            var commnets  =  stock.Comments;
           /*  foreach(Comment com in commnets){
                var user =   await _context.Users.FindAsync(com.UserId);
                com.AppUser =user;
            } */
            return commnets;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                var appuser = await _context.Users.FindAsync(comment.AppUserId);
                comment.AppUser = appuser;
            }

            return comment;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto commentDto)
        {
            var ExistingComment = await GetByIdAsync(id);

            if (ExistingComment is null)
            {
                return null;
            }
            else
            {
                ExistingComment.Content = commentDto.Content;
                ExistingComment.Title = commentDto.Title;
                await _context.SaveChangesAsync();
                return ExistingComment;
            }
        }

        public async Task DeleteAllCommentAsync()
        {
            await _context.Comments.ExecuteDeleteAsync();
        }
    }
}
