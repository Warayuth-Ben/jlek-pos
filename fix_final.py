import glob, re

# Fix remaining domain entity assertions: .Should().Be("EnumValue") -> .Should().Be(EnumType.EnumValue)
# These are on persisted domain entities, NOT on DTO responses

fixes = {
    'IngredientStatus': ['Available', 'Unavailable'],
    'TableStatus': ['Available', 'Occupied', 'Ready', 'Served'],
    'KitchenTicketStatus': ['Pending', 'Preparing', 'Ready', 'Served'],
    'ProductCategoryStatus': ['Available', 'Unavailable'],
    'ProductStatus': ['Available', 'Unavailable'],
    'ProductVisibility': ['CashierOnly', 'MenuAndCashier'],
}

# Build pattern: find all .Should().Be("Xxx") that should be enum values
# Only fix assertions on domain objects (persisted, result from API DTO is already fine)
# Actually the issue is: some .Should().Be() are on DTOs (which should be strings) 
# and some are on domain entities (which should be enums)

# Strategy: Only fix assertions on local variables that access domain entities
# Specifically: persisted.XXX.Should().Be("Value") should become persisted.Status.Should().Be(EnumType.Value)

files = [
    'src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs',
    'src/JLek.POS.IntegrationTests/Kitchen/KitchenTicketTests.cs',
    'src/JLek.POS.IntegrationTests/Catalog/IngredientTests.cs',
    'src/JLek.POS.IntegrationTests/Catalog/ProductCategoryTests.cs',
]

# Build mapping: line content -> replacement pairs for each file
for fpath in files:
    with open(fpath, encoding='utf-8') as f:
        lines = f.readlines()
    
    modified = False
    for i, line in enumerate(lines):
        # Check if this line has a Status assertion that looks like it's on a domain entity
        # Domain entities in tests are accessed as: persisted.Status, table.Status, product.Status, etc.
        if any(x in line for x in ['persisted.', 'persisted!.', 'table.', 'table!.', 'ticket.', 'ticket!.']):
            # Check if it has .Should().Be("Xxx")
            m = re.search(r'\.Should\(\)\.Be\("(\w+)"\)', line)
            if m:
                val = m.group(1)
                # Replace with enum value based on which file we're in
                if 'DiningTable' in fpath or 'diningTable' in line or 'table.' in line:
                    replacement = f'.Should().Be(TableStatus.{val})'
                elif 'KitchenTicket' in fpath or 'ticket.' in line:
                    replacement = f'.Should().Be(KitchenTicketStatus.{val})'
                elif 'Ingredient' in fpath:
                    replacement = f'.Should().Be(IngredientStatus.{val})'
                elif 'ProductCategory' in fpath:
                    replacement = f'.Should().Be(ProductCategoryStatus.{val})'
                else:
                    replacement = f'.Should().Be({val})'  # fallback
                lines[i] = line.replace(f'.Should().Be("{val}")', replacement)
                modified = True
    
    if modified:
        with open(fpath, 'w', encoding='utf-8') as f:
            f.writelines(lines)
        print(f'Fixed: {fpath}')
    else:
        print(f'No changes: {fpath}')

# Also fix the remaining .Value on Guid in DiningTableTests.cs line 317
with open('src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs', encoding='utf-8') as f:
    c = f.read()
c2 = c.replace('updated.Id.Value.ToString()', 'updated.Id.ToString()')
if c2 != c:
    with open('src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs', 'w', encoding='utf-8') as f:
        f.write(c2)
    print('Fixed .Value in DiningTableTests.cs')

print('All done.')