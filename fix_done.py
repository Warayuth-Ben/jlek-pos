# Final fixes for remaining 5 errors

# Fix 1: Name assertions incorrectly converted to enum
fpath = 'src/JLek.POS.IntegrationTests/Catalog/ProductCategoryTests.cs'
with open(fpath, encoding='utf-8') as f:
    c = f.read()
c = c.replace('.Name.Should().Be(ProductCategoryStatus.Drinks)', '.Name.Should().Be("Drinks")')
with open(fpath, 'w', encoding='utf-8') as f:
    f.write(c)
print('Fixed ProductCategoryTests.cs name assertion')

fpath = 'src/JLek.POS.IntegrationTests/Catalog/IngredientTests.cs'
with open(fpath, encoding='utf-8') as f:
    c = f.read()
c = c.replace('.Name.Should().Be(IngredientStatus.Chicken)', '.Name.Should().Be("Chicken")')
with open(fpath, 'w', encoding='utf-8') as f:
    f.write(c)
print('Fixed IngredientTests.cs name assertion')

# Fix 2: DiningTableTests - remaining enum string issues and .Value
fpath = 'src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs'
with open(fpath, encoding='utf-8') as f:
    lines = f.readlines()
# Line 271-275 (0-indexed: 270-274) need fixing
for i, line in enumerate(lines):
    if 'table.Status == "Available"' in line:
        lines[i] = line.replace('table.Status == "Available"', 'table.Status == TableStatus.Available')
    if 'table.Status.Should().Be("Available")' in line:
        lines[i] = line.replace('table.Status.Should().Be("Available")', 'table.Status.Should().Be(TableStatus.Available)')
    if 'table.Status.Should().Be("Occupied")' in line:
        lines[i] = line.replace('table.Status.Should().Be("Occupied")', 'table.Status.Should().Be(TableStatus.Occupied)')
    # Line 317: fix .Value
    if 'updated.Id.Value.ToString()' in line:
        lines[i] = line.replace('updated.Id.Value.ToString()', 'updated.Id.ToString()')
with open(fpath, 'w', encoding='utf-8') as f:
    f.writelines(lines)
print('Fixed DiningTableTests.cs remaining issues')
print('All done - 5 errors fixed')