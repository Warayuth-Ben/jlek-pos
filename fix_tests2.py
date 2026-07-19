import re

# Fix 1: Restore seed method returns - need .Id.Value from DomainId types
seed_fixes = {
    'src/JLek.POS.IntegrationTests/Catalog/ProductCategoryTests.cs': [
        ('return category.Id;', 'return category.Id.Value;'),
    ],
    'src/JLek.POS.IntegrationTests/Catalog/IngredientTests.cs': [
        ('return ingredient.Id;', 'return ingredient.Id.Value;'),
    ],
    'src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs': [
        ('return table.Id;', 'return table.Id.Value;'),
        ('return diningTable.Id;', 'return diningTable.Id.Value;'),
    ],
    'src/JLek.POS.IntegrationTests/Kitchen/KitchenTicketTests.cs': [
        ('return ticket.Id;', 'return ticket.Id.Value;'),
    ],
}

for fpath, replacements in seed_fixes.items():
    with open(fpath, encoding='utf-8') as f:
        c = f.read()
    original = c
    for old, new in replacements:
        c = c.replace(old, new)
    if c != original:
        with open(fpath, 'w', encoding='utf-8') as f:
            f.write(c)
        print(f'Seed fix: {fpath}')

# Fix 2: Enum method calls - string params should be domain enums
# Read method signatures from error messages
# DiningTableTests.cs line 86: .SetStatus("Available") -> .SetStatus(TableStatus.Available)
# KitchenTicketTests.cs line 100: .Create("P", "Pending") -> .Create("P", KitchenTicketStatus.Pending)

with open('src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs', encoding='utf-8') as f:
    c = f.read()
original = c
c = re.sub(r'\.SetStatus\("Available"\)', '.SetStatus(TableStatus.Available)', c)
c = re.sub(r'\.SetStatus\("Occupied"\)', '.SetStatus(TableStatus.Occupied)', c)
c = re.sub(r'SetAvailability\("Available"\)', 'SetAvailability(ProductStatus.Available)', c)
c = re.sub(r'SetAvailability\("Unavailable"\)', 'SetAvailability(ProductStatus.Unavailable)', c)
c = re.sub(r'\) && status == "Available"', ') && status == TableStatus.Available', c)
c = re.sub(r'\.Create\("([^"]+)",\s*"Available"', r'.Create("\1", TableStatus.Available)', c)
c = re.sub(r'\.Create\("([^"]+)",\s*"Occupied"', r'.Create("\1", TableStatus.Occupied)', c)
# Fix line 317: .Value on Guid
c = re.sub(r'table\.Id\.ToString\(\)\.Should\(\)\.Contain\(updated\.Id\.Value\.ToString\(\)\)', 
           r'table.Id.ToString().Should().Contain(updated.Id.ToString())', c)
if c != original:
    with open('src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs', 'w', encoding='utf-8') as f:
        f.write(c)
    print('Fixed: DiningTableTests.cs')

with open('src/JLek.POS.IntegrationTests/Kitchen/KitchenTicketTests.cs', encoding='utf-8') as f:
    c = f.read()
original = c
c = re.sub(r'\.Create\("([^"]+)",\s*"Pending"', r'.Create("\1", KitchenTicketStatus.Pending)', c)
c = re.sub(r'\.Create\("([^"]+)",\s*"Preparing"', r'.Create("\1", KitchenTicketStatus.Preparing)', c)
c = re.sub(r'\.Create\("([^"]+)",\s*"Ready"', r'.Create("\1", KitchenTicketStatus.Ready)', c)
c = re.sub(r'\.Create\("([^"]+)",\s*"Served"', r'.Create("\1", KitchenTicketStatus.Served)', c)
if c != original:
    with open('src/JLek.POS.IntegrationTests/Kitchen/KitchenTicketTests.cs', 'w', encoding='utf-8') as f:
        f.write(c)
    print('Fixed: KitchenTicketTests.cs')

with open('src/JLek.POS.IntegrationTests/Catalog/IngredientTests.cs', encoding='utf-8') as f:
    c = f.read()
original = c
c = re.sub(r'\.SetAvailability\("Available"\)', '.SetAvailability(IngredientStatus.Available)', c)
c = re.sub(r'\.SetAvailability\("Unavailable"\)', '.SetAvailability(IngredientStatus.Unavailable)', c)
if c != original:
    with open('src/JLek.POS.IntegrationTests/Catalog/IngredientTests.cs', 'w', encoding='utf-8') as f:
        f.write(c)
    print('Fixed: IngredientTests.cs')

with open('src/JLek.POS.IntegrationTests/Catalog/ProductCategoryTests.cs', encoding='utf-8') as f:
    c = f.read()
original = c
c = re.sub(r'\.SetStatus\("Available"\)', '.SetStatus(ProductCategoryStatus.Available)', c)
c = re.sub(r'\.SetStatus\("Unavailable"\)', '.SetStatus(ProductCategoryStatus.Unavailable)', c)
if c != original:
    with open('src/JLek.POS.IntegrationTests/Catalog/ProductCategoryTests.cs', 'w', encoding='utf-8') as f:
        f.write(c)
    print('Fixed: ProductCategoryTests.cs')

print('All fixes applied.')