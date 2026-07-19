namespace Api.Modules.Features.Brands.Commands.Delete;

public record DeleteBrandCommand(List<int> Ids) : ICommand;