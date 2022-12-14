using AutoMapper;
using MiniLojaVirtual.ProductApi.Dtos;
using MiniLojaVirtual.ProductApi.Models;
using MiniLojaVirtual.ProductApi.Repositories;

namespace MiniLojaVirtual.ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var productsEntity = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDto>>(productsEntity);
    }
    public async Task<ProductDto> GetProductById(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDto>(productEntity);
    }
    public async Task AddProduct(ProductDto productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Create(productEntity);
        productDto.Id = productEntity.Id;
    }
    public async Task UpdateProduct(ProductDto productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Update(productEntity);
    }
    public async Task RemoveProduct(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        await _productRepository.Delete(productEntity.Id);
    }
}