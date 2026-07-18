using JLek.POS.Domain.Catalog.Events;
using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class Ingredient : AggregateRoot<IngredientId>
{
    private Ingredient()
        : base(IngredientId.From(Guid.Empty))
    {
        Name = string.Empty;
    }

    private Ingredient(
        IngredientId id,
        string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public IngredientStatus Status { get; private set; }

    public static Ingredient Create(string name)
    {
        var ingredient = new Ingredient(
            IngredientId.New(),
            name);

        ingredient.RaiseDomainEvent(
            new IngredientCreatedEvent(ingredient.Id));

        return ingredient;
    }

    public void Rename(string name)
    {
        Name = name;
    }

    public void SetAvailability(IngredientStatus status)
    {
        Status = status;
    }
}