using System.Security.Claims;
using colekta_api.Models.Entities;
using colekta_api.Models.FiltersDto;
using colekta_api.Models.RequestDtos;
using colekta_api.Models.ResponseDtos;
using colekta_api.Models.ResultsModel;
using colekta_api.Repositories.Interfaces;
using colekta_api.Services.File;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace colekta_api.Services.Product;

public class ProductService : IProductInterface
{
    private readonly IProductRepository _productRepository;
    private readonly IFileInterface _fileService;
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository productRepository, IFileInterface fileService, UserManager<ApplicationUserModel> userManager, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _fileService = fileService;
        _userManager = userManager;
        _categoryRepository = categoryRepository;
    }

    public async Task<IResult> GetAllProductAsync(ProductFilterDto filters, ClaimsPrincipal userClaims)
    {
        var isAdmin = userClaims.IsInRole("Admin");
        var isCreator = userClaims.IsInRole("Creator");
        var includeDeleted = (isAdmin || isCreator) && filters.IncludeDeleted;
        
        var query = _productRepository.GetAllProductsQuery(filters, includeDeleted);

        var totalItems = await query.CountAsync();
    
        var itemsToSkip = (filters.Page - 1) * filters.PageSize;
        var products = await query.Skip(itemsToSkip)
            .Take(filters.PageSize)
            .ToListAsync();
    
        var itemsDto = products.Select(p => ProductResponseDto.ToDto(p)).ToList();
    
        var totalPages = (int)Math.Ceiling((double)totalItems / filters.PageSize);
        
        var response = new PagedResponseDto<ProductResponseDto>(
            Items: itemsDto,
            TotalItems: totalItems,
            CurrentPage: filters.Page,
            TotalPages: totalPages
            );

        return response.ToOkResult("Produtos listados com sucesso");
    }

    public async Task<IResult> CreateProductAsync(CreateProductDto dto, ClaimsPrincipal userClaims)
    {
        var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return "".ToUnauthorizedResult();
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return "".ToUnauthorizedResult();
        }
        
        if (!Guid.TryParse(dto.CategoriaId, out var categoryId))
        {
            return "Id de categoria inválido!".ToBadRequestResult();
        }
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null)
        {
            return "Categoria não encontrada!".ToBadRequestResult();
        }
        var productModel = CreateProductDto.ToProductModel(dto, category, user);
        
        for (int i = 0; i < dto.Imagens.Count; i++)
        {
            var file = dto.Imagens[i];
        
            var url = await _fileService.UploadImageAsync(file, "produtos-colecionaveis");
            var image = new ProductImageModel
            {
                Url = url,
                Order = i,
                IsCover = (i == dto.IndexImagemCapa),
                ProductId = productModel.Id,
                Product = productModel
            };
            productModel.Images.Add(image);
        }
        
        var response = ProductResponseDto.ToDto(productModel);
        
        var product = await _productRepository.CreateProductAsync(productModel);
        product.Category =  category;
        
        
        return response.ToOkResult("Produto cadastrado com sucesso!");
    }

    public async Task<IResult> GetProductById(Guid Id)
    {
        var product = await _productRepository.GetProductByIdAsync(Id);
        if (product is null)
        {
            return "Produto não encontrado!".ToNotFoundResult();
        }
        
        var productDto = ProductResponseDto.ToDto(product);
        productDto.Seller = SellerResponseDto.ToDto(product.Seller);
        
        return productDto.ToOkResult("Produto buscado com sucesso!");
    }

    public async Task<IResult> UpdateProductAsync(Guid id, UpdateProductDto productDto, ClaimsPrincipal userClaims)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product is null) 
        {
            return Results.NotFound("Produto não encontrado.");
        }

        var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        bool isAdminOrCreator = userClaims.IsInRole("Admin") || userClaims.IsInRole("Creator");

        if (!isAdminOrCreator && product.SellerId != userId)
        {
            return "Você não tem permissão para alterar este produto.".ToForbiddenResult();
        }
        
        var category = await _categoryRepository.GetByIdAsync(product.CategoryId);

        if (category is null)
        {
            return "Categoria não encontrada!".ToBadRequestResult();
        }

        if (productDto.Nome != null) product.Name = productDto.Nome;
        if (productDto.Descricao != null) product.Description = productDto.Descricao;
        if (productDto.Preco.HasValue) product.Price = productDto.Preco.Value;
        if (productDto.Estoque.HasValue) product.Stock = productDto.Estoque.Value;
        if (productDto.CategoriaId != null) product.CategoryId = category.Id;
        
        var response = ProductResponseDto.ToDto(await _productRepository.UpdateProductAsync(product));
        
        return response.ToOkResult("Produto atualizado com sucesso!");
    }

    public async Task<IResult> SoftDeleteProductAsync(Guid id, ClaimsPrincipal user)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        
        if (product is null || product.IsDelete) 
        {
            return "Produto não encontrado ou já removido.".ToNotFoundResult();
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        bool isAdminOrCreator = user.IsInRole("Admin") || user.IsInRole("Creator");

        if (!isAdminOrCreator && product.SellerId != userId)
        {
            return "Você não tem permissão para deletar este produto.".ToForbiddenResult();
        }

        product.IsDelete = true;

        await _productRepository.UpdateProductAsync(product);

        return "".ToOkResult("Produto removido com sucesso.");
    }
}