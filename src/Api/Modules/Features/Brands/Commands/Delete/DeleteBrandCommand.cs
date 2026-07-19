namespace Api.Modules.Features.Brands.Commands.Delete;

public record DeleteBrandCommand(List<Guid> Ids) : ICommand;