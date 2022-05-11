using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Common;
using Play.Inventory.Service.Clients;
using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> inventoryItemRepository;
        private readonly IRepository<CatalogItem> catalogItemRepository;


        public ItemsController(IRepository<InventoryItem> inventoryItemRepository, IRepository<CatalogItem> catalogItemRepository)
        {
            this.inventoryItemRepository = inventoryItemRepository;
            this.catalogItemRepository = catalogItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }

            var items = (await inventoryItemRepository.GetAllAsync(item => item.UserId == userId));
            var itemIds = items.Select(x => x.CatalogItemId);
            var catalogItems = await catalogItemRepository.GetAllAsync(item => itemIds.Contains(item.Id));
            var dtos = items.Select(item =>
            {
                var catalogItem = catalogItems.Single(x => x.Id == item.CatalogItemId);
                return item.AsDto(catalogItem.Name, catalogItem.Description);
            });


            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto)
        {
            var inventoryItem = await inventoryItemRepository.GetAsync(item => item.UserId == grantItemsDto.UserId
            && item.CatalogItemId == grantItemsDto.CatalogItemId);
            if (inventoryItem == null)
            {
                inventoryItem = new InventoryItem
                {
                    CatalogItemId = grantItemsDto.CatalogItemId,
                    UserId = grantItemsDto.UserId,
                    Quantity = grantItemsDto.Quantity,
                    AcquireDate = DateTimeOffset.UtcNow

                };
                await inventoryItemRepository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity += grantItemsDto.Quantity;
                await inventoryItemRepository.UpdateAsync(inventoryItem);
            }
            return Ok();

        }

    }


}