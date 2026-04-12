using Microsoft.AspNetCore.Identity;

namespace BookShoppingCartMvc1.Repositories
{
    public class CartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepository(ApplicationDbContext db,IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager) 
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AddItem(int bookId, int qty)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return false;

                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart()
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();
                // cart detail section
                var cartItem = _db.CartDetails.FirstOrDefault(a => a.ShoppingCartId==cart.Id && a.BookId==bookId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty
                    };
                    _db.CartDetails.Add(cartItem);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        private async Task<ShoppingCart> GetCart(string userId) 
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
