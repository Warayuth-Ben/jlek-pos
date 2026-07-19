import re

# Fix remaining domain entity assertions: persisted.Status.Should().Be("Available") -> persisted.Status.Should().Be(TableStatus.Available)
# These are domain entity property assertions (not DTO assertions)

with open('src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs', encoding='utf-8') as f:
    c = f.read()
original = c
# The errors are on persisted/table domain objects using string instead of enum
c = c.replace('persisted.Status.Should().Be("Available")', 'persisted.Status.Should().Be(TableStatus.Available)')
c = c.replace('persisted.Status.Should().Be("Occupied")', 'persisted.Status.Should().Be(TableStatus.Occupied)')
c = c.replace('persisted.Status.Should().Be("Ready")', 'persisted.Status.Should().Be(TableStatus.Ready)')
c = c.replace('persisted.Status.Should().Be("Served")', 'persisted.Status.Should().Be(TableStatus.Served)')
# line 317: updated.Id.Value.ToString()
c = c.replace('updated.Id.Value.ToString()', 'updated.Id.ToString()')
if c != original:
    with open('src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs', 'w', encoding='utf-8') as f:
        f.write(c)
    print('Fixed: DiningTableTests.cs')

with open('src/JLek.POS.IntegrationTests/Kitchen/KitchenTicketTests.cs', encoding='utf-8') as f:
    c = f.read()
original = c
c = c.replace('persisted.Status.Should().Be("Pending")', 'persisted.Status.Should().Be(KitchenTicketStatus.Pending)')
c = c.replace('persisted.Status.Should().Be("Preparing")', 'persisted.Status.Should().Be(KitchenTicketStatus.Preparing)')
c = c.replace('persisted.Status.Should().Be("Ready")', 'persisted.Status.Should().Be(KitchenTicketStatus.Ready)')
c = c.replace('persisted.Status.Should().Be("Served")', 'persisted.Status.Should().Be(KitchenTicketStatus.Served)')
if c != original:
    with open('src/JLek.POS.IntegrationTests/Kitchen/KitchenTicketTests.cs', 'w', encoding='utf-8') as f:
        f.write(c)
    print('Fixed: KitchenTicketTests.cs')

with open('src/JLek.POS.IntegrationTests/Catalog/IngredientTests.cs', encoding='utf-8') as f:
    c = f.read()
original = c
c = c.replace('persisted.Status.Should().Be("Available")', 'persisted.Status.Should().Be(IngredientStatus.Available)')
c = c.replace('persisted.Status.Should().Be("Unavailable")', 'persisted.Status.Should().Be(IngredientStatus.Unavailable)')
if c != original:
    with open('src/JLek.POS.IntegrationTests/Catalog/IngredientTests.cs', 'w', encoding='utf-8') as f:
        f.write(c)
    print('Fixed: IngredientTests.cs')

with open('src/JLek.POS.IntegrationTests/Catalog/ProductCategoryTests.cs', encoding='utf-8') as f:
    c = f.read()
original = c
c = c.replace('persisted.Status.Should().Be("Available")', 'persisted.Status.Should().Be(ProductCategoryStatus.Available)')
c = c.replace('persisted.Status.Should().Be("Unavailable")', 'persisted.Status.Should().Be(ProductCategoryStatus.Unavailable)')
if c != original:
    with open('src/JLek.POS.IntegrationTests/Catalog/ProductCategoryTests.cs', 'w', encoding='utf-8') as f:
        f.write(c)
    print('Fixed: ProductCategoryTests.cs')

print('Done')