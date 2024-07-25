using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core;
using Talabat.Core.Entities.Order;
using Talabat.Core.Services.Interfaces;
using Talabat_.APIS.DTOS;
using Talabat_.APIS.Errors;

namespace Talabat_.APIS.Controllers
{
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region CreateOrder
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto model)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<AddressDto, Address>(model.ShippingToAddress);
            var order = await _orderService.createOrderAsync(buyerEmail, model.BasketId, model.DeliveryMethodId, address);

            if (order is null) return BadRequest(new ApiResponse(400, "there is a problem with your order"));

            var result = _mapper.Map<Order, OrderToReturnDto>(order);

            return Ok(result);
        }
        #endregion



        #region  GetOrdersForUser
        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrdersForSpecUserAsync(buyerEmail);

            if (orders is null) return NotFound(new ApiResponse(404, "there is no order for you"));

            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }
        #endregion



        #region GetOrderByIdForUser
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdForSpecUSerAsync(buyerEmail, id);

            if (order is null) return NotFound(new ApiResponse(404, "there is no order for you"));

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
        #endregion



        #region GetDeilveryMethods
        [HttpGet("DeilveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeilveryMethods()
        {
            var DeliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

            return Ok(DeliveryMethods);
        }
        #endregion



    }
}
