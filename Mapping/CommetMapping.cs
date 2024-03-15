using api.Dtos.Comment;
using api.Models;

namespace api.Mapping
{
    public static class CommetMapping
    {
        public static Comment ToComment(
            this CreateCommentDto commentDto,
            int stockId,
            string UserId
        )
        {
            return new Comment
            {
                Content = commentDto.Content,
                Title = commentDto.Title,
                StockId = stockId,
                AppUserId = UserId
            };
        }

        public static Comment ToCommentWidthId(
            this CreateCommentDto commentDto,
            int id,
            int stockId,
            string userId
        )
        {
            return new Comment
            {
                Content = commentDto.Content,
                Title = commentDto.Title,
                Id = id,
                StockId = stockId,
                AppUserId = userId
            };
        }

        public static Comment ToCommentWidthId(
            this UpdateCommentDto commentDto,
            int id,
            int stockId,
            string userId
        )
        {
            return new Comment
            {
                Content = commentDto.Content,
                Title = commentDto.Title,
                Id = id,
                StockId = stockId,
                AppUserId = userId
            };
        }

        public static CommentViewDto ToViewDto(this Comment comment)
        {
            return new CommentViewDto
            {
                Title = comment.Title,
                Content = comment.Content,
                StockId = comment.StockId,
                CreatedOn = comment.CreatedOn,
                Id = comment.Id,
                CreateBy = comment.AppUser.UserName
            };
        }

        public static UpdateCommentDto ToUpdateDto(this Comment comment)
        {
            return new UpdateCommentDto { Title = comment.Title, Content = comment.Content, };
        }

        public static Comment ToCommentWithAppUser(this Comment comment, AppUser user)
        {
            comment.AppUser = user;
            return comment;
        }
    }
}
