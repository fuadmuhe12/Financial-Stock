using api.Dtos.Comment;
using api.Extensions;
using api.Interfaces;
using api.Mapping;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepostitory _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        const string _commentRouteGet = "GetCommentById";

        public CommentController(
            ICommentRepostitory commentRepo,
            IStockRepository stockRepo,
            UserManager<AppUser> userManager
        )
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            return Ok(comments.Select(comment => comment.ToViewDto()));
        }

        [HttpGet]
        [Route("{id:int}", Name = _commentRouteGet)]
        public async Task<IActionResult> GetCommentBYId([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment is null)
            {
                return NotFound();
            }
            return Ok(comment.ToViewDto());
        }

        [HttpGet]
        [Route("stock/{stockId:int}")]
        public async Task<IActionResult> getStockComment([FromRoute] int stockId)
        {
            var commnents = await _commentRepo.GetAllStockCommmentAsync(stockId);
            if (commnents is null)
            {
                return NotFound("Stock is Not Found");
            }
            return Ok(commnents.Select(comment => comment.ToViewDto()));
        }

        [HttpPost]
        [Route("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> Create(
            [FromBody] CreateCommentDto commentDto,
            [FromRoute] int stockId
        )
        {
            if (!await _stockRepo.StockExist(stockId))
            {
                return BadRequest("Stock does not Exist!");
            }
            var newComment = await _commentRepo.CreateCommentAsync(
                commentDto,
                stockId,
                User.GetUserId()!
            );
            newComment.AppUser = await _userManager.FindByIdAsync(User.GetUserId()!);

            return CreatedAtRoute(
                _commentRouteGet,
                new { id = newComment.Id },
                newComment.ToViewDto()
            );
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateCommentDto commentDto,
            [FromRoute] int id
        )
        {
            var newComment = await _commentRepo.UpdateCommentAsync(id, commentDto);
            if (newComment is null)
            {
                return NotFound();
            }
            return Ok(newComment.ToViewDto());
        }

        [HttpDelete]
        [Route("{CommentId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int CommentId)
        {
            await _commentRepo.DeleteCommentAsync(CommentId);
            return NoContent();
        }

        [HttpDelete]
        [Route("/DeleteAll")]
        public async Task<IActionResult> DeleteAllStockComment()
        {
            await _commentRepo.DeleteAllCommentAsync();
            return NoContent();
        }
    }
}