using Backend.Models;

namespace Backend.Services.Interfaces;

public interface ITokenProviderService
{
    (string token, DateTime validTo) CreateToken(UserModel? user);
}